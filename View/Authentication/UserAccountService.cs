using DAL;
using Entities;

namespace View.Authentication
{
    public class UserAccountService
    {
      
        public async Task<Entities.UserAccount>? GetByUserName(string _userName, string _password)

        {

            var ua = await DALUserAuth.Authenticate(_userName, _password);
            return ua;
        }

    }
}
