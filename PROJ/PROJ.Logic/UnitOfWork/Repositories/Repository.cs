using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Managers.Interfaces;
using System;

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
            var entity = _session.Get<TEntity>(id);

            if (entity.IsDeleted)
            {
                throw new InvalidOperationException("This entity is deleted, cannot be used.");
            }

            return Mapper.Map<TEntity, TDto>(_session.Get<TEntity>(id));
        }

        protected IQueryOver<TEntity, TEntity> GetQuery()
        {
            return _session.QueryOver<TEntity>().Where(x => !x.IsDeleted);
        }
    }
}
