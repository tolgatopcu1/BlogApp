using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comment {get;}

        void CreateComment (Comment comment);
    }
}