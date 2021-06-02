using Lga.Id.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lga.Id.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LgaIdDatabaseContext _context;

        public UnitOfWork(LgaIdDatabaseContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
