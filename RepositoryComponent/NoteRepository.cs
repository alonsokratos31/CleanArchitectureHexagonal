using DomainComponent.Entities;
using DomainComponent.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryComponent.Models;

namespace RepositoryComponent
{
    public class NoteRepository : ICommonRepository<Note>
    {
        private readonly ItemsDbContext _context;
        public NoteRepository(ItemsDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Note note)
        {
            var model = new NoteModel()
            {
                Message = note.Message,
                ItemId = note.ItemId,
                CreatedDate = DateTime.Now
            };

            _context.NotesModel.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return await _context.NotesModel
                .Select(n => new Note(n.Id, n.ItemId, n.Message))
                .ToListAsync();
        }
    }
}
