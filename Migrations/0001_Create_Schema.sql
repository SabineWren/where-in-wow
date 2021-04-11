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
	Constraint `FK_Zones_Maps`
		Foreign Key (ExpansionId) References Expansions(Id)
		On Delete Restrict
);

Create Table LocationsClassic (
	Id Binary(16) Primary key,
	ZoneId SmallInt Not Null,
	Name NVarChar(100) Not Null,
	Lat Float Not Null,
	Lng Float Not Null,
	Constraint `FK_LocationsClassic_Zones`
		Foreign Key (ZoneId) References Zones(Id)
		On Delete Restrict
);
Create Table LocationsRetail (
	Id Binary(16) Primary key,
	ZoneId SmallInt Not Null,
	Name NVarChar(100) Not Null,
	Lat Float Not Null,
	Lng Float Not Null,
	Constraint `FK_LocationsRetail_Zones`
		Foreign Key (ZoneId) References Zones(Id)
		On Delete Restrict
);

Create Table Sessions (
	Token Binary(16) Primary key,
	Location Binary(16) Not Null,
	GameMode Bit Not Null,
	Lives TinyInt Not Null,
	Score Int Not null
);

Create Table GuessesClassic (
	LocationID Binary(16) Not Null,
	Token Binary(16) Not Null,
	DistPct Float Not Null,
	Constraint `FK_GuessesClassic_LocationsClassic`
		Foreign Key (LocationID) References LocationsClassic(Id)
		On Delete Cascade,
	Constraint `FK_GuessesClassic_Sessions`
		Foreign Key (Token) References Sessions(Token)
		On Delete Cascade
);
Create Table GuessesRetail (
	LocationID Binary(16) Not Null,
	Token Binary(16) Not Null,
	DistPct Float Not Null,
	Constraint `FK_GuessesRetail_LocationsRetail`
		Foreign Key (LocationID) References LocationsRetail(Id)
		On Delete Cascade,
	Constraint `FK_GuessesRetail_Sessions`
		Foreign Key (Token) References Sessions(Token)
		On Delete Cascade
);

/*
Drop Table GuessesRetail;
Drop Table GuessesClassic;
Drop Table Sessions;
Drop Table LocationsRetail;
Drop Table LocationsClassic;
Drop Table Zones;
Drop Table Expansions;
Drop Table Maps;
*/
