using DomainComponent.Entities;
using DomainComponent.Interfaces;
using RepositoryComponent.ExtraData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryComponent.Factories
{
    public class NoteRepositoryFactory : IRepositoryFactory<IAddRepository<Note>, NoteExtraData>
    {
        private readonly ItemsDbContext _context;

        public NoteRepositoryFactory(ItemsDbContext context)
        {
            _context = context;
        }
        public IAddRepository<Note> create(NoteExtraData extraData)
         => new NoteFactoriedRepository(_context, extraData);
    }
}
