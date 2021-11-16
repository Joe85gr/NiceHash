using Library.Models;

namespace Library.Services
{
    public interface INiceHashService
    {
        Task<NiceHashData> GetAll(CancellationToken token = default);
    }
}