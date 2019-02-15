namespace AspNetCoreExample

open System.Net.Http

type DummyService(client: HttpClient) =
    member __.Get() =
        async {
            let! response = client.GetAsync("http://localhost:5500/api") |> Async.AwaitTask
            let! result = response.Content.ReadAsStringAsync() |> Async.AwaitTask
            return result
        }