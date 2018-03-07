using AutoMapper;
using ExamAce.Data;
using ExamAce.Data.Entities;
using ExamAce.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExamAce.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentController : Controller
    {
        private readonly IExamAceRepository _repository;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public StudentController(
            IExamAceRepository repository,
            ILogger<StudentController> logger,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                return Ok(_repository.GetStudentsInClassById(user.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get students: {ex}");
                return BadRequest("Failed to get students");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            try
            {
                var student = _repository.GetStudentById(id);

                if (student != null)
                    return Ok(_mapper.Map<Student, StudentViewModel>(student));
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get student: {ex}");
                return BadRequest("Failed to get student");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]StudentViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var newStudent = _mapper.Map<StudentViewModel, Student>(model);

                    _repository.AddEntity(newStudent);

                    if (_repository.SaveAll())
                    {
                        // Not real api url
                        return Created($"/api/students/{newStudent.UserId}", _mapper.Map<Student, StudentViewModel>(newStudent));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new student: {ex}");
                return BadRequest("Failed to save new student");
            }
            return BadRequest("Failed to save new student");
        }
    }
}
