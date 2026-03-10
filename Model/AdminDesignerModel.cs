namespace ThrendyThreads.Model
{
    public class AdminDesignerModel
    {
        // Registration
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte[]? Image { get; set; }

        // Designer
        public string DesignerName { get; set; }
        public byte[]? DesignerImage { get; set; }
        public string AboutDesigner { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}