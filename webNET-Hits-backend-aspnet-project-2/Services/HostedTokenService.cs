using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class HostedTokenService: BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public HostedTokenService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    
                var tokens = await context.DisactiveTokens.Where(x => x.TimeLogOut.AddHours(1) < DateTime.UtcNow).ToListAsync(cancellationToken: stoppingToken);
                foreach (var token in tokens)
                {
                    context.DisactiveTokens.Remove(token);
                }
                    
                await context.SaveChangesAsync(stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
    
}