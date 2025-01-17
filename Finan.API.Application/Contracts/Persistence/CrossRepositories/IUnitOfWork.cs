using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.API.Domain.Estudents;

namespace Finan.API.Application.Contracts.Persistence.CrossRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<StudentsEntity> Students { get; }
        Task<int> CompleteAsync();
    }
}
