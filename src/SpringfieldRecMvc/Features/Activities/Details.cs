using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpringfieldRecMvc.Infrastructure;
using SpringfieldRecMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Features.Activities
{
    public class Details
    {
        public class Query : IRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Model
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public String ImageUri { get; set; }

            public Command Command { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private readonly SpringfieldRecContext context;

            public QueryHandler(SpringfieldRecContext context)
            {
                this.context = context;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this.context.Activities.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                if (activity == null)
                {
                    throw new HttpException(404, "File Not Found");
                }

                Model model = new Model
                {
                    Command = new Command { ActivityId = request.Id },
                    Name = activity.Name,
                    Description = activity.Description,
                    ImageUri = activity.ImageUri
                };

                return model;
            }
        }

        public class Command : IRequest
        {
            
            public int ActivityId { get; set; }

            [Display(Name = "First name")]
            [Required]
            public String FirstName { get; set; }

            [Display(Name = "Last name")]
            [Required]
            public String LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public String PhoneNumber { get; set; }

            [EmailAddress]
            [Display(Name = "E-mail address")]
            public String EmailAddress { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(m => m.EmailAddress)
                    .NotEmpty()
                    .When(m => string.IsNullOrEmpty(m.PhoneNumber))
                    .WithMessage("At least one of e-mail address or phone number are required");

                RuleFor(m => m.PhoneNumber)
                    .NotEmpty()
                    .When(m => string.IsNullOrEmpty(m.EmailAddress))
                    .WithMessage("At least one of phone number or e-mail address are required");
            }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly SpringfieldRecContext context;

            public CommandHandler(SpringfieldRecContext context)
            {
                this.context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                Activity activity = await this.context.Activities
                    .FirstOrDefaultAsync(a => a.Id == request.ActivityId, cancellationToken);

                RecMember member = await this.context.Members
                    .Include(m => m.TelephoneNumbers)
                    .Include(m => m.EmailAddresses)
                    .FirstOrDefaultAsync(m => m.FirstName.ToUpper() == request.FirstName.ToUpper() && m.LastName.ToUpper() == request.LastName.ToUpper());

                if (member == null)
                {
                    member = new RecMember
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName
                    };

                    this.context.Members.Add(member);
                    await this.context.SaveChangesAsync();
                }

                NotificationRequest nr = new NotificationRequest();
                nr.Member = member;
                nr.Activity = activity;
                nr.RequestedOn = DateTime.Now;

                EmailAddress email;
                if (!string.IsNullOrEmpty(request.EmailAddress))
                {
                    email = member.EmailAddresses.FirstOrDefault(e => e.Address.ToUpper() == request.EmailAddress.ToUpper());
                    if (email == null)
                    {
                        member.AddEmailAddress(request.EmailAddress);
                        await this.context.SaveChangesAsync();
                    }

                    nr.IsContactByEmail = true;
                }

                TelephoneNumber phone;
                if (!string.IsNullOrEmpty(request.PhoneNumber))
                {
                    phone = member.TelephoneNumbers.FirstOrDefault(t => t.Number == request.PhoneNumber);
                    if (phone == null)
                    {
                        member.AddNumber(request.PhoneNumber);
                        await this.context.SaveChangesAsync();
                    }

                    nr.IsContactByPhone = true;
                }

                this.context.NotificationRequests.Add(nr);
                await this.context.SaveChangesAsync();
                
            }
        }
    }
}
