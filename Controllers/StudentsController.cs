using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Back.Data;
using Back.DTO;
using System.Linq;
using Back.Data;
using Back.DTO;
using Back.Models;

namespace Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly Context _context;

        public StudentsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<studentsDTO>> GetStudents()
        {
            var student = from students in _context.students
            join students_description in _context.students_description on students.id equals students_description.students_id
            select new studentsDTO
            {
                Students_id = students.id,
                Firstname = students_description.firstname,
                Lastname = students_description.lastname,
                Adress= students_description.adress,
                Age = students_description.age,
                Country = students_description.country
            };

            return Ok(student);
        }

        [HttpGet("{id}")]
        public ActionResult<studentsDTO> GetStudents_byId(int id)
        {
            var student = from students in _context.students
            join students_description in _context.students_description on students.id equals students_description.students_id
            select new studentsDTO
            {
                Students_id = students.id,
                Firstname = students_description.firstname,
                Lastname = students_description.lastname,
                Adress= students_description.adress,
                Age = students_description.age,
                Country = students_description.country
            };

            var book_by_id = student.ToList().Find(x => x.Students_id == id);

            if (book_by_id == null)
            {
                return NotFound();
            }
            return book_by_id;
        }
    }
}