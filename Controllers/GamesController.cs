using GameZone.FIlters;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{

    [Authorize]
    public class GamesController : Controller
    {
        private readonly AppDbContext context;
        private readonly ICategoriesService categoriesService;
        private readonly IDevicesService devicesService;
        private readonly IGamesService gamesService;

        public GamesController(AppDbContext context, ICategoriesService CategoriesService, IDevicesService devicesService, IGamesService gamesService)
        {
            this.context = context;
            this.categoriesService = CategoriesService;
            this.devicesService = devicesService;
            this.gamesService = gamesService;
        }
        //[MyFilter]
        public IActionResult Index()
        {
            var games = gamesService.GetAll();
            return View(games);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int ID)
        {
            var game = gamesService.GetById(ID);
            if (game is null) return NotFound();
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameVM game = new CreateGameVM()
            {
                Categories = categoriesService.GetSelectListItems(),
                GameDevices = devicesService.SelectListItems()
            };


            return View(game);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameVM game)
        {
            if (!ModelState.IsValid)
            {
                game.Categories = categoriesService.GetSelectListItems();

                game.GameDevices = devicesService.SelectListItems();

                return View(game);
            }
            await gamesService.Create(game);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int ID)
        {
            var game = gamesService.GetById(ID);
            if (game is null) return NotFound();

            EditGameVM gameVM = new EditGameVM()
            {
                ID = ID,
                Name = game.Name,
                Description = game.Description,
                CatID = game.CatID,
                GameDevicesID = game.GameDevices.Select(d => d.GameID).ToList(),
                Categories = categoriesService.GetSelectListItems(),
                GameDevices = devicesService.SelectListItems(),
                path = game.Cover
            };
            return View("Edit", gameVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameVM gameVM)
        {
            if (!ModelState.IsValid)
            {
                gameVM.Categories = categoriesService.GetSelectListItems();
                gameVM.GameDevices = devicesService.SelectListItems();
                return View(gameVM);
            }
            var game = await gamesService.Edit(gameVM);

            if (game is null) return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int ID)
        {
            var isDeleted=gamesService.Delete(ID);
          return  isDeleted? Ok():BadRequest();
        }

        //[HttpPost]
        //public IActionResult Delete(string ID)
        //{

        //}
    }
}
