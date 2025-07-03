var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services
.AddApplicationServices(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapGraphQL();
app.UseStaticFiles();
app.UseWebSockets();
app.UseCors(e =>
e.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin()
);
app.MapControllers();

app.Run();
