using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class EditGameVM
    {
        public int ID { get; set; } = default!;
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [AllowedExtenstion(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInMB)]
        public IFormFile? Cover { get; set; } = default!;
        public string? path { get; set; } = default!;

        [Display(Name = "Category")]
        [Required]
        public int? CatID { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> GameDevicesID { get; set; } = default!;

        public IEnumerable<SelectListItem> GameDevices { get; set; } = Enumerable.Empty<SelectListItem>();


    }
}
