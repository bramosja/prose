using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        //Meeting frequency is meeting times per month
        public string MeetingFrequency { get; set; }

        [Required]
        public int UserId { get; set; }

        [NotMapped]
        public List<ApplicationUser> Users { get; set; }
    }
}
