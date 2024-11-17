using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index(string url)
        {
            var posts = _postRepository.Posts;
            
            if (!String.IsNullOrEmpty(url))
            {
                posts = posts.Where(p=>p.Tags.Any(t=>t.Url==url));
            }

            return View(
                new PostViewModel
                {
                    Posts =await posts.ToListAsync(),    
                }
            );
        }

        public async Task<IActionResult> Details(string url)
        {
            var post = await _postRepository.Posts.Include(x=>x.Tags).Include(x=>x.Comments).ThenInclude(x=>x.User).FirstOrDefaultAsync(p=>p.Url==url);
            

            return View(
                new PostDetailsViewModel{
                    Post = post
                }
            );
        }
    }
}
