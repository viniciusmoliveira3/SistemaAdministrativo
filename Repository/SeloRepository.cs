using Colex.Context;
using Colex.Interfaces;
using Colex.Models;
using Microsoft.EntityFrameworkCore;

namespace Colex.Repository
{
    public class SeloRepository : RepositoryBase<Selo>, ISeloRepository
    {
        public SeloRepository(BaseContext context) : base(context)
        {
        }

        public List<Selo> GetAllComplete()
        {
            List<Selo> selo = Db.Set<Selo>()
                .Include(s => s.Fornecedor)
                .ToList();
            return selo;

        }
    }
}
