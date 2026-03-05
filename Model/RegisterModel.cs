namespace ThrendyThreads.Model
{
    public class RegisterModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public byte[]? Image { get; set; }   // fix

        public string Role { get; set; }
    }
}