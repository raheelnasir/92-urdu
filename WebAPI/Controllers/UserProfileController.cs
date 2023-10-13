using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using DAL;
using System.Data.SqlClient;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : Controller
    {
        [HttpPost]
        [Route("signupuser")]
        public async Task SignupUser(EntUserProfile eup)
        {
            if(eup!=null)
            {
                SqlParameter[] sp = new SqlParameter[]
                {
                new SqlParameter("@uid",eup.UId),
                new SqlParameter("@username",eup.UserName),
                new SqlParameter("@password",eup.Password),
                new SqlParameter("@role",eup.Role),
                new SqlParameter("@firstname",eup.FirstName),
                new SqlParameter("@lastname",eup.LastName),
                new SqlParameter("@emailaddress",eup.EmailAddress),
                new SqlParameter("@phonenumber",eup.PhoneNumber),
                new SqlParameter("@dateofbirth",eup.DateOfBirth.ToString()),
                new SqlParameter("@city",eup.City),
                new SqlParameter("@area",eup.Area),
                new SqlParameter("@location",eup.Location),
                new SqlParameter("@isactive",eup.IsActive)
                };
                await MyCrud.CRUD("sp_SignupUser", sp);
            };
        }
    }
}
