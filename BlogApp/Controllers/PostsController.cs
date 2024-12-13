using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private BlogContext _blogContext;

        private ITagRepository _tagRepository;
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository,ITagRepository tagRepository,BlogContext blogContext)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
            _blogContext = blogContext;
        }

        public async Task<IActionResult> Index(string url)
        {
            var claims = User.Claims;
            var posts = _postRepository.Posts.Where(i=>i.IsActive);
            
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
                .Include(x=>x.User)
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
        public async Task<JsonResult> AddComment(int PostId,string Text)
        {
            var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username =User.FindFirstValue(ClaimTypes.Name);
            var avatar =User.FindFirstValue(ClaimTypes.UserData);

            var comment = new Comment{
                PostId = PostId,
                Text=Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(comment);

            return Json(new {
                username,
                Text,
                comment.PublishedOn,
                avatar
            });

        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);

                _postRepository.CreatePost(
                    new Post{
                        Title = model.Title,
                        Content = model.Content,
                        Description=model.Description,
                        Url = model.Url,
                        UserId = int.Parse(userId?? ""),
                        PublishedOn = DateTime.Now,
                        Image = "1.jpg",
                        IsActive = false
                    }   
                );
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId =int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ??"");
            var role =User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role))
            {
                posts = posts.Where(p=>p.UserId == userId);
            }
            return View(await posts.ToListAsync());
        }
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var post = _postRepository.Posts.Include(i=>i.Tags).FirstOrDefault(i=>i.PostId==id);
            if (post==null)
            {
                return NotFound();
            }

            ViewBag.Tags=_tagRepository.Tags.ToList();

            return View(new PostCreateViewModel{
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Tags=post.Tags
            });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model, int[] tagIds)
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = new Post{
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                };

                if(User.FindFirstValue(ClaimTypes.Role)=="admin")
                {
                    entityToUpdate.IsActive = model.IsActive;
                }
                _postRepository.EditPost(entityToUpdate,tagIds);
                return RedirectToAction("List");
            }
            ViewBag.Tags=_tagRepository.Tags.ToList();
            return View(model);
        }
        [Authorize]
        public IActionResult Delete(int id)
        {   var deletedPost = _postRepository.Posts.FirstOrDefault(p=>p.PostId==id);
            _blogContext.Posts.Remove(deletedPost);
            _blogContext.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
