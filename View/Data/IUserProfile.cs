using Entities;

namespace View.Data
{
    public interface IUserProfile
    {

        Task SignupUser(EntUserProfile eo);

    }
}
