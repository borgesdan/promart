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
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Volunteer> Volunteers { get; set; }
        
        public Workshop()
        {
            Guid = Guid.NewGuid();
            Students = new HashSet<Student>();
            Volunteers = new HashSet<Volunteer>();
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }
}
