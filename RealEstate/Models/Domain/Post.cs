namespace RealEstate.Models.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Acreage { get; set; }
        public int Bedroom { get; set; }
        public int Toilet { get; set; }
        public int Floor { get; set; }
        public decimal Price { get; set; }


        public ICollection<Image> Images { get; set; }

    }
}
