namespace AspNetCoreExample

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Mvc
open Swashbuckle.AspNetCore.Swagger
open Microsoft.Extensions.Configuration

type Startup(configuration: IConfiguration) =

    member this.ConfigureServices(services: IServiceCollection) =

        services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            |> ignore

        services.AddSwaggerGen(fun c ->
            c.SwaggerDoc("v1", Info(Title = "My API", Version = "v1"))
            |> ignore)
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