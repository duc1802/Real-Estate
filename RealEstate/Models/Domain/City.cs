namespace RealEstate.Models.Domain
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NationId { get; set; }

        public Nation Nation { get; set; }

    }
}
