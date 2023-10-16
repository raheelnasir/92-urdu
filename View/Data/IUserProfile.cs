using System.Threading.Tasks;
using Entities;

namespace View.Data
{
    public interface IUserProfile
    {
        Task<SignupResponse> SignupUser(EntUserProfile eup);
        Task<SignupResponse> CreateUser(EntUserProfile cup);
        Task<List<EntUserProfile>> GetUsersData(string role);

    }
}
