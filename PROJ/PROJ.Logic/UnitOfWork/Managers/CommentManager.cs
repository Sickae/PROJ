using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class CommentManager : ManagerBase<Comment, CommentDTO>, ICommentManager
    {
        public CommentManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
