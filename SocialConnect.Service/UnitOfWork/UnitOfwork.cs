
using SocialConnect.Core.Models;
using SocialConnect.Repository.Data;
using SocialConnect.Service;
using SocialConnect.Service.UnitOfWork;

namespace SocialConnect.Service
{
    public class UnitOfwork
    {
        ApplicationDbContext db;
        GenericRepository<Post> Postrepository;
        CommentRepository Comment;
       
        GenericRepository<Comment> Commentrepository;
        GenericRepository<CommentReact> CommentReactsrepository;
        GenericRepository<postReacts> PostReactsrepository;


        public UnitOfwork(ApplicationDbContext db )
        {
            this.db = db;
        }
        public GenericRepository<Post> postrepository
        {
            get
            {
                if (Postrepository == null)
                {
                    Postrepository = new GenericRepository<Post>(db);
                }
                return Postrepository;
            }
        }
        public CommentRepository comment
        {
            get
            {
                if (Comment == null)
                {
                    Comment = new CommentRepository(db);
                }
                return Comment;
            }
        }
        public GenericRepository<postReacts> postReactsrepository
        {
            get
            {
                if (Postrepository == null)
                {
                    PostReactsrepository = new GenericRepository<postReacts>(db);
                }
                return PostReactsrepository;
            }
        }
        public GenericRepository<CommentReact> commentReactsrepository
        {
            get
            {
                if (Postrepository == null)
                {
                    CommentReactsrepository = new GenericRepository<CommentReact>(db);
                }
                return CommentReactsrepository;
            }
        }
        public GenericRepository<Comment> commentrepository
        {
            get
            {
                if (Commentrepository == null)
                {
                    Commentrepository = new GenericRepository<Comment>(db);
                }
                return Commentrepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

    }
}
