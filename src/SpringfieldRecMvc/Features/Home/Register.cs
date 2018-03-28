using MediatR;
using SpringfieldRecMvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Features.Home
{
    public class Register
    {
        public class Query : IRequest<Command>
        {

        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly SpringfieldRecContext context;

            public QueryHandler(SpringfieldRecContext context)
            {
                this.context = context;
            }
            public async Task<Command> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Command();
            }
        }
        

        public class Command : IRequest
        {
            [Display(Name = "First name")]
            public String FirstName { get; set; } = "Hello";

            [Display(Name = "Last name")]
            public String LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public String PhoneNumber { get; set; }

            [EmailAddress]
            [Display(Name = "E-mail address")]
            public String EmailAddress { get; set; }

        }

    }
}
