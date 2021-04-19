namespace WhereInWow.Models

type WiwError =
   | EmptyResult
   | NoRowAffected
   | Validation of string

type Expansion =
   | Classic
   | Retail
   static member ToDomain(e: byte): Result<Expansion,WiwError> =
      match e with
      | 1uy -> Classic |> Ok
      | 2uy -> Retail |> Ok
      | _ -> sprintf "Invalid Expansion [%d]" e |> Validation |> Error

[<CLIMutable>]
type LocationMeta = {
   MapId: byte
   Zone: string
   LocId: string; Location: string
   Lat: float; Lng: float
}
