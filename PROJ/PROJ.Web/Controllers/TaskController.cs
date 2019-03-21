using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using PROJ.Logic.UnitOfWork.Repositories;
using PROJ.Shared.Enums;

namespace PROJ.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository _taskRepository;
        private readonly ITaskManager _taskManager;

        public TaskController(TaskRepository taskRepository, ITaskManager taskManager)
        {
            _taskRepository = taskRepository;
            _taskManager = taskManager;
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
    }
}