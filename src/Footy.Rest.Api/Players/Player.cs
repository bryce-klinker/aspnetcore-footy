using System.ComponentModel.DataAnnotations;

namespace Footy.Rest.Api.Players
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Surname { get; set; }
    }
}