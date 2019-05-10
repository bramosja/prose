using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prose.Models.BookViewModels
{
    public class BooksIndexViewModel
    {
        public Book Book { get; set; }

        public int? VoteTotal { get; set; }
    }
}
