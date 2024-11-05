using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace novSocial_media.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; } // for pk we use data annotations
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")] // data anotation for display label with space no need to define the type of input use asp-for
        [Range(1,100,ErrorMessage ="Dispaly Order Must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
