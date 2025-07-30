﻿using DomainComponent.Entities;
using DomainComponent.Interfaces;

namespace ApplicationComponent
{
    public class CompleteItemService : ICompleteService
    {
        private ICompleteRepository _completeRepository;
        private IGetRepository<Item> _getRepository;

        public CompleteItemService(ICompleteRepository completeRepository, IGetRepository<Item> getRepository)
        {
            _completeRepository = completeRepository;
            _getRepository = getRepository;
        }

        public async Task Complete(int id)
        {
           var item = await _getRepository.GetByIdAsync(id);
            if (item == null) {
                throw new InvalidOperationException("No se encuentra el elemento");
            }

            await _completeRepository.Complete(id);
        }
    }
}
