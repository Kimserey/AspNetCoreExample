namespace AspNetCoreExample

open Microsoft.AspNetCore.Mvc

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
    member __.Post(id: string, todo: Todo) =
        "Hello"