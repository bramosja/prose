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

        [Required]
        public int ClubUserId { get; set; }

        public ClubUser ClubUser { get; set; }

        //to indicate if the book is currently being read
        public bool CurrentlyReading { get; set; } = false;

        //to indicate if a book has already been read
        public bool PastRead { get; set; } = false;

        public int Rank { get; set; }

    }
}
