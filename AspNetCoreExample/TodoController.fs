namespace AspNetCoreExample

open Microsoft.AspNetCore.Mvc
open System
open System.ComponentModel.DataAnnotations
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Options

type Todo =
    {
        [<Required>]
        title: string
        content: string
    }

[<CLIMutable>]
type Person =
    {
        name: string
        age: int
    }

[<ApiController>]
[<Route("/api/todos")>]
type TodoController(config: IConfiguration) =
    inherit ControllerBase()

    [<HttpGet>]
    member __.GetAll() =
        let x = config.GetValue("configuration1")
        [ x; "Bye bye" ]

    [<HttpGet("{id}")>]
    member __.Get(id: string) =
        "Hello"

    [<HttpPost("{id}")>]
    member ctrl.Post(id: string, [<FromHeader>]content: string, [<FromQuery>]test: string, todo: Todo) =
        "Hello"