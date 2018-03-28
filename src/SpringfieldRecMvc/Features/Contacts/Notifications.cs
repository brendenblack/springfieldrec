using MediatR;
using Microsoft.EntityFrameworkCore;
using SpringfieldRecMvc.Infrastructure;
using SpringfieldRecMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Features.Contacts
{
    public class Notifications
    {
        public class Query : IRequest<Model>
        {

        }

        public class Model
        {

        }

        public class RequestModel
        {
            public string Name { get; set; }
            public DateTime RequestedOn { get; set; }
            public string Activity { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly SpringfieldRecContext context;

            public Handler(SpringfieldRecContext context)
            {
                this.context = context;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                IEnumerable<NotificationRequest> requests = await this.context.NotificationRequests
                    .Include(nr => nr.Activity)
                    .Include(nr => nr.Member.TelephoneNumbers)
                    .Include(nr => nr.Member.EmailAddresses)
                    .ToListAsync();

                foreach (var r in requests)
                {
                    RequestModel rm = new RequestModel();
                    rm.Name = $"{r.Member.FirstName} {r.Member.LastName}";
                    rm.RequestedOn = r.RequestedOn;
                    rm.Activity = r.Activity.Name;
                }

                throw new NotImplementedException();
            }
        }

    }
}
