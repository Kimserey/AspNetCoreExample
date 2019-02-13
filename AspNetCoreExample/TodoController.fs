namespace AspNetCoreExample

open Microsoft.AspNetCore.Mvc
open System
open System.ComponentModel.DataAnnotations
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Options
open Microsoft.Extensions.Logging

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
type TodoController(config: IConfiguration, logger: ILogger<TodoController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member __.GetAll() =
        let x: string = config.GetValue("configuration1")

        logger.LogWarning("This is my configuration {hello}", x)

        // { "template": "This is my configuration {x}"; "hello": "hello world" }

        [ x; "Bye bye" ]

    [<HttpGet("{id}")>]
    member __.Get(id: string) =
        "Hello"

    [<HttpPost("{id}")>]
    member ctrl.Post(id: string, [<FromHeader>]content: string, [<FromQuery>]test: string, todo: Todo) =
        "Hello"