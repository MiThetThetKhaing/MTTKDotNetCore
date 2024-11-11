using MTTKDotNetCore.Domain.Features.Blog;

namespace MTTKDotNetCore.MinimalApi.Endpoints.Blog
{
    public static class BlogServiceEndpoint
    {
        public static void UseBlogEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", () =>
            {
                BlogService service = new BlogService();
                var lst = service.GetBlogs();
                return Results.Ok(lst);
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            app.MapGet("/blogs/{id}", (int id) =>
            {
                BlogService service = new BlogService();
                var item = service.GetBlog(id);
                if (item is null)
                {
                    return Results.BadRequest("No data found!");
                }
                return Results.Ok(item);
            })
            .WithName("GetBlog")
            .WithOpenApi();

            app.MapPost("/blogs", (TblBlog blog) =>
            {
                BlogService service = new BlogService();
                var result = service.CreateBlog(blog);

                return Results.Ok(result);
            })
            .WithName("CreateBlog")
            .WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                BlogService service = new BlogService();
                var item = service.UpdateBlog(id, blog);
                if (item is null)
                {
                    return Results.BadRequest("No data found!");
                }
                return Results.Ok(item);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();

            app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
            {
                BlogService service = new BlogService();
                var result = service.PatchBlog(id, blog);

                return Results.Ok(blog);
            })
            .WithName("PatchBlog")
            .WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                BlogService service = new BlogService();
                var item = service.DeleteBlog(id);

                return Results.Ok(item);
            })
            .WithName("DeleteBlog")
            .WithOpenApi();
        }
    }
}
