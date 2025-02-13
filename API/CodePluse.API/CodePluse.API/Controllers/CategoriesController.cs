using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);

            // Domain model to DTO
            var response = new CategoryDto
            {
                id = category.id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
        // GET: https://localhost:7075/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
           var categories = await categoryRepository.GetAllAsync();
            // Never expose the Domain models
            // Map Domain model to DTO
            var response = new List<CategoryDto>();
            foreach (var category in categories) {
                response.Add(new CategoryDto
                {
                    id=category.id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            return Ok(response);    
        }

        // GET: https://localhost:7075/api/Categories/{id}
        [HttpGet]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
        {
            var existingcategory = await categoryRepository.GetByIdAsync(categoryId);

            if(existingcategory == null)
            {
                return NotFound();
            }

            // Map Domain model to DTO
            var response = new CategoryDto
            {
                id = existingcategory.id,
                Name = existingcategory.Name,
                UrlHandle = existingcategory.UrlHandle
            };

            return Ok(response);
        }

        //PUT: https://localhost:7075/api/Categories/{id}
        [HttpPut]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, UpdateCategoryRequestDto request)
        {
            // Convery the request/(AKA DTO) to Domain Model
            var category = new Category
            {
                id = categoryId,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            category = await categoryRepository.UpdateAsync(category);

            if(category == null)
            {
                return NotFound();
            }

            // Map Domain model to DTO
            var response = new CategoryDto
            {
                id = category.id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

        }

        // DELETE: https://localhost:7075/api/Categories/{id}
        [HttpDelete]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            var category = await categoryRepository.DeleteAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO
            var response = new CategoryDto
            {
                id = category.id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }


    }
}
