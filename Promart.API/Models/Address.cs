using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Promart.API.Models
{    
    public class Address
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? Street { get; set; }
        [StringLength(50)]
        public string? Neighborhood { get; set; }
        [StringLength(10)]
        public string? Number { get; set; }
        [StringLength(50)]
        public string? Complement { get; set; }
        [StringLength(100)]
        public string? City { get; set; }
        [StringLength(50)]
        public string? State { get; set; }
        [StringLength(20)]
        public string? CEP { get; set; }
        /// <summary>Obtém ou define o tipo de persoa que contém esses endereços (Aluno ou voluntário).</summary>
        public string? Person { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Voluntary>? Volunteers { get; set; }

        public Address()
        {
            Students = new HashSet<Student>();
            Volunteers = new HashSet<Voluntary>();
        }
    }
}
