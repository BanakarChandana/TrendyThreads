using System;

namespace ThrendyThreads.Models
{
    public class ContactModel
    {
        public int ContactId { get; set; }

        public string YourName { get; set; }

        public string EmailAddress { get; set; }

        public string ReasonForContact { get; set; }

        public string Subject { get; set; }

        public string YourMessage { get; set; }
    }
}