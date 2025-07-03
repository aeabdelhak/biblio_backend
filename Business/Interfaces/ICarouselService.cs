using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
	public interface ICarouselService
	{
		public IQueryable<CarouselSlide> GetCarouselSlides();
		public Task<CarouselSlide> NewCarouselSlide(IFile document);
		public Task<BaseReturnEnum> RemoveCarouselSlide(Guid id);
		
	}
}