using DomainComponent.Interfaces;
using RepositoryComponent.Models;

namespace RepositoryComponent
{
    public class NoteMapperRepository : IAddRepository<NoteModel>
    {
        private readonly ItemsDbContext _context;

        public NoteMapperRepository(ItemsDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(NoteModel model)
        {
            _context.NotesModel.Add(model);
            await _context.SaveChangesAsync();
        }
    }
}
