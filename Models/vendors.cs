using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus_Service_Marketing_System.Models
{
    public class vendors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vendor_id { set; get; }
        [Required]
        [DisplayName("Vendor Name")]
        public string Vendor_name { set; get; }
        [Required]
        [DisplayName("Vendor Address")]
        public string Vendor_address { set; get; }
        [DisplayName("Vendor Status")]
        [RegularExpression("(Active|Inactive)")]
        public string Vendor_status { set; get; }

        public virtual IEnumerable<equipments> Equipments { set; get; }
    }
}