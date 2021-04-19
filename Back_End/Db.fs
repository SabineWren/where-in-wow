module internal WhereInWow.Db
open Dapper
open FSharp.Control.Tasks
open MySql.Data.MySqlClient
open WhereInWow.Models

let getConn() =
   new MySqlConnection(Environment.ConnString)

let Exec(sql:string)(arg: obj) =
   getConn().ExecuteAsync(sql, arg)

let ExecN(sql:string)(arg: obj) = task {
   let! numRows = getConn().ExecuteAsync(sql, arg)
   return match numRows >= 1 with
          | true -> Ok ()
          | false -> Error NoRowAffected
}

let First<'T>(sql:string)(arg: obj) = task {
   let! r = getConn().QueryFirstOrDefaultAsync<'T>(sql, arg)
   return match System.DBNull.Value.Equals(r) with
          | true -> EmptyResult |> Error
          | false -> r |> Ok
}

let Many<'T>(sql:string)(arg: obj) =
   getConn().QueryAsync<'T>(sql, arg)

let Scalar<'T>(sql:string)(arg: obj) = task {
   let! r = getConn().ExecuteScalarAsync<'T>(sql, arg)
   return match System.DBNull.Value.Equals(r) with
          | true -> Error EmptyResult
          | false -> Ok r
}
