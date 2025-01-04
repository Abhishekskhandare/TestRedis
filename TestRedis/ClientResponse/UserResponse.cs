namespace TestRedis.ClientResponse
{
    public class UserResponse
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? Gender { get; set; }
        public string DateOfBirth { get; set; }
    }
}
