using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Common.Input
{
    public class BookOptionInput
    {
        public decimal Price { get; set; }
		public bool IsActive { get; set; } = true;
    }
}