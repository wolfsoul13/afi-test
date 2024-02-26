using System.ComponentModel.DataAnnotations;

namespace AFI.BusinessLogic.Entities
{
    public class Contact : BaseEntity
    {
        [Required]
        public ContactType ContactType { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}