using MediatR;
using SpringfieldRecMvc.Infrastructure;
using SpringfieldRecMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Features.Home
{
    public class Seed
    {
        public class Command : IRequest
        { }

        public class Handler : IRequestHandler<Command>
        {
            private readonly SpringfieldRecContext context;

            public Handler(SpringfieldRecContext context)
            {
                this.context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                this.context.Database.EnsureCreated();
                if (!context.Activities.Any())
                {
                    Activity zumba = new Activity
                    {
                        Name = "Zumba",
                        Description = "Zumba involves dance and aerobic movements performed to energetic music",
                        ImageUri = "zumba.jpg"
                    };
                    context.Activities.Add(zumba);

                    Activity curling = new Activity
                    {
                        Name = "Curling",
                        Description = "Throw rocks at other rocks with friends",
                        ImageUri = "curling.jpg"
                    };
                    context.Activities.Add(curling);

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
