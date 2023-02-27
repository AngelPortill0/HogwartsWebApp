using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

using HogwartsWebApp.DataAccess;

namespace HogwartsWebApp.BusinessLogic
{
    [EnableCors("HogwartsCorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : Controller
    {
        public HogwartsContext databaseContext;

        public HouseController(HogwartsContext context)
        {
            databaseContext = context;
        }

        ///<summary>
        ///API para obtener todas las casas de Hogwarts
        ///</summary>
        ///<remarks>
        ///Realiza una consulta que devuelve el Status Code de la request y los datos de las casas de Hogwarts
        ///</remarks>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("List")]
        public IActionResult ListHouses()
        {
            List<House> houses = new List<House>();

            try
            {
                houses = databaseContext.Houses.ToList();
                return StatusCode(StatusCodes.Status200OK, new { houses });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }
    }
}
