using Colex.Models;

namespace Colex.Interfaces
{
    public interface ISeloRepository : IRepositoryBase<Selo>
    {
        List<Selo> GetAllComplete();
    }
}
