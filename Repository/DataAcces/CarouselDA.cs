using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class CarouselDA(AppDbContext _dbContext) : ICarouselDA
    {
        public IQueryable<CarouselSlide> GetCarouselSlides()
        {
			return _dbContext.CarouselSlides.AsQueryable();
        }
    }
}