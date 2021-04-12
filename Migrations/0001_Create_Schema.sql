Create Table Maps (
	Id TinyInt Primary Key,
	Name NVarChar(100) Not Null
);

Create Table Expansions (
	Id TinyInt Primary Key,
	MapId TinyInt,
	Name NVarChar(100) Not Null,
	Constraint `FK_Expansions_Maps`
		Foreign Key (MapId) References Maps(Id)
		On Delete Restrict
);

Create Table Zones (
	Id SmallInt Primary Key Auto_Increment,
	ExpansionId TinyInt Not Null,
	Name NVarChar(100) Not Null,
	Constraint `FK_Zones_Expansions`
		Foreign Key (ExpansionId) References Expansions(Id)
		On Delete Restrict
);

/* The original app used Md5(Name) as Id for classic locations,
but retail locations are a mix. Some are Md5(Name), others are
Md5(Concat(ZoneName,Name)), and the rest I couldn't figure out.
No 3C2 pair of (ExpansionId,ZoneName,Name) guarantees uniqueness.
To make them both unique and deterministic, I changed all Ids to
Md5(Concat(ExpansionId,ZoneName,Name)). */
Create Table Locations (
	Id Binary(16) Primary key,
	ZoneId SmallInt Not Null,
	Name NVarChar(100) Not Null,
	Lat Float Not Null,
	Lng Float Not Null,
	Constraint `FK_Locations_Zones`
		Foreign Key (ZoneId) References Zones(Id)
		On Delete Restrict
);

Create Table Sessions (
	Token Binary(16) Primary key,
	Location Binary(16) Not Null,
	ExpansionSelect TinyInt Not Null,
	Lives TinyInt Not Null,
	Score Int Not null
);

Create Table Guesses (
	LocationID Binary(16) Not Null,
	Token Binary(16) Not Null,
	DistPct Float Not Null,
	Constraint `FK_Guesses_Locations`
		Foreign Key (LocationID) References Locations(Id)
		On Delete Cascade,
	Constraint `FK_Guesses_Sessions`
		Foreign Key (Token) References Sessions(Token)
		On Delete Cascade
);

/*
Drop Table Guesses;
Drop Table Sessions;
Drop Table Locations;
Drop Table Zones;
Drop Table Expansions;
Drop Table Maps;
*/
