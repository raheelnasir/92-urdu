using Entities;
using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        [HttpPost]
        [Route("signupuser")]
        public async Task<IActionResult> SignupUser(EntUserProfile eup)
        {
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@username", eup.UserName),
                    new SqlParameter("@password", eup.Password),
                    new SqlParameter("@role", eup.Role),
                    new SqlParameter("@firstname", eup.FirstName),
                    new SqlParameter("@lastname", eup.LastName),
                    new SqlParameter("@emailaddress", eup.EmailAddress),
                    new SqlParameter("@phonenumber", eup.PhoneNumber),
                    new SqlParameter("@dateofbirth", eup.DateOfBirth.ToString()),
                    new SqlParameter("@city", eup.City),
                    new SqlParameter("@area", eup.Area),
                    new SqlParameter("@location", eup.Location),
                    new SqlParameter("@isactive", eup.IsActive)
                };

                string data = await MyCrud.CRUD("sp_SignupUser", sp);

                if (!string.IsNullOrEmpty(data))
                {
                    // Account created successfully
                 //   Console.WriteLine($"{data} OUTPUT PARAMETER OUTPUT");
                    return Ok(new { Message = data, Data = "sda" });
                }
                else
                {
                    // Handle the case when outputMessage is null (no error message)
                    return BadRequest(new { Message = "User registration failed", Data = "sad" });
                }
            }
            else
            {
                // Handle the case when eup is null
                return BadRequest(new { Message = "Invalid input", Data = "sad2" });
            }
        }


        [HttpPost]
        [Route("createaccount")]
        public async Task<IActionResult> CreateUser(EntUserProfile eup)
        {
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@username", eup.UserName),
                    new SqlParameter("@password", eup.Password),
                    new SqlParameter("@role", "User"),
                    new SqlParameter("@firstname", eup.FirstName ?? (object)DBNull.Value),
                    new SqlParameter("@lastname", eup.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@emailaddress", eup.EmailAddress ?? (object)DBNull.Value),
                    new SqlParameter("@phonenumber", String.Empty),
                    new SqlParameter("@dateofbirth", String.Empty),
                    new SqlParameter("@city", String.Empty),
                    new SqlParameter("@area", String.Empty),
                    new SqlParameter("@location", String.Empty),
                    new SqlParameter("@isactive", 1)
                };
                string data = await MyCrud.CRUD("sp_CreateUser", sp);

                if (!string.IsNullOrEmpty(data))
                {
                    // Account created successfully
               //     Console.WriteLine($"{data} OUTPUT PARAMETER OUTPUT");
                    return Ok(new { Message = data, Data = "sda" });
                }
                else
                {
                    // Handle the case when outputMessage is null (no error message)
                    return BadRequest(new { Message = "User registration failed", Data = "sad" });
                }
            }
            else
            {
                // Handle the case when eup is null
                return BadRequest(new { Message = "Invalid input", Data = "sad2" });
            }
        }


        [HttpGet]
        [Route("getusersdata")]
        public async Task<IActionResult> GetUsersData([FromQuery] string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return BadRequest("Role parameter is required.");
            }
         //   Console.WriteLine(role);
            var response = await DALUsersData.GetUsersData(role);

            if (response == null)
            {
                return NotFound();
            }
            else
            {
              //  Console.WriteLine($"Controller: {response}");
                return Ok(response);
            }
        }

        [HttpPut]
        [Route("updateusersprofiledata")]
        public async Task UpdateUsersProfileData(EntUserProfile eup)
        {
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@username", eup.UserName),
                    new SqlParameter("@role", eup.Role),
                    new SqlParameter("@isactive", eup.IsActive)
                };
                await MyCrud.CRUD("sp_UpdateUsersProfileData", sp);
            //    Console.WriteLine($" Controller {sp}");

            }
        }
       
        
        [HttpPut]
        [Route("deleteusersprofiledata")]
        public async Task DeleteUsersProfileData(EntUserProfile eup)
        {
            Console.WriteLine($"Delete Controller");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@role", eup.Role)
                };
                await MyCrud.CRUD("sp_DeleteUsersProfile", sp);
           //     Console.WriteLine($" Controller {sp}");

            }
        }
    }
}
