using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.API.Application.Contracts.Persistence.CrossRepositories;
using Finan.API.Domain.Estudents;
using Finan.API.Persistence.DBContext;

namespace Finan.API.Persistence.Repositories.CrossRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanDbContext _context;

        public IGenericRepository<StudentsEntity> Students { get; private set; }

        public UnitOfWork(FinanDbContext context)
        {
            _context = context;

            // Instanciar repositorios genéricos
            Students = new GenericRepository<StudentsEntity>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
