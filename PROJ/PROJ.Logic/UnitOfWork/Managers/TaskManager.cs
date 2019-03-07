using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class TaskManager : ManagerBase<Task, TaskDTO>, ITaskManager
    {
        public TaskManager(IUnitOfWork unitOfWork, IAppContext appContext) : base(unitOfWork, appContext)
        { }
    }
}
