using Microsoft.AspNetCore;
using ToDoList;

namespace FinFactory.Api;
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args)
            .Build()
            .Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options =>
            {
                options.ListenAnyIP(5000, listenOptions =>
                {
                    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024;
                });
            })
            .UseStartup<Startup>();
}