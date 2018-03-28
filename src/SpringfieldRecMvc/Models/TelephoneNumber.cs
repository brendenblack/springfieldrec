using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Models
{
    public class TelephoneNumber : BaseEntity
    {
        public virtual RecMember Owner { get; set; }

        // https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/PhoneAttribute.cs
        [Phone]
        [Required]
        public String Number { get; set; }

        public bool IsPrimary { get; set; }
    }
}
