using System.Configuration;
using Microsoft.EntityFrameworkCore;
// Другие необходимые using-инструкции...

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Другие настройки сервисов...

        // Добавление контекста базы данных
        services.AddDbContext<YourDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("YourConnectionString"));
        });

        // Другие настройки сервисов...
    }

    // Другие методы конфигурации...
}