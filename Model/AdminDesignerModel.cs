using System;

namespace ThrendyThreads.Model
{
    public class AdminDesignerModel
    {
        // --------------------------------
        // Registration Details
        // --------------------------------
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte[]? Image { get; set; }

        // --------------------------------
        // Designer Details
        // --------------------------------
        public string DesignerName { get; set; }
        public string AboutDesigner { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}