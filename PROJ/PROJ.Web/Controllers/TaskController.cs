using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using PROJ.Shared.Enums;

namespace PROJ.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository _taskRepository;
        private readonly ITaskManager _taskManager;
        private readonly IAppContext _appContext;

        public TaskController(TaskRepository taskRepository, ITaskManager taskManager, IAppContext appContext)
        {
            _taskRepository = taskRepository;
            _taskManager = taskManager;
            _appContext = appContext;
        }

        public IActionResult Rename(int taskId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Task name cannot be empty."
                });
            }

            var task = _taskRepository.Get(taskId);

            if (task == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Task."
                });
            }

            task.Name = name;
            _taskManager.Save(task);

            return Json(new { success = true });
        }

        public IActionResult Delete(int taskId)
        {
            var task = _taskRepository.Get(taskId);

            if (task == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Task."
                });
            }

            _taskManager.Delete(task);

            return Json(new { success = true });
        }

        public IActionResult ToggleComplete(int taskId, bool state)
        {
            var task = _taskRepository.Get(taskId);

            if (task == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Task."
                });
            }

            task.IsCompleted = state;
            _taskManager.Save(task);

            return Json(new { success = true });
        }

        public IActionResult SetPriority(int taskId, Priority priority)
        {
            var task = _taskRepository.Get(taskId);

            if (task == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Task."
                });
            }

            task.Priority = priority;
            _taskManager.Save(task);

            return Json(new { success = true });
        }

        public IActionResult JoinTask(int taskId)
        {
            var task = _taskRepository.Get(taskId);
            var user = _appContext.CurrentUser;

            if (task == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Task."
                });
            }

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "You are not logged in."
                });
            }

            if (task.AssignedUsers.Any(x => x.Id == user.Id))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "You are already assigned to this Task."
                });
            }

            task.AssignedUsers.Add(user);
            _taskManager.Save(task);

            return Json(new { success = true });
        }

        public IActionResult LeaveTask(int taskId)
        {
            var task = _taskRepository.Get(taskId);
            var user = _appContext.CurrentUser;

            if (task == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid Task."
                });
            }

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "You are not logged in."
                });
            }

            var toRemove = task.AssignedUsers.FirstOrDefault(x => x.Id == user.Id);

            if (toRemove == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "You are not assigned to this Task."
                });
            }

            task.AssignedUsers.Remove(toRemove);
            _taskManager.Save(task);

            return Json(new { success = true });
        }

        public IActionResult Details(int taskId)
        {
            var task = _taskRepository.Get(taskId);

            if (task == null)
            {
                return null;
            }

            return PartialView("_Details", task);
        }

        public IActionResult Edit(TaskDTO model)
        {
            _taskManager.Save(model);

            return RedirectToAction(nameof(ProjectController.Show), "Project", new { id = model.TaskGroup.Project.Id });
        }
    }
}