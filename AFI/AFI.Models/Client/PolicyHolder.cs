
namespace AFI.Models.Client
{
    public class PolicyHolder : IClientModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PolicyNumber { get; set; }

        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}