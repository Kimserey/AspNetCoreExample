namespace AspNetCoreExample

open Microsoft.AspNetCore.Mvc
open System

type Todo =
    {
        title: string
        content: string
    }

[<ApiController>]
[<Route("/api/todos")>]
type TodoController() =
    inherit ControllerBase()

    [<HttpGet>]
    member __.GetAll() = [ "Hello World"; "Bye bye" ]

    [<HttpGet("{id}")>]
    member __.Get(id: string) =
        "Hello"

    [<HttpPost("{id}")>]
    member ctrl.Post(id: string, [<FromHeader>]test: string, [<FromBody>]todo: Todo) =
        Console.WriteLine (sprintf "%A" ctrl.HttpContext.Request.Method)
        "Hello"