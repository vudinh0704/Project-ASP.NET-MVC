using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nexus_Service_Marketing_System.Models
{
    public class cities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("City ID")]
        public string City_id { set; get; }
        [Required]
        [DisplayName("City Name")]
        public string City_name { set; get; }

        public virtual ICollection<retailShowrooms> RetailShowrooms { set; get; }        
    }
}
