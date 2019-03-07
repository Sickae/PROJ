using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class ChecklistManager : ManagerBase<Checklist, ChecklistDTO>, IChecklistManager
    {
        public ChecklistManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
