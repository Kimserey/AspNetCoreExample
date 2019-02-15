namespace AspNetCoreExample

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Mvc
open Swashbuckle.AspNetCore.Swagger
open Microsoft.Extensions.Configuration
open System.Data.SQLite
open Polly
open System.Net.Http

type Startup(configuration: IConfiguration) =

    member this.ConfigureServices(services: IServiceCollection) =
        let policy (p: PolicyBuilder<HttpResponseMessage>)  =
            p.WaitAndRetryAsync(3, fun _ -> TimeSpan.FromMilliseconds(600.)) :> IAsyncPolicy<HttpResponseMessage>

        services
            .AddHttpClient<DummyService>()
            .AddTransientHttpErrorPolicy(new Func<_, _>(policy))
        |> ignore

        services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        |> ignore

        services.AddSwaggerGen(fun c ->
            c.SwaggerDoc("v1", Info(Title = "My API", Version = "v1"))
            |> ignore)
        |> ignore

        services.AddTransient<SQLiteConnection>(fun sp ->
            new SQLiteConnection("Data Source=data.db")
        )
        |> ignore


    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =

        app.UseSwagger()
        |> ignore

        app.UseSwaggerUI(fun c ->
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")
            |> ignore
        ) |> ignore

        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseMvc()
        |> ignore