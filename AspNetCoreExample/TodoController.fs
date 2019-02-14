namespace AspNetCoreExample

open Microsoft.AspNetCore.Mvc
open System
open System.ComponentModel.DataAnnotations
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Options
open Microsoft.Extensions.Logging
open System.Data.SQLite
open Dapper
open System.Collections.Generic

[<AutoOpen>]
module Dynamic =

    open System.Dynamic

    let pack (fields: #seq<string * obj>) =
        let expando = ExpandoObject()
        let expandoDictionary = expando :> IDictionary<string,obj>

        for (key, value) in fields do
            expandoDictionary.Add(key, value)

        expando

    [<AutoOpen>]
    module Operators =

        let (.=) (key: string) (value: 'a) =
            (key, box value)

        let (!@) fields = pack fields

type Todo =
    {
        [<Key>]
        id: string
        [<Required>]
        title: string
        [<Required>]
        content: string
    }

[<ApiController>]
[<Route("/api/todos")>]
type TodoController(logger: ILogger<TodoController>, conn: SQLiteConnection) =
    inherit ControllerBase()

    [<HttpGet>]
    member __.GetAll() =
        conn.QueryAsync("""
            SELECT *
            FROM [todos]
        """)

    [<HttpGet("{id}")>]
    member __.Get(id: string) =
        conn.QueryAsync(
            """SELECT *
               FROM [todos] as td
               WHERE td.[id] = @id""",
               !@ [
                "id" .= id
                "title" .= "hello"
                ])

    [<HttpPost>]
    member ctrl.Post(todo: Todo) =
        conn.ExecuteAsync(
            """INSERT INTO [todos]
                           ([id]
                           ,[title]
                           ,[content])
                VALUES (@id
                       ,@title
                       ,@content)""",
            box todo)