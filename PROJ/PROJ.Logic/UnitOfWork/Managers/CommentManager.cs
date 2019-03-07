using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Logic.UnitOfWork.Managers.Interfaces;
using System;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public class CommentManager : ManagerBase<Comment, CommentDTO>, ICommentManager
    {
        public CommentManager(IUnitOfWork unitOfWork, IAppContext appContext) : base(unitOfWork, appContext)
        { }

        public override int Create(CommentDTO dto)
        {
            if (!_appContext.UserId.HasValue)
            {
                throw new InvalidOperationException("There is no logged in user");
            }

            dto.Author = new UserDTO { Id = _appContext.UserId.Value };

            return base.Create(dto);
        }
    }
}
