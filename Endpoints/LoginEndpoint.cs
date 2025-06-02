using LearningProjectASP.Data;
using LearningProjectASP.Services;
using Microsoft.EntityFrameworkCore;
using static LearningProjectASP.Dto.LoginRequestDto;

namespace LearningProjectASP.Endpoints
{
    public static class LoginEndpoints
    {
        public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api");

            group.MapPost("/login", async (
                LoginRequest request,
                AppDbContext db,
                ITokenService tokenService) =>
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
                if (user is null)
                {
                    return Results.Unauthorized();
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    return Results.Unauthorized();
                }

                var token = tokenService.CreateToken(user);
                return Results.Ok(new { token });
            });
        }
    }
}
