using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implmentation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
        {
            //as data is coming
            //covert dto to domain model
            //creating a new domain model which we will map with the dto incoming values(request)
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                Author = request.Author
            };

            blogPost = await blogPostRepository.CreateAsync(blogPost); //will return the updated object

            //convert domain to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author
            };

            return Ok (response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPosts=await blogPostRepository.GetAllAsync();

            //convert domain model to dto
            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author
                });
            }
            return Ok (response);

        }
    }
}
