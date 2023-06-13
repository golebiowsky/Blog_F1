using Blog_F1.Models.Domain;

namespace Blog_F1.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts{ get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
