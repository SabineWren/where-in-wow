module internal WhereInWow.Environment
open FSharp.Data
open System.IO

let private envConnString = "WiwConnString"

let InitEnvironmentVariables() =
    let filepath = Directory.GetCurrentDirectory() + "/../dbconfig.json"
    let varNames = [| envConnString |]
    if (File.Exists filepath) then
        let json = File.ReadAllText(filepath) |> JsonValue.Parse
        let CopyVar (varName: string) =
            //JsonValue.ToString() adds quotes so <text> becomes <"text">
            let value = json.GetProperty(varName).AsString()
            do System.Environment.SetEnvironmentVariable(varName, value)
        do varNames |> Array.map CopyVar |> ignore
    let assertVarDefined (varName: string) =
        if System.Environment.GetEnvironmentVariable(varName) = null
        then do failwithf "Missing %s environment variable." varName
    do varNames |> Array.map assertVarDefined |> ignore

let ConnString: string =
    System.Environment.GetEnvironmentVariable(envConnString)
