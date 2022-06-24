using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Promart.API.Models
{
    public class Workshop
    {
        public int Id { get; set; }
        [Required]
        public Guid Guid { get; set; }
        [Required]
        [StringLength(200)]
        public string? Name { get; set; }
        [Required]
        public bool IsActivated { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }        

        //------ Relacionamentos ------//
        public ICollection<Student> Students { get; set; }
        public ICollection<Voluntary> Voluntaries { get; set; }
        
        public Workshop()
        {
            Students = new HashSet<Student>();
            Voluntaries = new HashSet<Voluntary>();
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }
}
