namespace Blog_F1.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Naglowek { get; set; }

        public string StronaTytul { get; set; }

        public string Zawartosc { get; set; }

        public string KrotkiOpis { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }

        public DateTime DataPublikacji { get; set; }

        public string Autor { get; set; }
        public bool Widocznosc { get; set; }

        public ICollection<Tag> Tags  { get; set; }
    }
}
