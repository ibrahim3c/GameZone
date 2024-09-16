using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext context;

        public CategoriesService(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return context.Categories.Select(c => new SelectListItem()
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).OrderBy(c=>c.Text).AsNoTracking().ToList();
        }
    }
}
