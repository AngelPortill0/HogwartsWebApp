using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using HogwartsWebApp.DataAccess;

namespace HogwartsWebApp.BusinessLogic
{
    [EnableCors("HogwartsCorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        public HogwartsContext databaseContext;

        public StatusController(HogwartsContext context)
        {
            databaseContext = context;
        }

        ///<summary>
        ///API para obtener los tipos de estatus de los estudiantes de Hogwarts
        ///</summary>
        ///<remarks>
        ///Realiza una consulta que devuelve el Status Code de la request y los datos de los status de estudiantes de Hogwarts
        ///</remarks>
        [HttpGet]
        [Route("List")]
        public IActionResult ListStatus()
        {
            List<Status> statuses = new List<Status>();

            try
            {
                statuses = databaseContext.Statuses.ToList();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = statuses });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }

        ///<summary>
        ///API para agregar un status nuevo
        ///</summary>
        ///<remarks>
        ///Realiza un insert del status y devuelve el Status Code de la request y un mensaje del resultado del proceso
        ///</remarks>
        [HttpPost]
        [Route("Add")]
        public IActionResult AddStatus([FromBody] Status status)
        {
            if (!Validators.isChangeableName(status.Description)) {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Solo se permiten caracteres, verifique" });
            }

            try
            {
                databaseContext.Statuses.Add(status);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Estatus agregado con exito" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }

        [HttpDelete]
        [Route("Delete/{statusId:int}")]
        public IActionResult DeleteStatus(int statusId)
        {
            Status? statusData = databaseContext.Statuses.Find(statusId);

            if (Validators.isRetrievedData(statusData) == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            try
            {
                databaseContext.Statuses.Remove(statusData);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Estatus eliminado con éxito" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }
    }
}
