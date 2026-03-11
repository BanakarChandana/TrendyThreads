namespace ThrendyThreads.Model
{
    public class OutfitChangeRequestModel
    {
        public int RequestId { get; set; }
        public string YourName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public int ProductId { get; set; }
        public int DesignerId { get; set; }
    }
}