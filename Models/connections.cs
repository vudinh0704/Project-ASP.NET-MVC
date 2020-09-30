using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nexus_Service_Marketing_System.Models
{
    public class connections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Connection ID")]
        public int Connection_id { get; set; }
        [Required]
        [DisplayName("Connection Name")]
        public string Connection_name { get; set; }
        [Required]
        [DisplayName("Connection Description")]
        public string Connection_description { get; set; }
        [DisplayName("Connection Image")]
        public string? Connection_image_thumbnail { get; set; }        
        [DisplayName("Connection Status")]
        [RegularExpression("(Active|Inactive)")]
        public string Connection_status { get; set; }
        
        public virtual ICollection<plans> Plans { get; set; }        
    }
}
