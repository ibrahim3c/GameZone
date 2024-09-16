using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class Game:BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; }=string.Empty;
        public string Cover { get; set; }=string.Empty;

        [ForeignKey("Category")]
        public int? CatID {  get; set; }
        public Category? Category { get; set; }=default!;
        public ICollection<GameDevice> GameDevices { get; set;} = new List<GameDevice>();
    }
}
