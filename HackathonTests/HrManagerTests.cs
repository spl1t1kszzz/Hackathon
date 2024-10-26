using Hackathon.Staff;
using Hackathon.Strategy;
using Moq;


namespace HackathonTests
{
    [TestFixture]
    public class HrManagerTests
    {
        [Test]
        public void CreateTeams_ReturnsCorrectNumberOfTeams()
        {
            var strategy = new StableMarriageStrategy();

            var hrManager = new HrManager(strategy);
            var teamLeads = new List<Employee>
            {
                new(1, "TeamLead1"),
                new(2, "TeamLead2")
            };

            var juniors = new List<Employee>
            {
                new(3, "Junior1"),
                new(4, "Junior2")
            };

            var leadLists = new List<Wishlist>
            {
                new(1, new[] { 3, 4 }),
                new(2, new[] { 4, 3 })
            };

            var junLists = new List<Wishlist>
            {
                new(3, new[] { 1, 2 }),
                new(4, new[] { 2, 1 }),
            };

            var teams = hrManager.CreateTeams(teamLeads, juniors, leadLists, junLists);

            Assert.That(teams.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CreateTeams_StrategyCalledOnce()
        {
            var strategy = new StableMarriageStrategy();

            var managerMock = new Mock<HrManager>(strategy);

            var teamLeads = new List<Employee>
            {
                new(1, "TeamLead1"),
                new(2, "TeamLead2")
            };

            var juniors = new List<Employee>
            {
                new(3, "Junior1"),
                new(4, "Junior2")
            };

            var teamLeadsWishlists = new List<Wishlist>
            {
                new(1, new[] { 3, 4 }),
                new(2, new[] { 4, 3 })
            };

            var juniorsWishlists = new List<Wishlist>
            {
                new(3, new[] { 1, 2 }),
                new(4, new[] { 2, 1 })
            };


            managerMock.Object.CreateTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);


            managerMock.Verify(m => m.CreateTeams(
                    It.IsAny<IEnumerable<Employee>>(),
                    It.IsAny<IEnumerable<Employee>>(),
                    It.IsAny<IEnumerable<Wishlist>>(),
                    It.IsAny<IEnumerable<Wishlist>>()),
                Times.Once);
        }

        [Test]
        public void BuildTeams_HandlesPreferenceChangesCorrectly()
        {
            var strategy = new StableMarriageStrategy();

            var hrManager = new HrManager(strategy);
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

            var teams = hrManager.CreateTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

            var teamsList = teams.ToList();
            Assert.Multiple(() =>
            {
                Assert.That(teamsList.First(x => x.TeamLead.Id == 1).Junior.Id, Is.EqualTo(4));
                Assert.That(teamsList.First(x => x.TeamLead.Id == 2).Junior.Id, Is.EqualTo(5));
                Assert.That(teamsList.First(x => x.TeamLead.Id == 3).Junior.Id, Is.EqualTo(6));
            });
            Console.WriteLine(teams);
        }
    }
}