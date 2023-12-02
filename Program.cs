using OrchardCore.Recipes;
using OrchardCore.ResourceManagement.TagHelpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.AddOrchardCore()
    .AddCommands()
    .AddSecurity()
    .AddMvc()
    .AddIdGeneration()
    .AddEmailAddressValidator()
    .AddSetupFeatures("OrchardCore.Setup")
    .AddDataAccess()
    .AddDataStorage()
    .AddBackgroundService()
    .AddScripting()

    .AddTheming()
    //.AddLiquidViews()
    .AddCaching()
    .ConfigureServices(services => services
        .AddRecipes())

    // OrchardCoreBuilder is not available in OrchardCore.ResourceManagement as it has to remain independent from OrchardCore.
    .ConfigureServices(s =>
    {
        s.AddResourceManagement();

        s.AddTagHelpers<LinkTagHelper>();
        s.AddTagHelpers<MetaTagHelper>();
        s.AddTagHelpers<ResourcesTagHelper>();
        s.AddTagHelpers<ScriptTagHelper>();
        s.AddTagHelpers<StyleTagHelper>();
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseOrchardCore();

app.Run();
