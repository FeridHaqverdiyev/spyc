namespace SpicyTemplate.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OurMenu> OurMenus { get; set; }
    }
}

