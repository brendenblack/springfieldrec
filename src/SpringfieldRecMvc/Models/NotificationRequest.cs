using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Models
{
    public class NotificationRequest : BaseEntity
    {
        public DateTime RequestedOn { get; set; }

        public virtual RecMember Member { get; set; }

        public bool IsContactByPhone { get; set; }

        public bool IsContactByEmail { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
