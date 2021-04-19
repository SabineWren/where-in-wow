module internal WhereInWow.Queries
open WhereInWow.Models

let Cte_Locations: string = "
Cte_Locations as (
   Select
   e.Id as ExpansionId,
   m.Id as MapId,
   z.Name as Zone,
   Lower(Hex(l.Id)) as LocId,
   l.Name as Location,
   l.Lat, l.Lng
   From Locations l
   Inner Join Zones      z On z.Id=l.ZoneId
   Inner Join Expansions e On e.Id=z.ExpansionId
   Inner Join Maps       m On m.Id=e.MapId)"

let GetRandomMapVanilla() =
   {| Test=true |}
   |> Db.First<LocationMeta> $"
With
{Cte_Locations}
Select MapId, Zone, LocId, Location, Lat, Lng
From Cte_Locations
Where ExpansionId = 1
Order By Rand()
Limit 1"

let GetRandomMapAll() =
   {| Test=true |}
   |> Db.First<LocationMeta> $"
With
{Cte_Locations}
Select MapId, Zone, LocId, Location, Lat, Lng
From Cte_Locations
Order By Rand()
Limit 1"
