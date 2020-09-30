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
    public class orders
    {
        private ModelsContext db = new ModelsContext();

        public orders()
        {
            Order_date = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Order ID")]
        public string Order_id { set; get; }        
        [DisplayName("Account ID")]
        [ForeignKey("Account")]
        public string Order_account_id { set; get; }        
        [DisplayName("Retail Showroom ID")]
        [ForeignKey("RetailShowrooms")]
        public int? Order_retailShowroom_id { set; get; }
        [Required]
        [DisplayName("Order Feasibility")]
        [RegularExpression("(Pending|Feasible|Infeasible)")]
        public string Order_feasibility { set; get; }
        [Required]
        [DisplayName("Order Status")]
        [RegularExpression("(Pending|Complete|Incomplete|Billed)")]
        public string Order_status { set; get; }
        [Required]
        [DisplayName("Order Phone")]
        [RegularExpression("^[0-9]{10}$")]
        public string Order_phone { set; get; }
        [Required]
        [DisplayName("Order Address")]
        [StringLength(50, MinimumLength = 5)]
        public string Order_address { set; get; }
        [Required]
        [DisplayName("Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Order_date { set; get; }

        public virtual retailShowrooms RetailShowrooms { set; get; }
        public virtual bills Bill { set; get; }
        public virtual accounts Account { set; get; }
        public virtual IEnumerable<equipments_orders> Equipments_orders { set; get; }
        public virtual IEnumerable<plans_orders> Plans_orders { set; get; }

        [DisplayName("Retail Showroom Name")]
        public virtual string Order_retailShowroom_name
        {
            get
            {
                var temp = db.retailShowrooms.FirstOrDefault(item => item.RetailShowroom_id.Equals(this.Order_retailShowroom_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.RetailShowroom_name;
            }
        }

        public virtual string Order_plans_orders_id
        {
            get
            {
                var temp = db.plans_orders.FirstOrDefault(item => item.Plans_orders_order_id.Equals(this.Order_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Plans_orders_id;
            }
        }
    }
}