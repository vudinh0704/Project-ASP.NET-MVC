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
    public class plans_orders
    {
        private ModelsContext db = new ModelsContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Plans Orders ID")]
        public string Plans_orders_id { set; get; }
        [ForeignKey("Plans")]
        public int? Plans_orders_plan_id { set; get; }
        [DisplayName("Order ID")]
        [ForeignKey("Orders")]
        public string Plans_orders_order_id { set; get; }
        [DisplayName("Plans Orders Status")]
        [RegularExpression("(Pending|Temporarily Inactive|Permanently Inactive|Connected|Disconnected|Expired)")]
        public string Plans_orders_status { set; get; } 

        public virtual orders Orders { set; get; }
        public virtual plans Plans { set; get; }
        public virtual ICollection<tracks> Tracks { set; get; }

        public virtual string Plans_orders_plan_name
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Plan_name;
            }
        }

        [DisplayName("Plan Price")]
        public virtual double? Plans_orders_plan_price
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));                               

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plan_fixed_price;
            }
        }

        [DisplayName("Plan Local Price")]
        public virtual double? Plans_orders_plan_price_local
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plan_local_price;
            }
        }

        [DisplayName("Plan Standard Price")]
        public virtual double? Plans_orders_plan_price_std
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plan_std_price;
            }
        }

        [DisplayName("Plan Messaging for Mobiles Price")]
        public virtual double? Plans_orders_plan_price_msg
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plan_messaging_for_mobiles_price;
            }
        }

        [DisplayName("Plan Security Deposit")]
        public virtual double? Plans_orders_plan_security_deposit
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plan_security_deposit;
            }
        }

        public virtual int? Plans_orders_connection_id
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plan_connection_id;
            }
        }

        [DisplayName("Connection Name")]
        public virtual string Plans_orders_connection_name
        {
            get
            {
                var temp = db.plans.SingleOrDefault(item => item.Plan_id.Equals(this.Plans_orders_plan_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Plan_connection_name;
            }
        }

        [DisplayName("Account ID")]
        public virtual string Plans_orders_account_id
        {
            get
            {
                var temp = db.orders.SingleOrDefault(item => item.Order_id.Equals(this.Plans_orders_order_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Order_account_id;
            }
        }

        [DisplayName("Order Status")]
        public virtual string Plans_orders_order_status
        {
            get
            {
                var temp = db.orders.FirstOrDefault(item => item.Order_id.Equals(this.Plans_orders_order_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Order_status;
            }
        }
    }
}