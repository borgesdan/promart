using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Promart.API.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public Guid Guid { get; set; }
        //------ Dados Pessoais ------//

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
        [StringLength(20)]
        public string? RG { get; set; }
        [StringLength(20)]
        public string? CPF { get; set; }
        [StringLength(50)]
        public string? Certidao { get; set; }

        //------ Dados Familiares ------//

        [StringLength(300)]
        public string? ResponsibleName { get; set; }        
        public int ResponsibleRelationship { get; set; }        
        /// <summary>Obtém ou define se o aluno é beneficiário do governo (Bolsa Família).</summary>
        public bool IsGovernmentBeneficiary { get; set; }
        public int Dwelling { get; set; }        
        public int MonthlyIncome { get; set; }

        //------ Contatos ------//
        [StringLength(20)]
        public string? Phone1 { get; set; }
        [StringLength(20)]
        public string? Phone2 { get; set; }
        [StringLength(200)]
        public string? Email1 { get; set; }
        [StringLength(200)]
        public string? Email2 { get; set; }

        //------ Dados Escolares ------//
        [StringLength(200)]
        public string? SchoolName { get; set; }        
        public int SchoolYear { get; set; }
        public int SchoolShift { get; set; }        

        //------ Dados Projeto ------//                        
        public int ProjectStatus { get; set; }        
        public int ProjectShift { get; set; }
        /// <summary>Obtém ou define a matrícula.</summary>
        [StringLength(20)]
        public string? ProjectEnrollment { get; set; }
        /// <summary>Obtém ou define a data da matrícula.</summary>
        public DateTime? ProjectEnrollmentDate { get; set; }

        //------ Outros ------//
        /// <summary>Observações do aluno.</summary>
        [StringLength(5000)]
        public string? Observations { get; set; }
        [StringLength(500)]
        public string? PhotoUrl { get; set; }

        //------ Relacionamentos ------//
        public Address? Address { get; set; }
        public virtual ICollection<Workshop>? Workshops { get; set; }
        public virtual ICollection<StudentRelationship> Relationships { get; set; }
        
        public Student()
        {
            Workshops = new HashSet<Workshop>();
            Relationships = new HashSet<StudentRelationship>();
        }

        public override string ToString()
        {
            return FirstName == null || LastName == null 
                ? string.Empty 
                : FirstName + " " + LastName;
        }
    }
}
