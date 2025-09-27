using System.ComponentModel.DataAnnotations;

namespace ZXManager.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}