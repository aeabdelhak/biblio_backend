using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.Helpers;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Business.Services
{
    public class CarouselService(DocumentHelpers _doc, ICommonDA _commonDA, ICarouselDA _carouselDA)
        : ICarouselService
    {
        public IQueryable<CarouselSlide> GetCarouselSlides()
        {
            return _carouselDA
                .GetCarouselSlides()
                .Include(e => e.Image)
                .OrderByDescending(e => e.CreatedAt);
        }

        public async Task<CarouselSlide> NewCarouselSlide(IFile document)
        {
            var item = new CarouselSlide { ImageId = (await _doc.uploadAndSave(document))!.Id };
            await _commonDA.DbInsert(item);
            return item;
        }

        public async Task<BaseReturnEnum> RemoveCarouselSlide(Guid id)
        {
            var item = await _carouselDA.GetCarouselSlides().SingleOrDefaultAsync(e => e.Id == id);
            if (item is null)
                return BaseReturnEnum.NotFound;
            await _commonDA.DbDelete(item);
            return BaseReturnEnum.Success;
        }
    }
}
