using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
    public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> builder)
        {
            builder.HasKey(e => e.Id);
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.BookReviews)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(e => e.Book)
                .WithMany(e => e.BookReviews)
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
