### Where In Wow
Fork of Kruithne's Where In Warcraft (upstream deleted).

This version will feature an expansion selector.

### Dependencies
- MariaDB or MySQL
- Npm
- Dotnet 5 or later
- VS Code with Ionide and ESLint
Use Ionide's file explorer to view FSharp files.

### Build Instructions
Provide conn string either as environment variable or in dbconfig.json.
```
{ "WiwConnString": "server=localhost;uid=<name>;pwd=<pwd>;database=<name>" }
```
DbUp will create a database using your default character set.
- Migrations `dotnet run -p Migrations Migrations.csproj`.
- Back end `dotnet run -p Back_End WhereInWow.fsproj`.
- Front-end build steps defined in package.json.
