using Hackathon.Staff;

namespace Hackathon.Strategy;

public class StableMarriageStrategy : ITeamBuildingStrategy
{
    public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var teams = new List<Team>();
        // Teamlead <-> Junior map
        var teamleadPairs = teamLeadsWishlists.ToDictionary(list => list.EmployeeId, list => 0);
        var junWishlistQueue = new Queue<Wishlist>(juniorsWishlists);

        while (junWishlistQueue.Count > 0)
        {
            var curJunList = junWishlistQueue.Dequeue();
            var curJunior = curJunList.EmployeeId;
            foreach (var teamlead in curJunList.DesiredEmployees)
            {
                var teamleadPair = teamleadPairs[teamlead];
                // Если у тимлилда ещё нет пары
                if (teamleadPair == 0)
                {
                    teamleadPairs[teamlead] = curJunior;
                    teams.Add(new Team(teamLeads.First(employee => employee.Id == teamlead),
                        juniors.First(employee => employee.Id == curJunior)));
                    break;
                }

                // Существующий джун для тимлида
                var existingJunior = teamleadPairs[teamlead];
                // Список предпочтений тимлида
                var teamleadList = teamLeadsWishlists.First(list => list.EmployeeId == teamlead).DesiredEmployees
                    .ToList();
                // Если новый джун более предпочтиленый, то мы удаляем команду со старым и добавляем нового
                if (teamleadList.IndexOf(existingJunior) <= teamleadList.IndexOf(curJunior)) continue;
                {
                    teamleadPairs[teamlead] = curJunior;
                    // Возвращаем в очередь старого джуна
                    junWishlistQueue.Enqueue(juniorsWishlists.First(list => list.EmployeeId == existingJunior));
                    teams.Remove(teams.First(team => team.Junior.Id == existingJunior));
                    teams.Add(new Team(teamLeads.First(employee => employee.Id == teamlead),
                        juniors.First(employee => employee.Id == curJunior)));
                    break;
                }
            }
        }

        return teams;
    }
}