using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Managers.Interfaces;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public abstract class Repository<TEntity, TDto> : IRepository<TEntity, TDto> where TEntity : Entity where TDto : DTOBase
    {
        protected readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public TDto Get(int id)
        {
            return Mapper.Map<TEntity, TDto>(_session.Get<TEntity>(id));
        }
    }
}
