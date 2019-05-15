using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models.ClubViewModels
{
    public class ClubIndexViewModel
    {
        public List<Club> Clubs { get; set; }

        public string CurrentUserId { get; set; }
        
    }
}
