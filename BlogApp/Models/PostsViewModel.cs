using BlogApp.Entity;

namespace BlogApp.Models
{
    class PostViewModel
    {
        public List<Post> Posts { get; set; } = new();
    }
}