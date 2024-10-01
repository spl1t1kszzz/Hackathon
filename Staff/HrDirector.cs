namespace Hackathon.Staff
{
    public class HrDirector
    {
        public double CalculateMeanHarmonic(List<Team> teams,
            List<Wishlist> juniorWishLists,
            List<Wishlist> teamleadWishLists)
        {
            var sum = (from team in teams
                let junior = team.Junior
                let teamlead = team.TeamLead
                let juniorWishlist = juniorWishLists.First(list => list.EmployeeId == junior.Id)
                let teamleadWishList = teamleadWishLists.First(list => list.EmployeeId == teamlead.Id)
                let juniorPoints =
                    juniorWishlist.DesiredEmployees.Length -
                    juniorWishlist.DesiredEmployees.ToList().IndexOf(teamlead.Id)
                let teamleadPoints =
                    teamleadWishList.DesiredEmployees.Length -
                    teamleadWishList.DesiredEmployees.ToList().IndexOf(junior.Id)
                select (1.0f / teamleadPoints) + (1.0f / juniorPoints)).Sum();

            // В команде 2 человека
            return teams.Count * 2 / sum;
        }
    }
}