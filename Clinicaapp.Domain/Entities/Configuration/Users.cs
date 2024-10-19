

using Clinicaapp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicaapp.Domain.Entities.Configuration
{
    [Table("Doctors", Schema = "dbo")]
    public class Users : BaseEntity
    {
        [Key]
        public int UserID { get; set; }
        
    }
}
