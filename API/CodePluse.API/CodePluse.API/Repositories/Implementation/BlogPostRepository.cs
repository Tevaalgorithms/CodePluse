using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDBContext dBContext;

        public BlogPostRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dBContext.BlogPosts.AddAsync(blogPost);
            await dBContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dBContext.BlogPosts.ToListAsync();
        }
    }
}
