using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Connection;

namespace Nexus_Service_Marketing_System.Models
{
    public class equipments
    {
        private ModelsContext db = new ModelsContext();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Equipment ID")]
        public int Equipment_id { set; get; }
        [Required]
        [DisplayName("Vendor ID")]
        [ForeignKey("Vendors")]
        public int Equipment_vendor_id { set; get; }
        [Required]
        [DisplayName("Equipment Name")]
        public string Equipment_name { set; get; }
        [Required]
        [DisplayName("Equipment Type")]
        [DataType(DataType.MultilineText)]
        public string Equipment_type { set; get; }
        [Required]
        [DisplayName("Equipment Image")]
        public string Equipment_image_thumbnail { set; get; }
        [Required]
        [DisplayName("Equipment Content")]
        [DataType(DataType.MultilineText)]
        public string Equipment_content { set; get; }
        [Required]
        [DisplayName("Equipment Price")]
        public double? Equipment_price { set; get; }
        [Required]
        [DisplayName("Equipment Quantity")]
        public int? Equipment_quantity { set; get; }
        [DisplayName("Equipment Status")]
        [RegularExpression("(Active|Inactive)")]
        public string Equipment_status { set; get; }

        public virtual vendors Vendors { set; get; }
        public virtual IEnumerable<equipments_orders> Equipments_orders { set; get; }

        public virtual string Equipment_vendor_name
        {
            get
            {
                var temp = db.vendors.Find(this.Equipment_vendor_id);

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Vendor_name;
            }
        }
    }
}