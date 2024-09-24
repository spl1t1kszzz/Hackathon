using Hackathon.Staff;

namespace Hackathon
{
    public class WishlistBuilder
    {
        private static readonly Random Random = new Random();

        public static Wishlist BuildWishlist(int employeeId, int[] employees)
        {
            // Random init
            for (var i = employees.Length - 1; i > 0; i--)
            {
                var j = Random.Next(i + 1);
                (employees[i], employees[j]) = (employees[j], employees[i]);
            }

            return new Wishlist(employeeId, employees);
        }
    }
}