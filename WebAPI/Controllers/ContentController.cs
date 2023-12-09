using DAL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        [HttpPost]
        [Route("setghazaldetails")]
        public async Task<int> SetContentData(EntContentDetails eup)
        {
            Console.WriteLine("Content Controller Reached");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@ghazalname", eup.ContentName),
                 };
                var Sd = await MyCrud.CRUD("SP_SetGhazalDetails", sp);

                Console.WriteLine($"CONTENTCONTROLLER {Sd}");

                return Convert.ToInt32(Sd);
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("postghazalverses")]
        public async Task PostVerses(EntVerse eup)
        {
            Console.WriteLine("HEY RREACH");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("@ghazalid", eup.ContentId),
                    new SqlParameter("@verse1", eup.Verse1),
                    new SqlParameter("@verse2", eup.Verse2)
                 };

                await MyCrud.CRUD("SP_PostGhazalVerses", sp);
            }
            else
            {
                Console.WriteLine("GHAZAL VERSE LIST IS EMPTY");
            }
        }

        [HttpPost]
        [Route("setnazamdetails")]
        public async Task<int> SetNazamDetails(EntContentDetails eup)
        {
            try
            {
                if (eup != null)
                {
                    SqlParameter[] sp = new SqlParameter[]
                    {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@nazamname", eup.ContentName),
                    };
                    var response = await MyCrud.CRUD("SP_SetNazamDetails", sp);
                    Console.WriteLine($"{response} CONTENT");

                    return Convert.ToInt32(response);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} ERROR IN NAZAM DETAILS");
                return -1;
            }
        }

        [HttpPost]
        [Route("postnazamverses")]
        public async Task PostNazamVerses(EntVerse eup)
        {
            Console.WriteLine("HEY RREACH");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("@nazamid", eup.ContentId),
                    new SqlParameter("@verse1", eup.Verse1),
                    new SqlParameter("@verse2", eup.Verse2)
                 };

                await MyCrud.CRUD("SP_PostNazamVerses", sp);
            }
            else
            {
                Console.WriteLine("Nazam VERSE LIST IS EMPTY");
            }
        }

        [HttpPost]
        [Route("postphrases")]
        public async Task PostPhrases(EntPhrases eup)
        {
            Console.WriteLine("HEY RREACH");
            if (eup != null)
            {
                SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("@uid", eup.UId),
                    new SqlParameter("@verse1", eup.Verse1),
                    new SqlParameter("@verse2", eup.Verse2)
                 };

                await MyCrud.CRUD("SP_PostPhrases", sp);
            }
            else
            {
                Console.WriteLine("Phrase Verses are EMPTY");
            }
        }
    }
}
