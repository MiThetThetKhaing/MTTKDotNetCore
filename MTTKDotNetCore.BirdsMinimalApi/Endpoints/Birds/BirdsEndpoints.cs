using Newtonsoft.Json;

namespace MTTKDotNetCore.BirdsMinimalApi.Endpoints.Birds
{
    public static class BirdsEndpoints
    {
        public static void UseBirdsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/birds", () =>
            {
                string folderPath = "Data/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                return Results.Ok(result.Tbl_Bird);
            })
            .WithName("GetBirds")
            .WithOpenApi();

            app.MapGet("/birds/{id}", (int id) =>
            {
                string folderPath = "Data/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No data found!");

                return Results.Ok(item);
            })
            .WithName("GetBird")
            .WithOpenApi();

            app.MapPost("/birds", (BirdModel bird) =>
            {
                string folderPath = "Data/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var birdObj = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                bird.Id = birdObj.Tbl_Bird.Count == 0 ? 1 : birdObj.Tbl_Bird.Max(x => x.Id) + 1;
                birdObj.Tbl_Bird.Add(bird);

                string final = JsonConvert.SerializeObject(birdObj, Formatting.Indented);
                File.WriteAllText(folderPath, final);

                return Results.Ok("Successfully created...");
            })
            .WithName("CreateBird")
            .WithOpenApi();

            app.MapPut("/birds/{id}", (int id, BirdModel bird) =>
            {
                string folderPath = "Data/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data found!");

                item.BirdMyanmarName = bird.BirdMyanmarName;
                item.BirdEnglishName = bird.BirdEnglishName;
                item.Description = bird.Description;
                item.ImagePath = bird.ImagePath;

                result.Tbl_Bird.Any(x => x.Id == id);

                string objTojson = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, objTojson);

                return Results.Ok("Edit Successfully..");
            })
            .WithName("EditBird")
            .WithOpenApi();

            app.MapPatch("/birds/{id}", (int id, BirdModel bird) =>
            {
                string folderPath = "Data/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data found!");

                if (bird.BirdMyanmarName != null)
                {
                    item.BirdMyanmarName = bird.BirdMyanmarName;
                }
                if (bird.BirdEnglishName != null)
                {
                    item.BirdEnglishName = bird.BirdEnglishName;
                }
                if (bird.Description != null)
                {
                    item.Description = bird.Description;
                }
                if (bird.ImagePath != null)    
                {
                    item.ImagePath = bird.ImagePath;
                }

                result.Tbl_Bird.Any(x => x.Id == id);

                string objTojson = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, objTojson);

                return Results.Ok("Edit Successfully..");
            })
            .WithName("PatchBird")
            .WithOpenApi();

            app.MapDelete("/birds/{id}", (int id) =>
            {
                string folderPath = "Data/Birds.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

                var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data Found!");

                result.Tbl_Bird.Remove(item);

                var ObjToJson = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText(folderPath, ObjToJson);

                return Results.Ok("Successfully Deleted!");
            })
            .WithName("DeleteBird")
            .WithOpenApi();
        }
    }
}
