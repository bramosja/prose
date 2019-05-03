using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models
{
    public class ClubUser
    {
        [Key]
        public int ClubUserId { get; set; }
        
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int ClubId { get; set; }

        public Club Club { get; set; }
    }
}
