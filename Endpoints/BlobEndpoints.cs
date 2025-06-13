using LearningProjectASP.Models;
using LearningProjectASP.Services;
using LearningProjectASP.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LearningProjectASP.Endpoints
{
    public static class BlobEndpoints
    {
        public static void MapBlobEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/blobs");

            group.MapPost("/", UploadBlob).Accepts<BlobUploadDto>("multipart/form-data")
     .Produces(StatusCodes.Status201Created)
     .Produces(StatusCodes.Status400BadRequest);
            group.MapGet("/", GetAllBlobs);
            group.MapGet("/{id}", GetBlobById);
            group.MapDelete("/{id}", DeleteBlob);
        }

        private static async Task<IResult> UploadBlob(
            HttpRequest request,
            IBlobService blobService,
            [FromForm] BlobUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                return Results.BadRequest("Empty file");
            }

            using var stream = new MemoryStream();
            await dto.File.CopyToAsync(stream);

            var blob = new BlobFile
            {
                Name = dto.Name,
                MetadataJson = dto.MetadataJson,
                Data = stream.ToArray()
            };

            var result = await blobService.UploadAsync(blob);
            return Results.Created($"/api/blobs/{result.Id}", new { result.Id });
        }

        private static async Task<IResult> GetAllBlobs(IBlobService blobService)
        {
            var blobs = await blobService.GetAllAsync();
            var dtos = blobs.Select(b => new BlobDto
            {
                Id = b.Id,
                Name = b.Name,
                Created = b.Created,
                Updated = b.Updated,
                MetadataJson = b.MetadataJson
            });

            return Results.Ok(dtos);
        }

        private static async Task<IResult> GetBlobById(int id, IBlobService blobService)
        {
            var blob = await blobService.GetAsync(id);
            if (blob == null)
            {
                return Results.NotFound();
            }

            return Results.File(blob.Data, "application/octet-stream", blob.Name);
        }

        private static async Task<IResult> DeleteBlob(int id, IBlobService blobService)
        {
            var deleted = await blobService.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        }
    }
}
