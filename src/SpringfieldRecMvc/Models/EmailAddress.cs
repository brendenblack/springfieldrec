using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Models
{
    public class EmailAddress : BaseEntity
    {
        public virtual RecMember Owner { get; set; }

        public string Address { get; set; }
    }
}
