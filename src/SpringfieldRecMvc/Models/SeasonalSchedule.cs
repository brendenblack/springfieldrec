using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Models
{
    public class SeasonalSchedule : BaseEntity
    {
        public Seasons Season { get; set; }
    }
}
