
using APISignalR.Services;
using SignalRApp;

namespace APISignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:7236");
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyHeader();
                                      policy.AllowCredentials();
                                  });
            });

            builder.Services.AddSingleton<JobService>();
            builder.Services.AddSignalR();

            var app = builder.Build();            

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);

            app.MapHub<JobHub>("/Jobs");

            app.Run();
        }
    }
}
