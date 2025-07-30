using DomainComponent.Entities;
using DomainComponent.Interfaces;
using RepositoryComponent.ExtraData;
using RepositoryComponent.Models;

namespace RepositoryComponent
{
    public class NoteFactoriedRepository : IAddRepository<Note>
    {
        private NoteExtraData _extraData;
        private readonly ItemsDbContext _context;

        public NoteFactoriedRepository(ItemsDbContext context, NoteExtraData extraData)
        {
            _context = context;
            _extraData = extraData;
        }
        public async Task AddAsync(Note note)
        {
            var model = new NoteModel()
            {
                Message = note.Message,
                ItemId = note.ItemId,
                CreatedDate = DateTime.Now,
                Color = _extraData.Color,
            };

            await _context.NotesModel.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
