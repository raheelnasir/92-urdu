using DAL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : Controller
    {
        [HttpPost]
        [Route("setcontentdetails")]
        public async Task<int> SetContentData(EntContentDetails eup)
        {
            Console.WriteLine("HEY RREACH");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@contentname", eup.ContentName),
                    new SqlParameter("@contenttype", eup.ContentType)
                 };
                var Sd=  await MyCrud.CRUD("sp_SetContentDetails", sp);
                Console.WriteLine($"CONTENTCONTROLLER{Sd}");

                return Convert.ToInt32(Sd) ;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("postverses")]
        public async Task PostVerses(EntVerse eup)
        {
            Console.WriteLine("HEY RREACH");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("@contentid", eup.ContentId),
                    new SqlParameter("@verse1", eup.Verse1),
                    new SqlParameter("@verse2", eup.Verse2)

                 };

                await MyCrud.CRUD("sp_PostVerse ", sp);
            }
        }
    }
}
