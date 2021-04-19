using DbUp;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
//Migrations journalled to table `schemaversions`.
//Package file includes logical filename for journal Id.
namespace Migrations {
	class Program {
		private class _dbConfig { public string WiwConnString { get; set; } }
		static int Main(string[] args) {
			var dbconfig = JsonSerializer.Deserialize<_dbConfig>(
				File.ReadAllText(Directory.GetCurrentDirectory() + "/../dbconfig.json"));
			var result = _run(dbconfig.WiwConnString);
			return result.Successful ? 0 : 1;
		}
		private static DbUp.Engine.DatabaseUpgradeResult _run(string connString) {
			EnsureDatabase.For.MySqlDatabase(connString);

			_print("Looking for migrations...");
			var assembly = Assembly.GetExecutingAssembly();
			_print(assembly.GetName().FullName);

			var upgrader = DeployChanges.To
				.MySqlDatabase(connString)
				.WithScriptsEmbeddedInAssembly(
					assembly,
					(string n) => !n.StartsWith("TODO") && n.EndsWith(".sql"))
				.WithTransactionPerScript()
				.WithExecutionTimeout(TimeSpan.FromMinutes(60))
				.LogToConsole()
				.LogScriptOutput()
				.Build();

			var _scripts = upgrader.GetScriptsToExecute();
			if (upgrader.IsUpgradeRequired()) {
				_print("DbUp Starting Migration(s)."); }
			else {
				_print("DbUp No Migration Required."); }

			string error;
			upgrader.TryConnect(out error);
			if (error.Length != 0) { _print(error); }
			return upgrader.PerformUpgrade();
		}
		private static void _print(string s) {
			System.Diagnostics.Debug.WriteLine(s);
			Console.WriteLine(s);
		}
	}
}
