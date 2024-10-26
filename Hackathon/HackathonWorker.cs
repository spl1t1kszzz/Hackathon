using Hackathon.csv;
using Hackathon.Staff;
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
        var juniorsFile = Path.Combine(Directory.GetCurrentDirectory(), "res/Juniors20.csv");
        var teamleadsFile = Path.Combine(Directory.GetCurrentDirectory(), "res/Teamleads20.csv");
        var juniors = CsvReader.ReadCsvFile(juniorsFile, ';', 1);
        var teamleads = CsvReader.ReadCsvFile(teamleadsFile, ';', 1);
        var juniorsList = new List<Employee>();
        var teamleadsList = new List<Employee>();
        int i;

        for (i = 0; i < juniors.Count; ++i)
        {
            juniorsList.Add(new Employee(i + 1, juniors[i][1]));
        }

        for (i = juniors.Count; i < juniors.Count + teamleads.Count; i++)
        {
            teamleadsList.Add(new Employee(i + 1, teamleads[i - juniors.Count][1]));
        }

        for (i = 1; i <= NumberOfHackathons; i++)
        {
            var wishListBuilder = new RandomWishlistBuilder();
            var juniorsWishLists = juniorsList.Select(junior =>
                    wishListBuilder.BuildWishlist(junior.Id,
                        teamleadsList.Select(teamlead => teamlead.Id).ToArray()))
                .ToList();
            var teamleadsWishLists = teamleadsList.Select(teamlead =>
                    wishListBuilder.BuildWishlist(teamlead.Id, juniorsList.Select(junior => junior.Id).ToArray()))
                .ToList();
            var mh = _hackathon.Run(juniorsList, teamleadsList, juniorsWishLists, teamleadsWishLists);
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