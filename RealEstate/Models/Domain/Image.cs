namespace RealEstate.Models.Domain
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
