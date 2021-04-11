With
Cte_Classic as (
Select
	e.Id as ExpansionId,
	m.Id as MapId,
	z.Name as Zone,
	l.Id as LocId,
	l.Name as Location,
	l.Lat, l.Lng
From LocationsClassic l
Inner Join Zones      z On z.Id=l.ZoneId
Inner Join Expansions e On e.Id=z.ExpansionId
Inner Join Maps       m On m.Id=e.MapId)

With
Cte_Retail as (
Select
	e.Id as ExpansionId,
	m.Id as MapId,
	z.Name as Zone,
	l.Id as LocId,
	l.Name as Location,
	l.Lat, l.Lng
From LocationsRetail l
Inner Join Zones      z On z.Id=l.ZoneId
Inner Join Expansions e On e.Id=z.ExpansionId
Inner Join Maps       m On m.Id=e.MapId)


-- Back end Vanilla
Select MapId, Zone, Hex(LocId), Location, Lat, Lng
From Cte_Classic;

-- Back end TBC
Select MapId, Zone, Hex(LocId), Location, Lat, Lng
From Cte_Classic
Union All
Select MapId, Zone, Hex(LocId), Location, Lat, Lng
From Cte_Retail
Where ExpansionId = 2;

-- Back end WotLK
Select MapId, Zone, Hex(LocId), Location, Lat, Lng
From Cte_Classic
Union All
Select MapId, Zone, Hex(LocId), Location, Lat, Lng
From Cte_Retail
Where ExpansionId = 2
Or    ExpansionId = 3;

-- Back end WoD
Select MapId, Zone, Hex(LocId), Location, Lat, Lng
From Cte_Retail
Where ExpansionId <= 4;

-- Back end BfA
Select MapId, Zone, Hex(LocId), Location, Lat, Lng
From Cte_Retail;
