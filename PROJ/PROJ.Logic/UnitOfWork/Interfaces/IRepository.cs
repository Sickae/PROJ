using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;

namespace PROJ.Logic.Managers.Interfaces
{
    public interface IRepository<TEntity, TDTo> where TEntity : Entity where TDTo : DTOBase
    {
        TDTo Get(int id);
    }
}
