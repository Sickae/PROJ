using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class TaskGroupManager : ManagerBase<TaskGroup, TaskGroupDTO>, ITaskGroupManager
    {
        public TaskGroupManager(IUnitOfWork unitOfWork, IAppContext appContext) : base(unitOfWork, appContext)
        { }
    }
}
