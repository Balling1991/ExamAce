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
    }
}
