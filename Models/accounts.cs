using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Nexus_Service_Marketing_System.Connection;

namespace Nexus_Service_Marketing_System.Models
{
    public class accounts
    {
        private ModelsContext db = new ModelsContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [DisplayName("Account ID")]
        [StringLength(20, MinimumLength = 5)]
        public string Account_id { set; get; }  
        [DisplayName("Retail Showroom ID")]
        [ForeignKey("RetailShowroom")]
        public int? Account_retailShowroom_id { set; get; }
        [DisplayName("Account Name")]
        [StringLength(20, MinimumLength = 5)]
        public string Account_name { set; get; }
        [Required]
        [DisplayName("Account Password")]
        [StringLength(20, MinimumLength = 5)]
        public string Account_password { set; get; }
        [Required]
        [DisplayName("Account Role")]
        [RegularExpression("(Admin|Accountant|Technician|Sales Person|Customer)")]        
        public string Account_role { set; get; }
        [Required]
        [DisplayName("Account Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\d{10}")]
        public string Account_phone { set; get; }
        [DisplayName("Account Email")]
        [DataType(DataType.EmailAddress)]
        public string Account_email { set; get; }
        [Required]
        [DisplayName("Account Address")]
        [StringLength(50, MinimumLength = 5)]
        public string Account_address { set; get; }
        [Required]        
        [DisplayName("Account Status")]
        [RegularExpression("(Active|Inactive)")]
        public string Account_status { set; get; }

        public virtual retailShowrooms RetailShowroom { set; get; }

        [DisplayName("Retail Showroom Name")]
        public virtual string Account_retailShowroom_name
        {
            get
            {
                var temp = db.retailShowrooms.FirstOrDefault(item => item.RetailShowroom_id.Equals(this.Account_retailShowroom_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.RetailShowroom_name;
            }
        }
    }
}
