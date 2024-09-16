using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly IFileService fileService;
        private readonly AppDbContext context;

        public GamesService(IFileService fileService,AppDbContext context)
        {
            this.fileService = fileService;
            this.context = context;
        }
        public async Task Create(CreateGameVM Model)
        {
          var FilePath= await fileService.UploadFileAsync(Model.Cover, FileSettings.ImagePath);
            if (!string.IsNullOrEmpty(FilePath))
            {
                Game game = new()
                {
                    Name = Model.Name,
                    Description = Model.Description,
                    Cover = FilePath,
                    CatID=Model.CatID,
                    GameDevices = Model.GameDevicesID.Select(g => new GameDevice { DeviceID = g }).ToList()
                };
                context.Add(game);
                context.SaveChanges();
            }
        }

        public bool Delete(int Id)
        {
            var game = context.Games.FirstOrDefault(g=>g.ID==Id);
            if (game is null) return false;
            context.Remove(game);
            var effectiveRows=context.SaveChanges();
            if (effectiveRows > 0)
            {
                //delete image from server
                fileService.DeleteFileAsync(game.Cover);
                return true;
            }
            return false;
        }

        public async Task<Game?> Edit(EditGameVM model)
        {
            var game = context.Games.Include(g => g.GameDevices).FirstOrDefault(g => g.ID == model.ID);
            if (game == null) return null;

            // Update game properties
            game.Name = model.Name;
            game.Description = model.Description;
            game.CatID = model.CatID;

            // Update game devices
            game.GameDevices.Clear();
            game.GameDevices = model.GameDevicesID.Select(d => new GameDevice { DeviceID = d, GameID = game.ID }).ToList();

            // Handle cover file update
            string? oldCoverPath = game.Cover;
            if (model.Cover != null)
            {
                game.Cover = await fileService.UploadFileAsync(model.Cover, FileSettings.ImagePath);
            }

            context.Update(game);
            var effectedRows = await context.SaveChangesAsync();
            if (effectedRows > 0)
            {
                // Delete the old cover image if a new one was uploaded
                if (model.Cover != null && !string.IsNullOrEmpty(oldCoverPath))
                {
                    await fileService.DeleteFileAsync(oldCoverPath);
                }
                return game;
            }
            else
            {
                // If saving changes failed and a new cover was uploaded, delete the new cover
                if (model.Cover != null)
                {
                    await fileService.DeleteFileAsync(game.Cover);
                }
                return null;
            }
        }


        public IEnumerable<Game> GetAll()
        {
            return context.Games.AsNoTracking().Include(g=>g.Category).Include(g=>g.GameDevices).ThenInclude(d=>d.Device).ToList();
        }

        public Game? GetById(int Id)
        {
          return context.Games.Include(g=>g.Category).Include(g=>g.GameDevices).ThenInclude(d=>d.Device).FirstOrDefault(g=>g.ID==Id);

        }
     
    }
}
