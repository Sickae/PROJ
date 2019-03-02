using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using System.Collections.Generic;

namespace PROJ.Logic.Managers.Interfaces
{
    public interface IManagerBase<TEntity, TDto> : IManagerBase where TEntity : Entity where TDto : DTOBase
    {
        TDto Get(int id);
        IList<TDto> GetAll();
        int Save(TDto dto);
    }

    public interface IManagerBase { }
}
