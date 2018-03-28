using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Models
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUri { get; set; }
    }
}
