using RealEstate.Models.Domain;

namespace RealEstate.Models.DTO
{
    public class StreetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WardDto ward { get; set; }
    }
}
