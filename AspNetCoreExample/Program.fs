namespace AspNetCoreExample

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging

module Program =
    open NLog.Web

    let exitCode = 0

    let CreateWebHostBuilder args =
        WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(fun logging ->
                logging.ClearProviders()
                |> ignore
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                |> ignore
            )
            .UseNLog()

    [<EntryPoint>]
    let main args =
        let logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger()

        try
            try
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run()
            with ex ->
                logger.Error(ex, "Stopped program because of exception")
                raise ex
        finally
            NLog.LogManager.Shutdown();

        exitCode
