using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models.BookViewModels
{
    public class BooksIndexViewModel
    {
        public Book Book { get; set; }

        public string OwnerId { get; set; }

        public string CurrentUserId { get; set; }

        public int? VoteTotal { get; set; }
    }
}
