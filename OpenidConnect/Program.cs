var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "OpenID";
}).AddCookie("Cookies").AddOpenIdConnect("OpenID", options =>
{
    options.Authority = "https://accounts.google.com";
    options.ClientId = "750963443093-349basht02qd13ef71cuc9b5epp0lk58.apps.googleusercontent.com";
    options.ClientSecret = "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";
    options.CallbackPath = "/signin-oidc";
    options.ResponseType = "code";
    options.SaveTokens = true;
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
