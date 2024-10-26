namespace Hackathon.Staff
{
    public class RandomWishlistBuilder : IWishlistBuilder
    {
        private static readonly Random Random = new Random();

        public Wishlist BuildWishlist(int employeeId, int[] employees)
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