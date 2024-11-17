using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
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

        public async Task<IActionResult> Details(int id)
        {


            var post = await _postRepository.Posts
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
            {
                return NotFound("Belirtilen post bulunamadı.");
            }

            return View(post);
        }
        [HttpPost]
        public async Task<JsonResult> AddComment(int PostId,string UserName,string Text)
        {
            var comment = new Comment{
                Text=Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = new User{UserName = UserName, Image="p1.jpg"}
            };
            _commentRepository.CreateComment(comment);

            return Json(new {
                UserName,
                Text,
                comment.PublishedOn,
                comment.User.Image
            });

        }
    }
}
