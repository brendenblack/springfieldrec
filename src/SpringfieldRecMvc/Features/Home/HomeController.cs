using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SpringfieldRecMvc.Features.Home
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var model = await mediator.Send(new Index.Query());
            return View(model);
        }

        public async Task<IActionResult> Register(Register.Query query)
        {
            var model = await mediator.Send(query);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register.Command command)
        {
            await mediator.Send(command);

            return this.RedirectToActionJson(nameof(Index));
        }

        // TODO: delete in a future sprint
        // An endpoint used only for rapid prototype development
        [HttpPost]
        public async Task Seed(Seed.Command command)
        {
            await mediator.Send(command);
        }
    }
}