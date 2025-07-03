using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Common.Output
{
    public class GenericResponse<S,T>
    {
        public S Status { get; set; }
        public T? Data { get; set; }
    }
}