namespace AspNetCoreExample

open Microsoft.AspNetCore.Mvc
open System
open System.ComponentModel.DataAnnotations

type Todo =
    {
        [<Required>]
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
    member ctrl.Post(id: string, [<FromHeader>]content: string, [<FromQuery>]test: string, todo: Todo) =
        "Hello"