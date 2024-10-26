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

    public double Run(List<Employee> juniorsList, List<Employee> teamleadsList, List<Wishlist> juniorsWishlists,
        List<Wishlist> teamleadsWishlists)
    {
        var teams = _hrManager.CreateTeams(teamleadsList, juniorsList, teamleadsWishlists,
            juniorsWishlists);
        var mh = _hrDirector.CalculateMeanHarmonic(teams.ToList(), juniorsWishlists, teamleadsWishlists);
        return mh;
    }
}