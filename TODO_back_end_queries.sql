With
Cte_Locations as (
Select
	e.Id as ExpansionId,
	m.Id as MapId,
	z.Name as Zone,
	l.Id as LocId,
	l.Name as Location,
	l.Lat, l.Lng
From Locations l
Inner Join Zones      z On z.Id=l.ZoneId
Inner Join Expansions e On e.Id=z.ExpansionId
Inner Join Maps       m On m.Id=e.MapId)

-- Back end Vanilla
Select MapId, Zone, Hex(LocId), Location, Lat, Lng From Cte_Locations
Where ExpansionId = 1;

-- Back end TBC
Select MapId, Zone, Hex(LocId), Location, Lat, Lng From Cte_Locations
Where ExpansionId <= 2;

-- Back end WotLK
Select MapId, Zone, Hex(LocId), Location, Lat, Lng From Cte_Locations
Where ExpansionId <= 3;

-- Back end WoD
Select MapId, Zone, Hex(LocId), Location, Lat, Lng From Cte_Locations
Where ExpansionId >= 2 && ExpansionId <= 3;

-- Back end BfA
Select MapId, Zone, Hex(LocId), Location, Lat, Lng From Cte_Locations
Where ExpansionId >= 2;
