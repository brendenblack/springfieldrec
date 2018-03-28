using Microsoft.Extensions.DependencyInjection;
using SpringfieldRecMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Infrastructure
{
    public class Seed
    {
        public static void Initialize(SpringfieldRecContext db)
        {
            db.Database.EnsureCreated();

            if (!db.Activities.Any())
            {
                Activity zumba = new Activity
                {
                    Name = "Zumba",
                    Description = "Zumba involves dance and aerobic movements performed to energetic music",
                    ImageUri = "zumba.jpg"
                };
                db.Activities.Add(zumba);

                Activity curling = new Activity
                {
                    Name = "Curling",
                    Description = "Throw rocks at other rocks with friends",
                    ImageUri = "curling.jpg"
                };
                db.Activities.Add(curling);

                db.SaveChanges();

            };
            
        }
    }
}
