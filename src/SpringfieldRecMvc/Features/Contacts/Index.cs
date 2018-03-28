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
    public class Index
    {
        public class Query : IRequest<Model> { }

        public class Model
        {
            public List<MemberModel> Members { get; set; } = new List<MemberModel>();
        }

        public class MemberModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
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
                IEnumerable<RecMember> members = await this.context.Members
                    .Include(m => m.TelephoneNumbers)
                    .Include(m => m.EmailAddresses)
                    .ToListAsync();

                Model result = new Model();
                foreach (var m in members)
                {
                    MemberModel model = new MemberModel
                    {
                        Name = $"{m.FirstName} {m.LastName}",
                        Email = m.PrimaryEmailAddress?.Address ?? "",
                        PhoneNumber = m.PrimaryTelephoneNumber?.Number ?? ""
                    };
                    result.Members.Add(model);
                }

                return result;
            }
        }
    }
}
