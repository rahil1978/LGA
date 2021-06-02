using System.Threading.Tasks;

namespace Lga.Id.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
