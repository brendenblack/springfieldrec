using MediatR;
using Microsoft.EntityFrameworkCore;
using SpringfieldRecMvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Features.Home
{
    public class Index
    {
        public class Query : IRequest<ActivitiesEnvelope>
        {

        }

        public class ActivitiesEnvelope
        {
            public List<ActivityModel> Activities { get; set; } = new List<ActivityModel>();
        }

        public class ActivityModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageUri { get; set; }
            
        }

        public class Handler : IRequestHandler<Query, ActivitiesEnvelope>
        {
            private readonly SpringfieldRecContext context;
            public Handler(SpringfieldRecContext context)
            {
                this.context = context;
            }

            public SpringfieldRecContext Context { get; }

            public async Task<ActivitiesEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await this.context.Activities.ToListAsync();

                var envelope = new ActivitiesEnvelope();
                foreach (var activity in activities)
                {
                    envelope.Activities.Add(new ActivityModel
                    {
                        Id = activity.Id,
                        Description = activity.Description,
                        Name = activity.Name,
                        ImageUri = activity.ImageUri
                    });
                }

                return envelope;
            }
        }

    }
}
