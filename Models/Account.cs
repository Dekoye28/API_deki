using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string password { get; set; }
        public int otp { get; set; }
        public DateTime ExpiredTime { get; set; }
        public bool isTrue { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Profiling Profiling { get; set; }
        public virtual ICollection<AccountRole> AccountRole { get; set; }

    }
}
