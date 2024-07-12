using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Books
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }

    public class CityViewModel
    {
        public string CityName { get; set; }
        public int CountryId { get; set; }
    }
}
