using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nexus_Service_Marketing_System.Connection;

namespace Nexus_Service_Marketing_System.Models
{
    public class bills
    {
        private ModelsContext db = new ModelsContext();

        public bills()
        {
           Bill_date = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Bill ID")]
        public string Bill_id { set; get; }   
        [DisplayName("Order ID")]
        [ForeignKey("Order")]
        public string Bill_order_id { set; get; }        
        [DisplayName("Track ID")]
        [ForeignKey("Track")]
        public string Bill_track_id { set; get; }        
        [Required]
        [DisplayName("Bill Status")]
        [RegularExpression("(Unpaid|Paid)")]
        public string Bill_status { set; get; }        
        [Required]
        [DisplayName("Bill Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Bill_date { set; get; }
        
        public virtual orders Order { set; get; }
        public virtual tracks Track { set; get; }
    }
}