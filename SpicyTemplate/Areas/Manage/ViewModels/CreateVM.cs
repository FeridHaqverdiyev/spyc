using SpicyTemplate.Models;

namespace SpicyTemplate.Areas.Manage.ViewModels
{
    public class CreateVM
    {
        public IFormFile Image { get; set; }
        public  string Name{ get; set; }
        public  decimal Price { get; set; }
        public string Description { get; set; }
        public List<Category> categories { get; set; }
        public int CategoryId { get; set; }
    }
}
