using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models.ClubViewModels
{
    public class ClubUserIndexViewModel
    {
        public List<ClubUser> ClubUsers { get; set; }
        public string CurrentUserId { get; set; }
    }
}
