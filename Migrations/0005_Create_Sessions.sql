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
	Primary key(LocationID, Token),
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
*/
