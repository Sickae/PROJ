using Microsoft.Extensions.DependencyInjection;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using System;
using System.Linq;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class TaskGroupManager : ManagerBase<TaskGroup, TaskGroupDTO>, ITaskGroupManager
    {
        private readonly ITaskManager taskManager;

        public TaskGroupManager(IUnitOfWork unitOfWork, ITaskManager taskManager) : base(unitOfWork)
        {
            this.taskManager = taskManager;
        }

        public override int Create(TaskGroupDTO dto)
        {
            var unsavedTasks = dto.Tasks.Where(x => x.Id == 0).ToList();
            foreach (var task in unsavedTasks)
            {
                dto.Tasks.Remove(task);
                task.Id = taskManager.Create(task);
                dto.Tasks.Add(task);
            }

            return base.Create(dto);
        }
    }
}
