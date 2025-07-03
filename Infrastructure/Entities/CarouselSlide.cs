using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
    public class CarouselSlide :BaseEntity
    {
        public Document Image { get; set; }
        public Guid ImageId { get; set; }
    }
}