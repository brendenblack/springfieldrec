using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SpringfieldRecMvc.Features.Activities
{
    [Route("activities")]
    public class ActivitiesController : Controller
    {
        private readonly IMediator mediator;

        public ActivitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Details(Details.Query query)
        {
            var model = await this.mediator.Send(query);
            return View(model);
        }

        [HttpPost("details/{activityId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Details.Command command)
        {
            await this.mediator.Send(command);

            return this.RedirectToActionJson(nameof(Details));
            // return RedirectToAction("Details", command.ActivityId);
        }
    }
}