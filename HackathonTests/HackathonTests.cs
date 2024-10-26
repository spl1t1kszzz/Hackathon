using Hackathon.Staff;
using Hackathon.Strategy;

namespace HackathonTests;

public class HackathonTests
{
    [Test]
    public void Run_Hackathon_ReturnsExpectedMeanHarmonic()
    {
        var hrDirector = new HrDirector();
        var strategy = new StableMarriageStrategy();
        var hrManager = new HrManager(strategy);
        var hackathon = new Hackathon.Model.Hackathon(hrManager, hrDirector);
        var teamLeads = new List<Employee>
        {
            new(1, "TeamLead1"),
            new(2, "TeamLead2"),
            new(3, "TeamLead3"),
        };

        var juniors = new List<Employee>
        {
            new(4, "Junior1"),
            new(5, "Junior2"),
            new(6, "Junior3")
        };

        var teamLeadsWishlists = new List<Wishlist>
        {
            new(1, new[] { 4, 5, 6 }),
            new(2, new[] { 5, 4, 6 }),
            new(3, new[] { 6, 5, 4 })
        };

        var juniorsWishlists = new List<Wishlist>
        {
            new(4, new[] { 1, 2, 3 }),
            new(5, new[] { 2, 1, 3 }),
            new(6, new[] { 3, 1, 2 })
        };
        var teams = new List<Team>
        {
            new(teamLeads[0], juniors[0]),
            new(teamLeads[1], juniors[1]),
            new(teamLeads[2], juniors[2])
        };
        var mh = hackathon.Run(juniors, teamLeads, juniorsWishlists, teamLeadsWishlists);
        Assert.That(mh, Is.EqualTo(3.0));
    }
}