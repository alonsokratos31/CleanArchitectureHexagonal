using ApplicationComponent;
using ApplicationComponent.DTOs;
using RepositoryComponent.Models;

namespace MapperComponent
{
    public class NoteModelMapper : IMapper<NoteDTO, NoteModel>
    {
        public NoteModel Map(NoteDTO dto)
        => new NoteModel()
        {
            Id = dto.Id,
            ItemId = dto.ItemId,
            Message = dto.Message,
            CreatedDate = DateTime.Now,
            Color = dto.Color,
        };
    }
}
