using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class ChecklistTaskManager : ManagerBase<ChecklistTask, ChecklistTaskDTO>, IChecklistTaskManager
    {
        public ChecklistTaskManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
