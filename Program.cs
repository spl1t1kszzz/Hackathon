using Hackathon.Staff;
using Hackathon.csv;

namespace Hackathon
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var juniorsFile = Path.Combine(Directory.GetCurrentDirectory(), "../../../res/Juniors20.csv");
            var teamleadsFile = Path.Combine(Directory.GetCurrentDirectory(), "../../../res/Teamleads20.csv");
            var juniors = CsvReader.ReadCsvFile(juniorsFile, ';', 1);
            var teamleads = CsvReader.ReadCsvFile(teamleadsFile, ';', 1);
            const int numberOfHackathons = 1000;
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

            double sum = 0.0f;
            for (i = 1; i <= numberOfHackathons; ++i)
            {
                var wishListBuilder = new RandomWishlistBuilder();
                var juniorsWishLists = juniorsList.Select(junior =>
                        wishListBuilder.BuildWishlist(junior.Id,
                            teamleadsList.Select(teamlead => teamlead.Id).ToArray()))
                    .ToList();
                var teamleadsWishLists = teamleadsList.Select(teamlead =>
                        wishListBuilder.BuildWishlist(teamlead.Id, juniorsList.Select(junior => junior.Id).ToArray()))
                    .ToList();

                var teams = new HrManager().BuildTeams(teamleadsList, juniorsList, teamleadsWishLists,
                    juniorsWishLists);
                var mh = HrDirector.CalculateMeanHarmonic(teams.ToList(), juniorsWishLists, teamleadsWishLists);
                Console.WriteLine("Hackathon {0} : {1}", i, mh);
                sum += mh;
            }

            Console.WriteLine(sum / numberOfHackathons);
        }
    }
}