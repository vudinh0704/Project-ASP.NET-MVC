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
    public class feedbacks
    {
        private ModelsContext db = new ModelsContext();

        public feedbacks()
        {
            Feedback_created_at = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Feedback ID")]
        public int Feedback_id { set; get; }
        [Required]
        [DisplayName("Account ID")]
        [ForeignKey("Accounts")]
        public string Feedback_account_id { set; get; }        
        [DisplayName("Equipment ID")]
        [ForeignKey("Equipment")]
        public int? Feedback_equipment_id { set; get; }        
        [DisplayName("Plan ID")]
        [ForeignKey("Plans")]
        public int? Feedback_plan_id { set; get; }
        [Required]
        [DisplayName("Feedback Title")]
        public string Feedback_title { set; get; }
        [Required]
        [DisplayName("Feedback Content")]
        [DataType(DataType.MultilineText)]
        public string Feedback_content { set; get; }
        [Required]
        [DisplayName("Feedback Created At")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Feedback_created_at { set; get; }

        public virtual accounts Accounts { set; get; }
        public virtual IEnumerable<replies> Replies
        {
            get
            {
                var results = db.replies.Where(item => item.Reply_feedback_id.Equals(this.Feedback_id)).ToList();
                
                return results;
            }
        }        

        [DisplayName("Account ID")]
        public virtual string Feedback_reply_account_id
        {
            get
            {
                var temp = db.replies.FirstOrDefault(item => item.Reply_feedback_id.Equals(this.Feedback_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Reply_account_id;
            }
        }

        [DisplayName("Account Name")]
        public virtual string Feedback_account_name
        {
            get
            {
                var temp = db.accounts.FirstOrDefault(item => item.Account_id.Equals(this.Feedback_account_id));

                if (temp == null)
                {
                    return "NULL";
                }

                return temp.Account_name;
            }
        }
    }
}