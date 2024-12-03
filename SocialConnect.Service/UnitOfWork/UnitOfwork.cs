
using SocialConnect.Core.Models;
using SocialConnect.Repository.Data;
using SocialConnect.Service;

namespace SocialConnect.Service
{
    public class UnitOfwork
    {
        ApplicationDbContext db;
        GenericRepository<Post> Postrepository;
       
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
