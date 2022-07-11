using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Promart.API.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        [Required]
        public Guid Guid { get; set; }        
        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(200)]
        public string? LastName { get; set; }
        [StringLength(100)]
        public string? NickName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Gender { get; set; }
        [StringLength(100)]
        public string? Occupation { get; set; }
        [StringLength(100)]
        public string? Education { get; set; }        
        [StringLength(20)]
        public string? RG { get; set; }
        [StringLength(20)]
        public string? CPF { get; set; }
        [StringLength(20)]
        public string? Phone1 { get; set; }
        [StringLength(20)]
        public string? Phone2 { get; set; }
        [StringLength(200)]
        public string? Email1 { get; set; }
        [StringLength(200)]
        public string? Email2 { get; set; }        
        /// <summary>Obtém ou define o registro do voluntário.</summary>
        [StringLength(20)]
        public string? ProjectRegistration{ get; set; }
        /// <summary>Obtém ou define a data do registro.</summary>
        public DateTime? ProjectRegistrationDate { get; set; }
        /// <summary>Observações do aluno.</summary>
        [StringLength(5000)]
        public string? Observations { get; set; }
        [StringLength(500)]
        public string? PhotoUrl { get; set; }
        
        public Address? Address { get; set; }
        public virtual ICollection<Workshop>? Workshops { get; set; }

        public Volunteer()
        {
            Guid = Guid.NewGuid();
            Workshops = new HashSet<Workshop>();
        }

        public override string ToString()
        {
            return FirstName == null || LastName == null
                ? string.Empty
                : FirstName + " " + LastName;
        }
    }
}
