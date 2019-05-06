using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Image { get; set; }

        public string Details { get; set; }

        public int ISBN { get; set; }

        [Required]
        public int ClubUserId { get; set; }

        public ClubUser ClubUser { get; set; }

        public bool CurrentlyReading { get; set; } = false;


    }
}
