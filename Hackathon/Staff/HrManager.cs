using Hackathon.Strategy;

namespace Hackathon.Staff
{
    public class HrManager
    {
        private readonly ITeamBuildingStrategy _strategy;

        public HrManager(ITeamBuildingStrategy strategy)
        {
            _strategy = strategy;
        }

        public virtual IEnumerable<Team> CreateTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists,
            IEnumerable<Wishlist> juniorsWishlists)
        {
            return _strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        }
    }
}