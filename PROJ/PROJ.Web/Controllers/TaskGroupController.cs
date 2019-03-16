using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;

namespace PROJ.Web.Controllers
{
    [Authorize]
    public class TaskGroupController : Controller
    {
        private readonly TaskGroupRepository _taskGroupRepository;
        private readonly ITaskManager _taskManager;

        public TaskGroupController(TaskGroupRepository taskGroupRepository, ITaskManager taskManager)
        {
            _taskGroupRepository = taskGroupRepository;
            _taskManager = taskManager;
        }

        public IActionResult AddNewTask(int groupId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(false);
            }

            var group = _taskGroupRepository.Get(groupId);

            if (group == null)
            {
                return Json(false);
            }

            var task = new TaskDTO
            {
                Name = name,
                TaskGroup = group
            };

            _taskManager.Create(task);
            return Json(true);
        }
    }
}