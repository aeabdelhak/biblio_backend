using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Common.Input
{
    public class SellingDetailInput
    {
		public int Quantity { get; set; }
		public Guid BookId { get; set; }
        public Guid? OrderId { get; set; }
    }
}