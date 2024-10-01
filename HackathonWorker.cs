using Microsoft.Extensions.Hosting;

namespace Hackathon;

public class HackathonWorker : IHostedService
{
    private readonly Model.Hackathon _hackathon;
    private const int NumberOfHackathons = 1000;

    public HackathonWorker(Model.Hackathon hackathon)
    {
        _hackathon = hackathon;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        double sum = 0.0f;
        for (var i = 1; i <= NumberOfHackathons; i++)
        {
            var mh = _hackathon.Run();
            Console.WriteLine("Hackathon {0} : {1}", i, mh);
            sum += mh;
        }

        Console.WriteLine(sum / NumberOfHackathons);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("HackathonWorker has been stopped.");
        return Task.CompletedTask;
    }
}