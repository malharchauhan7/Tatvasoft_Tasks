using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Books
{
    public class MissionApplication
    {
        [Key]
        public int Id { get; set; }
        public int MissionId { get; set; }
        [NotMapped]
        public string? MissionTitle { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public string? UserName { get; set; }
        [NotMapped]
        public string? UserImage { get; set; }
        [Column(TypeName = "date")]
        public DateTime AppliedDate { get; set; }
        public bool Status { get; set; }
        public int Sheet { get; set; }
    }
}
