using Hackathon.Staff;

namespace HackathonTests;

[TestFixture]
public class RandomWishlistBuilderTests
{
    [Test]
    public void GenerateWishlist_SizeMatchesNumberOfTeamLeads()
    {
        var builder = new RandomWishlistBuilder();
        const int employeeId = 1;
        var teamLeads = new[] { 1, 2, 3, 4 };

        var wishlist = builder.BuildWishlist(employeeId, teamLeads);

        Assert.That(wishlist.DesiredEmployees, Has.Length.EqualTo(teamLeads.Length));
    }

    [Test]
    public void GenerateWishlist_ContainsSpecifiedEmployee()
    {
        var builder = new RandomWishlistBuilder();
        const int employeeId = 1;
        var teamLeads = new [] { 1, 2, 3, 4 };

        var wishlist = builder.BuildWishlist(employeeId, teamLeads);

        Assert.That(wishlist.DesiredEmployees, Does.Contain(3));
    }
}