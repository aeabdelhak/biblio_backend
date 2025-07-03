using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
	public class CarouselMutation(ICarouselService _service)
	{
		public Task<CarouselSlide> NewCarouselSlide(IFile document)=>_service.NewCarouselSlide(document);
		public Task<BaseReturnEnum> RemoveCarouselSlide(Guid id)=>_service.RemoveCarouselSlide(id);
		
	}
}