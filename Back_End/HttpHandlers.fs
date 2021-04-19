module internal WhereInWow.HttpHandlers
open FSharp.Control.Tasks
open Giraffe
open Microsoft.AspNetCore.Http
open System.Threading.Tasks
open WhereInWow.Models

let TaskResultBind<'A,'B>(b: 'A -> Task<Result<'B,WiwError>>)(a: Result<'A,WiwError>) = task {
   match a with
   | Ok v -> return! b v
   | Error e -> return Error e
}


let GetRandomLocation(expansion: byte) = task {
   let re: Result<Expansion,WiwError> = Expansion.ToDomain expansion
   let getLocation e =
      match e with
      | Classic -> Queries.GetRandomMapVanilla()
      | Retail -> Queries.GetRandomMapAll()
   return! re |> TaskResultBind getLocation
}
let Init =
   fun (next: HttpFunc)(ctx: HttpContext) ->
      task {
         let! location = GetRandomLocation 1uy
         match location with
         | Ok v -> return! json v next ctx
         | Error e ->
            let msg =
               match e with
               | EmptyResult -> "No database row(s) retrieved."
               | NoRowAffected -> "No database row(s) affected."
               | Validation m -> m
            ctx.SetStatusCode 500
            return! ctx.WriteTextAsync msg
      }
