using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Common.Output
{
	public enum BaseReturnEnum
	{
		Success,
		WrongData,
		NotFound,
		Error,
		Conflict,
		NotAcceptable,
		Unauthorized,
		Deleted
	}
}