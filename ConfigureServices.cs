using System.Security.Claims;
using System.Text;
using backend.Business.Helpers;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Business.Services;
using BiblioPfe.Common;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Graphql.Types.Mutation;
using BiblioPfe.Graphql.Types.Query;
using BiblioPfe.helpers;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.DataAcces;
using BiblioPfe.Repository.Interfaces;
using dentalevo_backend.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration Configuration
        )
        {
            services.AddDbContext<AppDbContext>(
                options =>
                    options
                        .UseMySQL(
                            Constants.dbconn,
                            x =>
                            {
                                x.EnableRetryOnFailure();
                            }
                        )
                        .EnableDetailedErrors(),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient
            );

            JWTParams jWTParams =
                new()
                {
                    Secret = Configuration["JWTKey:Secret"]!,
                    ValidAudience = Configuration["JWTKey:ValidAudience"]!,
                    ValidIssuer = Configuration["JWTKey:ValidIssuer"]!
                };

            services.AddScoped<AuthHelper>();
            services.AddSingleton<DocumentHelpers>();
            services.AddTransient<AppDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(e => jWTParams);
            services.AddServices();
            services.AddRepositories();
            services.AddQueries();
            services.AddMutations();
            services.AddGraphql();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jWTParams.ValidAudience,
                        ValidIssuer = jWTParams.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jWTParams.Secret)
                        )
                    };
                });

            services.AddCors(
                delegate(CorsOptions options)
                {
                    options.AddPolicy(
                        "CorsPolicy",
                        delegate(CorsPolicyBuilder builder)
                        {
                            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                        }
                    );
                }
            );

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAuthorDA, AuthorDA>();
            services.AddTransient<ICarouselDA, CarouselDA>();
            services.AddTransient<IRulesDA, RulesDA>();
            services.AddTransient<ISeoDataDA, SeoDataDA>();
            services.AddTransient<ICommonDA, CommonDA>();
            services.AddTransient<IUserDa, UserDA>();
            services.AddTransient<ICategoriesDA, CategoriesDA>();
            services.AddTransient<IBookDA, BookDA>();
            services.AddTransient<IDeliveryAddressDA, DeliveryAddressDA>();
            services.AddTransient<ICountryDA, CountryDA>();
            services.AddTransient<IOrderDA, OrderDA>();
            services.AddTransient<IBookReviewsDA, BookReviewsDA>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRulesService, RulesService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IBookServices, BookServices>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ISeoDataServices, SeoDataServices>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICarouselService, CarouselService>();
            services.AddTransient<IDeliveryAddressService, DeliveryAddressService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IBookReviewsService, BookReviewsService>();

            return services;
        }

        public static IServiceCollection AddMutations(this IServiceCollection services)
        {
            services.AddScoped<AuthMutation>();
            services.AddScoped<CategoriesMutation>();
            services.AddScoped<RulesMutation>();
            services.AddScoped<BookMutation>();
            services.AddScoped<CarouselMutation>();
            services.AddScoped<UserMutation>();
            services.AddScoped<DeliveryAddressMutation>();
            services.AddScoped<AuthorMutation>();
            services.AddScoped<BookReviewMutation>();
            services.AddScoped<OrderMutation>();
            services.AddScoped<SeoMutation>();

            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddTransient<AuthQuery>();
            services.AddTransient<CategoriesQuery>();
            services.AddTransient<RulesQuery>();
            services.AddTransient<BookQuery>();
            services.AddTransient<CarouselQuery>();
            services.AddTransient<UserQuery>();
            services.AddTransient<DeliveryAddressQuery>();
            services.AddTransient<AuthorQuery>();
            services.AddTransient<CountryQuery>();
            services.AddTransient<BookReviewQuery>();
            services.AddTransient<OrderQuery>();
            services.AddTransient<SeoQuery>();

            return services;
        }

        public static IServiceCollection AddGraphql(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddType<UploadType>()
                .AddAuthorization(options =>
                {
                    options.AddPolicy(
                        UserRole.Admin.ToString(),
                        policy =>
                            policy
                                .RequireAuthenticatedUser()
                                .RequireAssertion(context =>
                                    new[] { UserRole.Admin, UserRole.SuperAdmin }.Contains(
                                        Enum.Parse<UserRole>(
                                            context.User.FindFirstValue(ClaimTypes.Role)
                                        )
                                    )
                                )
                    );
                    options.AddPolicy(
                        UserRole.SuperAdmin.ToString(),
                        policy =>
                            policy
                                .RequireAuthenticatedUser()
                                .RequireAssertion(context =>
                                    new[] { UserRole.SuperAdmin }.Contains(
                                        Enum.Parse<UserRole>(
                                            context.User.FindFirstValue(ClaimTypes.Role)
                                        )
                                    )
                                )
                    );
                })
                .AddUploadType()
                .AddQueryType<AuthQueryType>()
                .AddTypeExtension<CategoriesQueryType>()
                .AddTypeExtension<RulesQueryTypes>()
                .AddTypeExtension<BookQueryTypes>()
                .AddTypeExtension<CarouselQueryTypes>()
                .AddTypeExtension<UserQueryTypes>()
                .AddTypeExtension<DeliveryAddressQueryTypes>()
                .AddTypeExtension<AuthorQueryTypes>()
                .AddTypeExtension<CountryQueryTypes>()
                .AddTypeExtension<BookReviewQueryTypes>()
                .AddTypeExtension<OrderQueryTypes>()
                .AddTypeExtension<SeoQueryTypes>()
                .AddFiltering()
                .AddSorting()
                .AddMutationType<AuthMutationType>()
                .AddTypeExtension<CategoriesMutationType>()
                .AddTypeExtension<RulesMutationType>()
                .AddTypeExtension<BookMutationTypes>()
                .AddTypeExtension<CarouselMutationTypes>()
                .AddTypeExtension<UserMutationTypes>()
                .AddTypeExtension<DeliveryAddressMutationTypes>()
                .AddTypeExtension<AuthorMutationTypes>()
                .AddTypeExtension<BookReviewMutationTypes>()
                .AddTypeExtension<OrderMutationTypes>()
                .AddTypeExtension<SeoMutationTypes>()
                .UseField<AddBaseUrlToDocumentsMiddleware>()
                .InitializeOnStartup()
                .AddInMemorySubscriptions()
                .SetRequestOptions(_ => new HotChocolate.Execution.Options.RequestExecutorOptions
                {
                    ExecutionTimeout = TimeSpan.FromMinutes(4)
                })
                .InitializeOnStartup();

            return services;
        }
    }
}
