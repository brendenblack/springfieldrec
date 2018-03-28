using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SpringfieldRecMvc.Features.Contacts
{
    [Route("contacts")]
    public class ContactsController : Controller
    {
        private readonly IMediator mediator;

        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Index.Query query)
        {
            var model = await this.mediator.Send(query);

            return View(model);
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> Notifications(Notifications.Query query)
        {
            var model = await this.mediator.Send(query);

            return View(model);
        }



        


    }
}