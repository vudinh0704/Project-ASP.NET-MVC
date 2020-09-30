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
    public class equipments_orders
    {
        private ModelsContext db = new ModelsContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Equipments_orders_id { set; get; }
        [Required]
        [ForeignKey("Equipments")]
        public int? Equipments_orders_equipment_id { set; get; }
        [Required]
        [ForeignKey("Orders")]
        public string Equipments_orders_order_id { set; get; }
        [Required]
        public int? Equipments_orders_quantity { set; get; }

        public virtual equipments Equipments { set; get; }
        public virtual orders Orders { set; get; }

        public virtual string Equipments_orders_equipment_name
        {
            get
            {
                var temp = db.equipments.SingleOrDefault(item => item.Equipment_id.Equals(this.Equipments_orders_equipment_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Equipment_name;
            }
        }

        public virtual double? Equipments_orders_equipment_price
        {
            get
            {
                var temp = db.equipments.SingleOrDefault(item => item.Equipment_id.Equals(this.Equipments_orders_equipment_id));

                if (temp == null)
                {
                    return 0;
                }

                return temp.Equipment_price;
            }
        }

        public virtual double? Equipments_orders_sub_total
        {
            get
            {
                var temp = this.Equipments_orders_quantity * this.Equipments_orders_equipment_price;

                return temp;
            }
        }
    }
}