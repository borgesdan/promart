using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promart.API.Models
{
    public class StudentRelationship
    {        
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }        
        public int Age { get; set; }
        [StringLength(100)]
        public string? Relationship { get; set; }
        [StringLength(100)]
        public string? Occupation { get; set; }
        [StringLength(100)]
        public string? Education { get; set; }
        [StringLength(200)]
        public string? Income { get; set; }

        //------ Relacionamentos ------//

        public Student? Student { get; set; }
    }
}