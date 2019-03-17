using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using PROJ.Shared.Enums;

namespace PROJ.Web.Controllers
{
    [Authorize]
    public class TaskGroupController : Controller
    {
        private readonly TaskGroupRepository _taskGroupRepository;
        private readonly ITaskGroupManager _taskGroupManager;
        private readonly ITaskManager _taskManager;

        public TaskGroupController(TaskGroupRepository taskGroupRepository, ITaskGroupManager taskGroupManager, ITaskManager taskManager)
        {
            _taskGroupRepository = taskGroupRepository;
            _taskGroupManager = taskGroupManager;
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
                TaskGroup = group,
                Priority = Priority.Normal
            };

            _taskManager.Create(task);
            return Json(true);
        }

        public IActionResult Rename(int groupId, string name)
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

            group.Name = name;
            _taskGroupManager.Save(group);

            return Json(true);
        }
    }
}