namespace MagicCuisine.Models
{
    public class ManageIndexViewModel
    {
        public bool HasPassword { get; set; }

        public UserViewModel User { get; set; }

        public AddressViewModel Address { get; set; }
    }
}