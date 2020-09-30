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
    public class retailShowrooms
    {
        ModelsContext db = new ModelsContext();
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Retail Showroom ID")]
        public int RetailShowroom_id { set; get; }
        [Required]
        [DisplayName("City")]
        [ForeignKey("Cities")]        
        public string RetailShowroom_city_id { set; get; }
        [Required]
        [DisplayName("Retail Showroom Name")]
        public string RetailShowroom_name { set; get; }
        [Required]
        [DisplayName("Retail Showroom Address")]
        public string RetailShowroom_address { set; get; }
        [Required]
        [DisplayName("Retail Showroom Status")]
        [RegularExpression("(Active|Inactive)")]
        public string RetailShowroom_status { set; get; }

        public virtual cities Cities { set; get; }
        public virtual IEnumerable<accounts> Accounts { set; get; }

        [DisplayName("City Name")]
        public virtual string RetailShowroom_city_name
        {
            get
            {                
                var temp = db.cities.SingleOrDefault(item => item.City_id == this.RetailShowroom_city_id);

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.City_name;
            }
        }
    }
}
