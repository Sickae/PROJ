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
        private readonly ICommentManager _commentManager;
        private readonly IAppContext _appContext;

        public TaskController(TaskRepository taskRepository, ITaskManager taskManager, ICommentManager commentManager, IAppContext appContext)
        {
            _taskRepository = taskRepository;
            _taskManager = taskManager;
            _commentManager = commentManager;
            _appContext = appContext;
        }

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpGet]
        public IActionResult Details(int taskId)
        {
            var task = _taskRepository.Get(taskId);

            if (task == null)
            {
                return null;
            }

            return PartialView("_Details", task);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(TaskDTO model)
        {
            var task = _taskRepository.Get(model.Id);

            model.Comments = task.Comments;
            _taskManager.Save(model);

            return RedirectToAction(nameof(ProjectController.Show), "Project", new { id = model.TaskGroup.Project.Id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddNewComment(int taskId, string commentText)
        {
            if (string.IsNullOrEmpty(commentText) || string.IsNullOrWhiteSpace(commentText))
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Comment cannot be empty."
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

            var comment = new CommentDTO
            {
                Author = new UserDTO { Id = _appContext.UserId.Value },
                Text = commentText,
                Task = task
            };

            var id = _commentManager.Create(comment);

            return Json(new
            {
                success = true,
                id
            });
        }
    }
}