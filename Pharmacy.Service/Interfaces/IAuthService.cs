using System.Threading.Tasks;

namespace Pharmacy.Service.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string> GenerateToken(string username, string password);
    }
}
