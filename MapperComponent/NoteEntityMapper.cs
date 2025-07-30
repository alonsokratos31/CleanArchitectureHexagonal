using ApplicationComponent;
using ApplicationComponent.DTOs;
using DomainComponent.Entities;

namespace MapperComponent
{
    public class NoteEntityMapper : IMapper<NoteDTO, Note>
    {
        public Note Map(NoteDTO dto)
            => new Note(dto.Id, dto.ItemId, dto.Message);
    }
}
