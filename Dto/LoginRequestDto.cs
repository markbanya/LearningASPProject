namespace LearningProjectASP.Dto
{
    public record LoginRequestDto
    {
        public record LoginRequest(string Username, string Password);
    }
}
