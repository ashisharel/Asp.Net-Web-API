using System.ComponentModel.DataAnnotations;

namespace LibertyWebAPI.DTO.Product
{
    //[AtLeastOneProperty("CategoryId", "CatalogId", ErrorMessage = "You must supply at least one value")]
    public class CategoryRequestDTO
    {
        [Required]
        public int? CategoryId { get; set; }

        public int? MaxCount { get; set; }
    }
}