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
        public async Task SetContentData(EntContentDetails eup)
        {
            Console.WriteLine("HEY RREACH");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@contentname", eup.ContentName),
                    new SqlParameter("@contentarrangement", eup.ContentArrangement),
                    new SqlParameter("@contentid", eup.ContentId),
                    new SqlParameter("@contenttype", eup.ContentType)
                 };

                await MyCrud.CRUD("sp_SetContentDetails", sp);
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
                    new SqlParameter("@verse", eup.Verse)
                 };

                await MyCrud.CRUD("sp_PostVerse ", sp);
            }
        }
    }
}
