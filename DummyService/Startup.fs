namespace DummyService

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Mvc

module Counter =

    let mutable value = 0

[<Route("api")>]
[<ApiController>]
type DummyController() =
    inherit ControllerBase()

    [<HttpGet>]
    member __.Get() =
        Counter.value <- Counter.value + 1

        if Counter.value < 3 then
            failwith "not working"

        "Dummy is working!"

type Startup() =

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddMvc()
        |> ignore

    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseMvc()
        |> ignore