using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly AppDbContext context;

        public DevicesService(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> SelectListItems()
        {
            return context.Devices.Select(c => new SelectListItem()
            {
                Value = c.ID.ToString(),
                Text = c.Name
            })
                .OrderBy(c => c.Text).AsNoTracking()
                .ToList();
        }

    }
}
