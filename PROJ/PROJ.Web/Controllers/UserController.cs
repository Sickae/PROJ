using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.DTOs;
using PROJ.Logic.Identity.Managers;
using PROJ.Logic.UnitOfWork;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PROJ.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager _signInManager;
        private readonly ITaskGroupManager _taskGroupManager;
        private readonly ITaskManager _taskManager;
        private readonly UserRepository _userRepository;
        private readonly IUserManager _userManager;

        public UserController(SignInManager signInManager, ITaskGroupManager taskgorupManager, ITaskManager taskManager, IUserManager userManager, UserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _taskGroupManager = taskgorupManager;
            _taskManager = taskManager;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult TestCreate()
        {
            var taskGroup = new TaskGroupDTO
            {
                Name = "test project",
                Tasks = new List<TaskDTO>
                {
                    new TaskDTO
                    {
                        Name = "Test 1"
                    },
                    new TaskDTO
                    {
                        Name = "Test 2"
                    }
                }
            };
            _taskGroupManager.Create(taskGroup);
            return Json(true);
        }
    }
}