using Hackathon.Staff;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Hackathon;

namespace HackathonTests
{
    [TestFixture]
    public class HrDirectorTests
    {
        private HrDirector _hrDirector;

        [SetUp]
        public void SetUp()
        {
            _hrDirector = new HrDirector();
        }

        [Test]
        public void CalculateMeanHarmonic_SameNumbers_ReturnsSameValue()
        {
            var nums = new List<int> { 10, 10, 10, 10, 10 };
            Assert.That(HmCounter.CountHM(nums), Is.EqualTo(10));
        }


        [Test]
        public void CalculateMeanHarmonic_DifferentValues_ReturnsExpectedResult()
        {
            var numbers2 = new List<int> { 12, 1, 5, 11, 4 };
            Assert.That(HmCounter.CountHM(numbers2), Is.EqualTo(3.07835d).Within(0.0001));
        }

        [Test]
        public void CalculateMeanHarmonic_GivenTeamsAndWishlists_ReturnsExpectedValue()
        {
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

            var result = _hrDirector.CalculateMeanHarmonic(teams, juniorsWishlists, teamLeadsWishlists);


            Assert.That(result, Is.EqualTo(3.0));
        }
    }
}