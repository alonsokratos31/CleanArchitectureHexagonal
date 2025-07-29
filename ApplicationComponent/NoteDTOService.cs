using ApplicationComponent.DTOs;
using DomainComponent.Interfaces;

namespace ApplicationComponent
{
    public class NoteDTOService : ICommonService<NoteDTO>
    {
        private readonly ICommonRepository<NoteDTO> _repository;

        public NoteDTOService(ICommonRepository<NoteDTO> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(NoteDTO noteDto)
        {
            await _repository.AddAsync(noteDto);
        }

        public async Task<IEnumerable<NoteDTO>> GetAsync()
        => await _repository.GetAllAsync();
    }
}
