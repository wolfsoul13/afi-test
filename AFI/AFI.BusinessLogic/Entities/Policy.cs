using System.ComponentModel.DataAnnotations;


namespace AFI.BusinessLogic.Entities
{
    public class Policy : BaseEntity
    {
        [Required]
        public string PolicyNumber { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
