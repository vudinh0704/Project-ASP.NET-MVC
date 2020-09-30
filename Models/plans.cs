using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nexus_Service_Marketing_System.Connection;

namespace Nexus_Service_Marketing_System.Models
{
    public class plans
    {
        private ModelsContext db = new ModelsContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Plan ID")]
        public int Plan_id { get; set; }
        [Required]
        [DisplayName("Connection ID")]        
        [ForeignKey("Connections")]
        public int? Plan_connection_id { get; set; }
        [Required]
        [DisplayName("Plan Name")]        
        public string Plan_name { get; set; }
        [Required]
        [DisplayName("Plan Description")]        
        public string Plan_description { get; set; }
        [DisplayName("Plan Image")]        
        public string? Plan_image_thumbnail { get; set; }
        [Required]
        [DisplayName("Plan Fixed Price")]        
        public double Plan_fixed_price { get; set; }
        [DisplayName("Plan Local Price")]        
        public double? Plan_local_price { get; set; }
        [DisplayName("Plan Standard Price")]        
        public double? Plan_std_price { get; set; }
        [DisplayName("Plan Messaging for Mobiles Price")]        
        public double? Plan_messaging_for_mobiles_price { get; set; }
        [DisplayName("Plan Status")]
        [RegularExpression("(Active|Inactive)")]
        public string Plan_status { get; set; }

        public virtual connections Connections { get; set; }
        public virtual ICollection<plans_orders> Plans_Orders { get; set; }

        [DisplayName("Connection Name")]        
        public virtual string Plan_connection_name
        {
            get
            {
                var temp = db.connections.SingleOrDefault(item => item.Connection_id.Equals(this.Plan_connection_id));
                
                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Connection_name;
            }
        }

        [DisplayName("Plan Security Deposit")]
        public virtual double? Plan_security_deposit
        {
            get
            {
                if (this.Plan_connection_id == 1)
                {
                    return 325;
                }
                else if (this.Plan_connection_id == 2)
                {
                    return 250;
                }
                else
                {
                    return 500;
                }
            }
        }

    }
}