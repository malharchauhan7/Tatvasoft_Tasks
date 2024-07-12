using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Books
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
        public ICollection<City> Cities { get; set; }
    }

    public class CountryViewModel
    {
        public string CountryName { get; set; }
    }
}
