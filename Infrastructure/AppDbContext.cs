using BiblioPfe.Common;
using BiblioPfe.Infrastructure.Configurations;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq.Expressions;

namespace BiblioPfe.Infrastructure
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{

		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookAuthor> BookAuthors { get; set; }
		public DbSet<BookCategory> BookCategories { get; set; }
		public DbSet<BookOrderOption> BookOrderOptions { get; set; }
		public DbSet<Document> Documents { get; set; }
		public DbSet<AuthorNationality> AuthorNationalities { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
		public DbSet<ErrorLog> ErrorLogs { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderStatus> OrderStatuses { get; set; }
		public DbSet<Rules> Rules { get; set; }
		public DbSet<SeoData> SeoDatas { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<CarouselSlide> CarouselSlides { get; set; }
		public DbSet<BookReview> BookReviews { get; set; }
		public DbSet<CustomClient> CustomClient { get; set; }
		public override int SaveChanges()
		{
			var deletedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is BaseEntity && x.State == EntityState.Deleted);

			foreach (var entry in deletedEntries)
			{
				((BaseEntity)entry.Entity).DeletedAt = DateTime.Now;
				entry.State = EntityState.Modified;
			}

			return base.SaveChanges();
		}
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var deletedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is BaseEntity && x.State == EntityState.Deleted);

			foreach (var entry in deletedEntries)
			{
				((BaseEntity)entry.Entity).DeletedAt = DateTime.Now;
				entry.State = EntityState.Modified;
			}

			return base.SaveChangesAsync(cancellationToken);
		}
		public void DetachAllEntities()
		{
			var undetachedEntriesCopy = this.ChangeTracker.Entries()
				.Where(e => e.State != EntityState.Detached)
				.ToList();

			foreach (var entry in undetachedEntriesCopy)
				entry.State = EntityState.Detached;
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AuthorConfiguration());
			modelBuilder.ApplyConfiguration(new AuthorNationalityConfiguration());
			modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
			modelBuilder.ApplyConfiguration(new BookCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new BookConfiguration());
			modelBuilder.ApplyConfiguration(new BookOrderOptionConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new CountryConfiguration());
			modelBuilder.ApplyConfiguration(new DeliveryAddressConfiguration());
			modelBuilder.ApplyConfiguration(new DocumentConfiguration());
			modelBuilder.ApplyConfiguration(new ErrorLogConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
			modelBuilder.ApplyConfiguration(new RulesConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new SeoDataConfiguration());
			modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
			modelBuilder.ApplyConfiguration(new CarouselSlideConfiguration());
			modelBuilder.ApplyConfiguration(new BookReviewConfiguration());
			modelBuilder.ApplyConfiguration(new CustomClientConfiguration());


			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				var isDeletedProperty = entityType.FindProperty("DeletedAt");
				if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(DateTime?))
				{
					var parameter = Expression.Parameter(entityType.ClrType, "e");
					var filter = Expression.Lambda(Expression.Equal(Expression.Property(parameter, isDeletedProperty.PropertyInfo), Expression.Constant(null, typeof(DateTime?))), parameter);
					entityType.SetQueryFilter(filter);
				}
			}



			SuperUserSeed.Seed(modelBuilder);
			PlatfromRulesSeed.Seed(modelBuilder);

			base.OnModelCreating(modelBuilder);

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseMySQL(Constants.dbconn, x =>
				{
					x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
				});
			}
		}
		public class DesignedAppDbContext : IDesignTimeDbContextFactory<AppDbContext>
		{
			public AppDbContext CreateDbContext(string[] args)
			{
				var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
				optionsBuilder.UseMySQL(Constants.dbconn);
				return new AppDbContext(optionsBuilder.Options);
			}
		}

	}
}
