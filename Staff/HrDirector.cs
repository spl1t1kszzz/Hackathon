namespace Hackathon.Staff
{
    public class HrDirector
    {
        public static double CalculateMeanHarmonic(List<Team> teams,
            List<Wishlist> juniorWishLists,
            List<Wishlist> teamleadWishLists)
        {
            var sum = 0.0f;
            foreach (var team in teams)
            {
                // Текущий джун и тимлид
                var junior = team.Junior;
                var teamlead = team.TeamLead;
                // Их списки предпочтений
                var juniorWishlist = juniorWishLists.First(list => list.EmployeeId == junior.Id);
                var teamleadWishList = teamleadWishLists.First(list => list.EmployeeId == teamlead.Id);
                // Считаем индексы удовлетворённости
                var juniorPoints = juniorWishlist.DesiredEmployees.Length -
                                   juniorWishlist.DesiredEmployees.ToList().IndexOf(teamlead.Id);
                var teamleadPoints = teamleadWishList.DesiredEmployees.Length -
                                     teamleadWishList.DesiredEmployees.ToList().IndexOf(junior.Id);

                sum += (1.0f / teamleadPoints) + (1.0f / juniorPoints);
            }

            // В команде 2 человека
            return teams.Count * 2 / sum;
        }
    }
}