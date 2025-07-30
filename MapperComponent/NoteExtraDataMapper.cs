using ApplicationComponent;
using ApplicationComponent.DTOs;
using RepositoryComponent.ExtraData;

namespace MapperComponent
{
    public class NoteExtraDataMapper : IMapper<NoteDTO, NoteExtraData>
    {
        public NoteExtraData Map(NoteDTO data)
        => new NoteExtraData()
        {
            Color = data.Color,
        };
    }
}
