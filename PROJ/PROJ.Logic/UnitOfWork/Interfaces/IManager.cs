using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.UnitOfWork.Interfaces
{
    public interface IManager<TEntity, TDto> where TEntity : Entity where TDto : DTOBase
    {
        int Save(TDto dto);
        int Create(TDto dto);
        void Delete(TDto dto);
        void Delete(int id);
    }
}