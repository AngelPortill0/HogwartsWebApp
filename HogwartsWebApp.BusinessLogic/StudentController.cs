using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using HogwartsWebApp.DataAccess;

namespace HogwartsWebApp.BusinessLogic
{
    ///<summary>
    ///Controlador para el modelo de Students
    ///</summary>
    ///<remarks>
    ///Consulta, Actualiza y Elimina estudiantes del modelo
    ///</remarks>
    [EnableCors("HogwartsCorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        public HogwartsContext databaseContext;

        public StudentController(HogwartsContext context)
        {
            databaseContext = context;
        }

        ///<summary>
        ///API para obtener todos los estudiantes
        ///</summary>
        ///<remarks>
        ///Realiza una consulta que devuelve el Status Code de la request y los datos de los estudiantes
        ///</remarks>
        [HttpGet]
        [Route("List")]
        public IActionResult ListStudents()
        {
            List<Student> students = new List<Student>();

            try
            {
                students = databaseContext.Students.Include(s => s.StudentHistories).ToList();
                return StatusCode(StatusCodes.Status200OK, new {responseMessage = "OK", responseBody = students});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = ex });
            }
        }

        ///<summary>
        ///API para obtener a un estudiante por su ID de estudiante
        ///</summary>
        ///<remarks>
        ///Realiza una consulta que devuelve el Status Code de la request y los datos del estudiante si se encuentra inscrito
        ///</remarks>
        [HttpGet]
        [Route("Consult/{studentId:int}")]
        public IActionResult ConsultStudent(int studentId)
        {
            Student? student = databaseContext.Students.Find(studentId);

            if (Validators.isRetrievedData(student) == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            try
            {
                student = databaseContext.Students
                    .Include(history => history.StudentHistories)
                    .Where(s => s.StudentId == studentId)
                    .FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = student });
            }
            catch
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "Error inesperado" });
            }
        }

        ///<summary>
        ///API para inscribir a un estudiante
        ///</summary>
        ///<remarks>
        ///Realiza un insert de la data del estudiante y devuelve el Status Code de la request y un mensaje del resultado del proceso
        ///</remarks>
        [HttpPost]
        [Route("Enroll")]
        public IActionResult EnrollStudent([FromBody] Student student)
        {
            if (!
                (Validators.isChangeableName(student.Name) && 
                Validators.isChangeableName(student.LastName) && 
                Validators.isValidIdentityNumber(student.IdentityNumber) &&
                Validators.isValidAge(student.Age))
                )
            {
                return StatusCode(StatusCodes.Status200OK, new { responseBody = "Por favor, ingrese valores correctos en los campos" });
            }

            try
            {
                databaseContext.Students.Add(student);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Alumno inscrito, bienvenido a Hogwarts", student = student.StudentId });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseBody = "Error inesperado" });
            }
        }

        ///<summary>
        ///API para actualizar los datos de un estudiante
        ///</summary>
        ///<remarks>
        ///Devuelve el Status Code de la request y un mensaje del resultado obtenido
        ///</remarks>
        [HttpPut]
        [Route("UpdateInformation")]
        public IActionResult UpdateInformation([FromBody] Student? student)
        {
            Student? StudentData = databaseContext.Students.Find(student.StudentId);

            if (Validators.isRetrievedData(StudentData) == false)
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            if (!
                (Validators.isChangeableName(student.Name) &&
                Validators.isChangeableName(student.LastName) &&
                Validators.isValidIdentityNumber(student.IdentityNumber) &&
                Validators.isValidAge(student.Age))
                )
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "BadRequest", responseBody = "Datos suministrados no validos" });
            }
            
            try
            {
                StudentData.Name = student.Name;
                StudentData.LastName = student.LastName;
                StudentData.Age = student.Age;
                StudentData.IdentityNumber = student.IdentityNumber;

                databaseContext.Students.Update(StudentData);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Alumno actualizado con éxito" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }

        ///<summary>
        ///API para eliminar los datos de un estudiante
        ///</summary>
        ///<remarks>
        ///Devuelve el Status Code de la request y un mensaje del resultado obtenido
        ///</remarks>
        [HttpDelete]
        [Route("Delete/{studentId:int}")]
        public IActionResult DeleteStudent(int studentId)
        {
            Student? StudentData = databaseContext.Students.Find(studentId);

            if (Validators.isRetrievedData(StudentData) == false)
            {
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "BadRequest", responseBody = "Alumno no inscrito" });
            }

            try
            {
                databaseContext.Students.Remove(StudentData);
                databaseContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { responseMessage = "OK", responseBody = "Alumno eliminado con éxito" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Error inesperado" });
            }
        }
    }
}
