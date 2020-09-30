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
    public class tracks
    {
        private ModelsContext db = new ModelsContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Track ID")]
        public string Track_id { set; get; }
        [Required]
        [DisplayName("Plans Orders ID")]
        [ForeignKey("Plans_orders")]
        public string Track_plans_orders_id { set; get; }
        [Required]
        [DisplayName("Track Date From")]        
        public string Track_date_from { set; get; }
        [Required]
        [DisplayName("Track Date To")]        
        public string Track_date_to { set; get; }
        [Required]
        [DisplayName("Track Local Used Time")]
        public int Track_time_used_local { set; get; }
        [Required]
        [DisplayName("Track STD Used Time")]
        public int Track_time_used_std { set; get; }
        [Required]
        [DisplayName("Track Messaging for Mobiles Used Time")]
        public int Track_time_used_msg { set; get; }
        [Required]
        [DisplayName("Track Status")]
        [RegularExpression("(Pending|Complete|Billed)")]
        public string Track_status { set; get; }

        public virtual plans_orders Plans_orders { set; get; }
        public virtual bills Bill { set; get; }

        public virtual string Track_order_id
        {
            get
            {
                var temp = db.plans_orders.SingleOrDefault(item => item.Plans_orders_id.Equals(this.Track_plans_orders_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Plans_orders_order_id;
            }
        }

        [DisplayName("Account ID")]
        public virtual string Track_account_id
        {
            get
            {
                var temp = db.plans_orders.SingleOrDefault(item => item.Plans_orders_id.Equals(this.Track_plans_orders_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Plans_orders_account_id;
            }
        }
        
        public virtual double? Track_plan_price_local
        {
            get
            {
                var temp = db.plans_orders.SingleOrDefault(item => item.Plans_orders_id.Equals(this.Track_plans_orders_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plans_orders_plan_price_local;
            }
        }

        public virtual double? Track_plan_price_sub_total_local
        {
            get
            {
                var temp = this.Track_time_used_local * this.Track_plan_price_local;

                return temp;
            }
        }
        
        public virtual double? Track_plan_price_std
        {
            get
            {
                var temp = db.plans_orders.SingleOrDefault(item => item.Plans_orders_id.Equals(this.Track_plans_orders_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plans_orders_plan_price_std;
            }
        }

        public virtual double? Track_plan_price_sub_total_std
        {
            get
            {
                var temp = this.Track_time_used_std * this.Track_plan_price_std;

                return temp;
            }
        }
        
        public virtual double? Track_plan_price_msg
        {
            get
            {
                var temp = db.plans_orders.SingleOrDefault(item => item.Plans_orders_id.Equals(this.Track_plans_orders_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Plans_orders_plan_price_msg;
            }
        }

        public virtual double? Track_plan_price_sub_total_msg
        {
            get
            {
                var temp = this.Track_time_used_msg * this.Track_plan_price_msg;

                return temp;
            }
        }
    }
}