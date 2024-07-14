using AspNecCore.Identity.Database;
using AspNecCore.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  //=> raha ampiasa SWAGGER avy @ Swashbuckle.AspNetCore.Swagger 

 /* use openApi instead*/
//builder.Services.AddOpenApiDocument(); // mila NSwag.AspNetCore 

/*"implementation authentication support" using Asp.Net Core Identity (AddAuthorization + AddAuthentication)*/
builder.Services.AddAuthorization(); 
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme)  // cookie no mora ampiasaina + authentication Scheme (Shema d'authentification) on utilise le constans (avy @ Micr AspNet core Identity ) 
    .AddBearerToken(IdentityConstants.BearerScheme); 
/*avieo mila ampiana IdentityCore ilay service + specification identity user*/
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Mydatabase")));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseOpenApi();  // UseSwagger() raha SwaggerGen no ampiasaina
    //app.UseSwaggerUi(); // UseSwaggerUI(); raha SwaggerGen no ampiasaina

    app.ApplyMigrations();
}

//protected endpoint with authorization
app.MapGet("users/me", async (ClaimsPrincipal claims, ApplicationDbContext context) =>
{
    string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    return await context.Users.FindAsync(userId);
})
.RequireAuthorization() ;

app.UseHttpsRedirection();

app.MapIdentityApi<User>();
app.Run();
