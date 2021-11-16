using System.Threading.Tasks;
using Blazored.LocalStorage;
using WebClient.Models;

namespace WebClient.Services
{
    public interface IStorageService
    {
        Task RemoveItemAsync(LocalStorage key);
        Task SetItemAsStringAsync(LocalStorage key, string data);
    }

    public class StorageService : IStorageService
    {
        private readonly ILocalStorageService _localStorageService;

        public StorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task RemoveItemAsync(LocalStorage key)
        {
            await _localStorageService.RemoveItemAsync(key.ToString());
        }

        public async Task SetItemAsStringAsync(LocalStorage key, string data)
        {
            await _localStorageService.SetItemAsStringAsync(key.ToString(), data);
        }
    }
}