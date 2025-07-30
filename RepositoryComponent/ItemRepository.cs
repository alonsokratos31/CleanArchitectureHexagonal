using DomainComponent.Entities;
using DomainComponent.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryComponent.Models;

namespace RepositoryComponent
{
    public class ItemRepository : IRepository, ICompleteRepository, IGetRepository<Item>
    {
        private readonly ItemsDbContext _dbContext;

        public ItemRepository(ItemsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Item item)
        {
            var model = new ItemModel()
            {
                Title = item.Title,
                IsCompleted = item.IsCompleted,
                CreatedDate = DateTime.Now,
            };

            _dbContext.ItemsModel.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Complete(int id)
        {
            var model = await _dbContext.ItemsModel.FindAsync(id);

            if (model == null)
                throw new InvalidOperationException($"No se ha encontrado el item {id}.");

            model.IsCompleted = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _dbContext.ItemsModel.Select(e => new Item(e.Id, e.Title, e.IsCompleted)).ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            var model = await _dbContext.ItemsModel.FindAsync(id);

            if(model != null)
            {
                var item = new Item(model.Id, model.Title, model.IsCompleted);
                return item;
            }

            return null;
        }
    }
}
