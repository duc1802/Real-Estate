namespace RealEstate.Models.Domain
{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WardId { get; set; }

        public Ward Ward { get; set; }
    }
}
