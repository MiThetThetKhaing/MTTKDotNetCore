using Newtonsoft.Json;

namespace MTTKDotNetCore.SnakeMinimalApi.Endpoints.Snake
{
    public static class SnakeEndpoint
    {
        public static void UseSnakeEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/snakes", () =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<SnakeModel[]>(jsonStr)!;
                //var result = JsonConvert.DeserializeObject<SnakeResponseModel>(jsonStr);

                return Results.Ok(result);
            })
            .WithName("GetSnakes")
            .WithOpenApi();

            app.MapGet("/snake/{id}", (int id) =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<SnakeModel[]>(jsonStr)!;

                var item = result.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data Found!");

                return Results.Ok(item);
            })
            .WithName("GetSnake")
            .WithOpenApi();

            app.MapPost("/snakes", (SnakeModel snake) =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var snakeObj = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr)!;

                var obj = new SnakeModel
                {
                    Id = snake.Id,
                    MMName = snake.MMName,
                    EngName = snake.EngName,
                    Detail = snake.Detail,
                    IsPoison = snake.IsPoison,
                    IsDanger = snake.IsDanger
                };
                if (obj.Id == 0)
                {
                    obj.Id = snakeObj.Count + 1;
                }
                snakeObj.Add(obj);
                var snakes = JsonConvert.SerializeObject(snakeObj, Formatting.Indented);
                File.WriteAllText(folderPath, snakes);

                return Results.Ok(snakeObj);
            })
            .WithName("CreateSnake")
            .WithOpenApi();

            app.MapPut("/snakes/{id}", (int id, SnakeModel snake) =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr)!;

                var item = result.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data Found!");

                result[id - 1] = new SnakeModel
                {
                    Id = id,
                    MMName = snake.MMName,
                    EngName = snake.EngName,
                    Detail = snake.Detail,
                    IsDanger = snake.IsDanger,
                    IsPoison = snake.IsPoison
                };

                var final = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, final);

                return Results.Ok(final);
            })
            .WithName("UpdateSnake")
            .WithOpenApi();

            app.MapPatch("/snakes/{id}", (int id, SnakeModel snake) =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr)!;

                var item = result.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data Found!");

                result[id - 1] = new SnakeModel
                {
                    Id = id,
                    MMName = !string.IsNullOrEmpty(snake.MMName) ? snake.MMName : result[id - 1].MMName,
                    EngName = !string.IsNullOrEmpty(snake.EngName) ? snake.EngName : result[id - 1].EngName,
                    Detail = !string.IsNullOrEmpty(snake.Detail) ? snake.Detail : result[id - 1].Detail,
                    IsPoison = !string.IsNullOrEmpty(snake.IsPoison) ? snake.IsPoison : result[id - 1].IsPoison,
                    IsDanger = !string.IsNullOrEmpty(snake.IsDanger) ? snake.IsDanger : result[id - 1].IsDanger,
                };

                var final = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, final);

                return Results.Ok(final);
            })
            .WithName("PatchSnake")
            .WithOpenApi();

            app.MapDelete("/snakes/{id}", (int id) =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr)!;

                var item = result.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data is found!");

                result.Remove(item);
                var snakes = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, snakes);

                return Results.Ok(snakes);
            })
            .WithName("DeleteSnake")
            .WithOpenApi();
        }
    }
}
