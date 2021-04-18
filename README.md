## where-in-wow
Fork of Kruithne's Where In Warcraft (upstream deleted).

This version will feature an expansion selector.

## Build Instructions
You need NPM and dotnet 5 or later. Project set up for VS Code, but use Ionide's file explorer to view FSharp files.
- Front end build steps defined in package.json.
- Back end `dotnet run -p Back_End WhereInWhere.fsproj`.

## Migrations
The SQL dialect is MariaDB, although any DB that supports auto_increment will probably work (i.e. not SQL Server). Provide a connection string in /Migrations/dbconfig.json
```
{ "ConnString": "server=localhost;uid=<name>;pwd=<pwd>;database=<name>" }
```
DbUp will create a database using your default character set. Run with `dotnet run -p Migrations Migrations.csproj`.
