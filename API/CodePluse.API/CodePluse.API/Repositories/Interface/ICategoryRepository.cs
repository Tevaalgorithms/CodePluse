using CodePluse.API.Models.Domain;

namespace CodePluse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
    }
}
