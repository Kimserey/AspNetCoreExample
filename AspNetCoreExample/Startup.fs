namespace AspNetCoreExample

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("/api/todos")>]
type TodoController() =
    inherit ControllerBase()

    [<HttpGet>]
    member __.GetAll() = [ "Hello World"; "Bye bye" ]

    [<HttpGet("{id}")>]
    member __.Get(id: string) =
        "Hello"


type Startup() =

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddMvc() |> ignore

        ()

    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =

        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseMvc()
        |> ignore