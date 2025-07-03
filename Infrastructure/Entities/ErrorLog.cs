using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
    public class ErrorLog : BaseEntity
    {
        public string? ReqParams { get; set; }
        public string? ReqBody { get; set; }
        public string Message { get; set; }
    }
}