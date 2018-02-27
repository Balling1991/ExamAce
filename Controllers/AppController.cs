using ExamAce.Data;
using ExamAce.Data.Entities;
using ExamAce.Models;
using ExamAce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExamAce.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IExamAceRepository _repository;
        private readonly UserManager<User> _userManager;

        public AppController(IMailService mailService, IExamAceRepository repository, UserManager<User> userManager)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [HttpGet("contact")]
        // public IActionResult Contact()
        // {
        //     return View();
        // }

        // [HttpPost("contact")]
        // public IActionResult Contact(ContactViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _mailService.SendEmail("test@test.com", model.Subject, $"From:{ model.Name } - { model.Email }, Message: { model.Message }");
        //         ViewBag.UserMessage = "Mail Sent";
        //         ModelState.Clear();
        //     }

        //     return View();
        // }

        // [HttpGet("student")]
        // public async Task<IActionResult> Student()
        // {
        //     var user = await _userManager.GetUserAsync(HttpContext.User);

        //     var results = _repository.GetStudentsInClassById(user.Id);

        //     return View(results);
        // }
    }
}
