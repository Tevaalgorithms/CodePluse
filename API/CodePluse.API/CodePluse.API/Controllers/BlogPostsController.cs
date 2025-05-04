using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
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
        // post: {apibase url}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
        {
            // convert DTO to Domain
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Title = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle
            };

            blogPost = await blogPostRepository.CreateAsync(blogPost);

            // Convert the Domain model back to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle
            };

            return Ok(response);
        }

        // GET: {apibase url}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetBlogPosts()
        {
           var blogPost = await blogPostRepository.GetAllAsync();
            // Never expose the Domain models
            // Map Domain model to DTO
            var response = new List<BlogPostDto>();
            foreach (var post in blogPost)
            {
                response.Add(new BlogPostDto
                {
                    Id = post.Id,
                    Author = post.Author,
                    Content = post.Content,
                    FeaturedImageUrl = post.FeaturedImageUrl,
                    IsVisible = post.IsVisible,
                    PublishedDate = post.PublishedDate,
                    ShortDescription = post.ShortDescription,
                    Title = post.Title,
                    UrlHandle = post.UrlHandle
                });
            }
            return Ok(response);
        }

    }
}
