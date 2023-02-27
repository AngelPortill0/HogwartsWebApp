using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using HogwartsWebApp.DataAccess;

namespace HogwartsWebApp.BusinessLogic
{
    ///<summary>
    ///Controlador para el modelo de StudentHistory
    ///</summary>
    ///<remarks>
    ///Añade, Consulta, Actualiza y Elimina solicitudes del modelo
    ///</remarks>
    [EnableCors("HogwartsCorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentHistoryController : Controller
    {
        public HogwartsContext databaseContext;

        public StudentHistoryController(HogwartsContext context)
        {
            databaseContext = context;
        }

        ///<summary>
        ///API para obtener todas las solicitudes de estudiantes
        ///</summary>
        ///<remarks>
        ///Realiza una consulta que devuelve el Status Code de la request y los datos de las solicitudes de estudiantes
        ///</remarks>
        [HttpGet]
        [Route("List")]
        public IActionResult ListStudentHistory()
        {
            List<StudentHistory> studentHistory = new List<StudentHistory>();

            try
            {
                studentHistory = databaseContext.StudentHistories.ToList();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = studentHistory });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }

        ///<summary>
        ///API para obtener la solicitud de un estudiante por su ID de historial
        ///</summary>
        ///<remarks>
        ///Realiza una consulta que devuelve el Status Code de la request y la solicitud del estudiante 
        ///</remarks>
        [HttpGet]
        [Route("Consult/{studentHistoryId:int}")]
        public IActionResult ConsultStudentHistory(int studentHistoryId)
        {
            StudentHistory? studentHistory = databaseContext.StudentHistories.Find(studentHistoryId);

            if (Validators.isRetrievedData(studentHistory) == false)
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            try
            {
                studentHistory = databaseContext.StudentHistories
                    .Where(s => s.StudentHistoryId == studentHistoryId)
                    .FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = studentHistory });
            }
            catch
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "Error inesperado" });
            }
        }

        ///<summary>
        ///API para actualizar la solicitud de Ingreso
        ///</summary>
        ///<remarks>
        ///Devuelve el Status Code de la request y un mensaje del resultado obtenido
        ///</remarks>
        [HttpPut]
        [Route("UpdateHistory/{studentHistoryId:int}")]
        public IActionResult UpdateHistory(int studentHistoryId, [FromBody] StudentHistory studentHistory)
        {
            StudentHistory? StudentData = databaseContext.StudentHistories.Find(studentHistoryId);

            if (Validators.isRetrievedData(studentHistory) == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            try
            {
                StudentData.Student = studentHistory.Student == StudentData.Student ? StudentData.Student : studentHistory.Student;
                StudentData.House = studentHistory.House == StudentData.House ? StudentData.House : studentHistory.House;
                StudentData.Status = studentHistory.Status == StudentData.Status ? StudentData.Status : studentHistory.Status;
                StudentData.LastestUpdate = DateTime.Now;

                databaseContext.StudentHistories.Update(StudentData);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Solicitud de Ingreso actualizada con éxito" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }

        ///<summary>
        ///API para registrar la solicitud del estudiante
        ///</summary>
        ///<remarks>
        ///Realiza un insert de la data del estudiante y devuelve el Status Code de la request y un mensaje del resultado del proceso
        ///</remarks>
        [HttpPost]
        [Route("AddHistory/{studentId:int}")]
        public IActionResult AddHistory(int studentId, [FromBody] StudentHistory studentHistory)
        {
            var studentController = new StudentController(databaseContext);
            var studentExist = studentController.ConsultStudent(studentId);

            if (Validators.isRetrievedData(studentHistory) == false)
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            try
            {
                var date = DateTime.Now;
                studentHistory.RegistrationDate = date;
                studentHistory.LastestUpdate = date;

                databaseContext.StudentHistories.Add(studentHistory);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Alumno procesado" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error Inesperado" });
            }
        }

        [HttpDelete]
        [Route("Delete/{studentHistoryId:int}")]
        public IActionResult DeleteHistory(int studentHistoryId)
        {
            StudentHistory? StudentHistory = databaseContext.StudentHistories.Find(studentHistoryId);
            
            if (Validators.isRetrievedData(StudentHistory) == false)
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            try
            {
                databaseContext.StudentHistories.Remove(StudentHistory);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Historial eliminado con éxito" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }
    }
}
