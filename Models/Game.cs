using System.ComponentModel.DataAnnotations;

namespace ZXManager.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public int Year { get; set; }

        [Display(Name = "Condition")]
        public string Condition { get; set; }

        [Range(1, 5)]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}