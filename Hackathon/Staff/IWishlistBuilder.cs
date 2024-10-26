namespace Hackathon.Staff;

public interface IWishlistBuilder
{
    public Wishlist BuildWishlist(int employeeId, int[] employees);
}