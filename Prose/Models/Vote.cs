using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models
{
    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        [Required]
        public int ClubUserId { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}
