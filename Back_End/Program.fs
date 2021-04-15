module WhereInWow.App

open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.FileProviders
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open System
open System.IO
open WhereInWow

let webroot = Directory.GetCurrentDirectory() + "/../Web_Root"

let webApp =
   choose [
      GET >=> choose [
         route "/" >=> htmlFile "index.html"
         route "/hello" >=> HttpHandlers.GetHello
      ]
      setStatusCode 404 >=> text "Not Found" ]

let errorHandler (ex : Exception) (logger : ILogger) =
   logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
   clearResponse
   >=> setStatusCode 500
   >=> text ex.Message

let configureCors (builder : CorsPolicyBuilder) =
   builder
      .WithOrigins(
         "http://localhost:5000",
         "https://localhost:5001")
      .AllowAnyMethod()
      .AllowAnyHeader()
      |> ignore

let configureApp (app : IApplicationBuilder) =
   app.UseDeveloperExceptionPage()//.UseGiraffeErrorHandler(errorHandler)
      .UseStaticFiles(
         new StaticFileOptions(
            FileProvider = new PhysicalFileProvider(webroot),
            RequestPath = new PathString("")))
      .UseCors(configureCors)
      .UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
   services.AddCors()   |> ignore
   services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
   builder.AddConsole()
         .AddDebug() |> ignore

[<EntryPoint>]
let main args =
   Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(
         fun webHostBuilder ->
            webHostBuilder
               .Configure(Action<IApplicationBuilder> configureApp)
               .ConfigureServices(configureServices)
               .ConfigureLogging(configureLogging)
               .UseContentRoot(webroot)
               .UseWebRoot(webroot)
               |> ignore)
      .Build()
      .Run()
   0
