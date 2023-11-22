using AutoMapper;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations;
using MonefyWeb.DomainServices.DomainWebPage.Contracts;
using MonefyWeb.DomainServices.DomainWebPage.Implementations;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infraestructure.RepositoryWebPage.Implementations;
using MonefyWeb.Transversal.WebMappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Http Client
// ------------------------------------------------------------------------------------------------
builder.Services.AddHttpClient();

// Contracts
// ------------------------------------------------------------------------------------------------
builder.Services.AddScoped<ICryptoAppService, CryptoAppService>();
builder.Services.AddScoped<ICryptoLogic, CryptoLogic>();
builder.Services.AddScoped<IEncryptUtils, EncryptUtils>();
builder.Services.AddScoped<IRedisCache, RedisCache>();
builder.Services.AddScoped<IAlphaVantage, AlphaVantage>();

// Mapper Configurator
// ------------------------------------------------------------------------------------------------
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new WebMappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<WebMappingHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Privacy",
        pattern: "Privacy",
        defaults: new { controller = "Privacy", action = "Privacy" }
    );

    endpoints.MapControllerRoute(
        name: "AccountDetail",
        pattern: "AccountDetail",
        defaults: new { controller = "AccountDetail", action = "AccountDetail" }
    );

    endpoints.MapControllerRoute(
        name: "AccounntChart",
        pattern: "AccounntChart",
        defaults: new { controller = "AccounntChart", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "Login",
        pattern: "Login",
        defaults: new { controller = "Login", action = "Login" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();