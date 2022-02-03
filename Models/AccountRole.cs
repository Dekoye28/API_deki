using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_AccountRole")]
    public class AccountRole
    {
        //[Key]
        public string NIK { get; set; }
        //[ForeignKey("Role")]
        public string RoleID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }

    }
}
