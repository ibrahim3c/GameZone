using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesService
    {
        public  Task Create(CreateGameVM Game);
        public IEnumerable<Game> GetAll();
        public Game? GetById(int Id);
        public Task<Game?> Edit(EditGameVM Model);
        public bool Delete(int Id);

    }
}
