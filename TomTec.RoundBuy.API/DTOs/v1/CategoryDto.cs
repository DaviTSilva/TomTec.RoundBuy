using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class CategoryDto
    {
        public string Name { get; set; }

        public Category ToModel()
        {
            return new Category()
            {
                Name = this.Name,
            };
        }
    }
}
