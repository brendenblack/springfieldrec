using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Models
{
    public enum Seasons
    {
        [Display(Name = "spring")]
        SPRING,
        [Display(Name = "summer")]
        SUMMER,
        [Display(Name = "fall")]
        FALL,
        [Display(Name = "winter")]
        WINTER
    }
}
