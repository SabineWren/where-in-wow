//This module wraps Dapper functions with curryable calls and exception catching
module internal WhereInWow.Db
open Dapper
open FSharp.Control.Tasks
open MySql.Data.MySqlClient
open System.Data.Common
open System.Threading.Tasks
open WhereInWow.Models

let getConn() =
   new MySqlConnection(Environment.ConnString)

let ExecMaybe(sql:string)(arg: obj): Task<Result<Unit,WiwError>> = task {
   try
      let! _ = getConn().ExecuteAsync(sql, arg)
      return Ok ()
   with
   | :?DbException as ex ->
      return SqlExcept ex |> Error
}

let ExecN(sql:string)(arg: obj): Task<Result<Unit,WiwError>> = task {
   try
      let! numRows = getConn().ExecuteAsync(sql, arg)
      return match numRows >= 1 with
             | true -> Ok ()
             | false -> Error NoRowAffected
   with
   | :?DbException as ex ->
      return SqlExcept ex |> Error
}

let First<'T>(sql:string)(arg: obj): Task<Result<'T,WiwError>> = task {
   try
      let! r = getConn().QueryFirstAsync<'T>(sql, arg)
      return r |> Ok
   with
   | :?DbException as ex ->
      return SqlExcept ex |> Error
}

let Many<'T>(sql:string)(arg: obj): Task<Result<'T seq,WiwError>> = task {
   try
      let! rs = getConn().QueryAsync<'T>(sql, arg)
      return rs |> Ok
   with
   | :?DbException as ex ->
      return SqlExcept ex |> Error
}

let Scalar<'T>(sql:string)(arg: obj): Task<Result<'T,WiwError>> = task {
   try
      let! r = getConn().ExecuteScalarAsync<'T>(sql, arg)
      return match System.DBNull.Value.Equals(r) with
             | true -> Error EmptyResult
             | false -> Ok r
   with
   | :?DbException as ex ->
      return SqlExcept ex |> Error
}
