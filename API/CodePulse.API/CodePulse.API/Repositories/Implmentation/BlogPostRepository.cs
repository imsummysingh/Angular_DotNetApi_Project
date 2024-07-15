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

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost !=null)
            {
                dbContext.BlogPosts.Remove(existingBlogPost);
                await dbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            //return await dbContext.BlogPosts.ToListAsync();

            //for fetching categories too, EF provide us method which we can use to fetch all the related entities as well
            //this stmt says that: we also trying to fetch the categories with the blogposts
            return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlogPost=await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if(existingBlogPost == null)
            {
                return null;
            }

            //update blogpost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            //update categories
            existingBlogPost.Categories = blogPost.Categories;

            await dbContext.SaveChangesAsync();

            return blogPost;
        }
    }
}
