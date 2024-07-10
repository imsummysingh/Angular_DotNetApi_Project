using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implmentation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbcontext)
        {
            this.dbContext = dbcontext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            //return await dbContext.BlogPosts.ToListAsync();

            //for fetching categories too, EF provide us method which we can use to fetch all the related entities as well
            //this stmt says that: we also trying to fetch the categories with the blogposts
            return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }
    }
}
