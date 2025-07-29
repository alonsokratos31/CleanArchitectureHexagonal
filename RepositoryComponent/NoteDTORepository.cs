using ApplicationComponent.DTOs;
using DomainComponent.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryComponent.Models;

namespace RepositoryComponent
{
    public class NoteDTORepository : ICommonRepository<NoteDTO>
    {
        private readonly ItemsDbContext _context;

        public NoteDTORepository(ItemsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NoteDTO noteDTO)
        {
            var model = new NoteModel()
            {
                Message = noteDTO.Message,
                ItemId = noteDTO.ItemId,
                Color = noteDTO.Color,
                CreatedDate = DateTime.Now
            };

            _context.NotesModel.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NoteDTO>> GetAllAsync()
        {
            return await _context.NotesModel
                .Select(n => new NoteDTO
                {
                    Id = n.Id,
                    Message = n.Message,
                    ItemId = n.ItemId,
                    Color = n.Color,
                }).ToListAsync();
        }
    }
}
