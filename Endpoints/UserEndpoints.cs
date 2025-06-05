using LearningProjectASP.Dto;
using LearningProjectASP.Models;
using LearningProjectASP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LearningProjectASP.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users");

            group.MapPost("/register", RegisterUser);
            group.MapPost("/login", LoginUser);

            group.MapGet("/{id}", GetUserById).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
            group.MapPut("/{id}", UpdateUser).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
            group.MapDelete("/{id}", DeleteUser).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
        }

        private static async Task<IResult> RegisterUser(AppUserDto userDto, UserManager<AppUser> userManager)
        {
            var user = new AppUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };

            var result = await userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                return Results.BadRequest(result.Errors);
            }

            await userManager.AddToRoleAsync(user, "User");

            return Results.Ok("User created");
        }


        private static async Task<IResult> LoginUser(
                LoginRequestDto loginDto,
                UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager,
                ITokenService jwtService)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Results.Unauthorized();
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Results.Unauthorized();
            }

            var token = await jwtService.GenerateTokenAsync(user);
            return Results.Ok(new { Token = token });
        }

        private static async Task<IResult> GetUserById(string id, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByIdAsync(id);
            return user != null ? Results.Ok(user) : Results.NotFound();
        }

        private static async Task<IResult> UpdateUser(string id, AppUserDto updatedDto, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            user.FirstName = updatedDto.FirstName;
            user.LastName = updatedDto.LastName;
            user.Email = updatedDto.Email;
            user.UserName = updatedDto.Email;

            var result = await userManager.UpdateAsync(user);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        }
        private static async Task<IResult> DeleteUser(string id, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            var result = await userManager.DeleteAsync(user);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        }

    }

}