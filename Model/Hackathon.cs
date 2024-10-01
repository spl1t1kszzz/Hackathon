using Hackathon.csv;
using Hackathon.Staff;

namespace Hackathon.Model;

public class Hackathon
{
    private readonly HrManager _hrManager;
    private readonly HrDirector _hrDirector;

    public Hackathon(HrManager hrManager, HrDirector hrDirector)
    {
        _hrManager = hrManager;
        _hrDirector = hrDirector;
    }

    public double Run()
    {
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


        var wishListBuilder = new RandomWishlistBuilder();
        var juniorsWishLists = juniorsList.Select(junior =>
                wishListBuilder.BuildWishlist(junior.Id,
                    teamleadsList.Select(teamlead => teamlead.Id).ToArray()))
            .ToList();
        var teamleadsWishLists = teamleadsList.Select(teamlead =>
                wishListBuilder.BuildWishlist(teamlead.Id, juniorsList.Select(junior => junior.Id).ToArray()))
            .ToList();

        var teams = _hrManager.CreateTeams(teamleadsList, juniorsList, teamleadsWishLists,
            juniorsWishLists);
        var mh = _hrDirector.CalculateMeanHarmonic(teams.ToList(), juniorsWishLists, teamleadsWishLists);
        return mh;
    }
}