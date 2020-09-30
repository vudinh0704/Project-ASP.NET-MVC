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
    public class replies
    {
        private ModelsContext db = new ModelsContext();

        public replies()
        {
            Reply_created_at = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Reply ID")]
        public int Reply_id { set; get; }
        [Required]
        [DisplayName("Account ID")]
        [ForeignKey("Accounts")]
        public string Reply_account_id { set; get; }
        [Required]
        [DisplayName("Feedback ID")]
        [ForeignKey("Feedbacks")]
        public int Reply_feedback_id { set; get; }
        [Required]
        [DisplayName("Reply Content")]
        [DataType(DataType.MultilineText)]
        public string Reply_content { set; get; }
        [Required]
        [DisplayName("Reply Created At")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Reply_created_at { set; get; }

        public virtual feedbacks Feedbacks { set; get; }
        public virtual accounts Accounts { set; get; }

        [DisplayName("Account Reply Name")]
        public virtual string Reply_account_name
        {
            get
            {
                var temp = db.accounts.FirstOrDefault(item => item.Account_id.Equals(this.Reply_account_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Account_name;
            }
        }
    }
}