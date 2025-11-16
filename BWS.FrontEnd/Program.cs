using BWS.FrontEnd.Converters;

namespace BWS.FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
 
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ClienteServiceApi>();

            builder.Services.AddHttpClient<ClienteServiceApi>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
            });


            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Clientes/Error"); //Editei fuçar pra ver se tem erro dps
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Clientes}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
