using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.Managers.Interfaces;

namespace PROJ.Logic.UnitOfWork.Repositories
{
    public abstract class Repository<TEntity, TDto> : IRepository<TEntity, TDto> where TEntity : Entity where TDto : DTOBase
    {
        protected readonly ISession _session;
        protected readonly IAppContext _appContext;

        public Repository(ISession session, IAppContext appContext)
        {
            _session = session;
            _appContext = appContext;
        }

        public virtual TDto Get(int id)
        {
            var entity = _session.Get<TEntity>(id);

            if (entity == null || entity.IsDeleted)
            {
                return null;
            }

            return Mapper.Map<TEntity, TDto>(_session.Get<TEntity>(id));
        }

        protected IQueryOver<TEntity, TEntity> GetQuery()
        {
            return _session.QueryOver<TEntity>().Where(x => !x.IsDeleted);
        }
    }
}
