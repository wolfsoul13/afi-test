using System.ComponentModel.DataAnnotations;

namespace AFI.BusinessLogic.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<Policy> Policies { get; } = new();
        public List<Contact> Contacts { get; } = new();
    }
}
