using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Education")]
    public class Education
    {
        public string EducationID { get; set; }
        public string Degre{ get; set; }
        public string GPA { get; set; }
        [ForeignKey("University")]
        public string University_id { get; set; }
        public virtual ICollection<Profiling> Profiling { get; set; }
        public virtual University University { get; set; }
    }
}
