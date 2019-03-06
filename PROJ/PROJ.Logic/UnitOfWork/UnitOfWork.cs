using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Shared.Attributes;
using System;

namespace PROJ.Logic.UnitOfWork
{
    public abstract class UnitOfWork<TEntity, TDto> : IUnitOfWork<TEntity, TDto> where TEntity : Entity where TDto : DTOBase
    {
        private readonly ISession _session;

        public UnitOfWork(ISession session)
        {
            _session = session;
        }

        public int Create(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = Map(dto);
            entity.Id = 0;
            InTransaction(() => _session.Save(entity));

            return entity.Id;
        }

        public void Delete(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = Map(dto);
            DeleteInternal(entity);
        }

        public void Delete(int id)
        {
            var entity = _session.Get<TEntity>(id);
            DeleteInternal(entity);
        }

        public virtual int SaveChanges(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            TEntity entity = _session.Load<TEntity>(dto.Id);
            InTransaction(() =>
            {
                entity = Map(dto);
                entity.ModificationDate = DateTime.UtcNow;
                _session.Merge(entity);
            });

            return entity.Id;
        }

        private void DeleteInternal(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!Attribute.IsDefined(entity.GetType(), typeof(DeletableEntityAttribute)))
            {
                throw new InvalidOperationException("This entity cannot be physically deleted.");
            }
            InTransaction(() => _session.Delete(entity));
        }

        private void InTransaction(Action action)
        {
            using (var transaction = _session.BeginTransaction())
            {
                action();
                transaction.Commit();
            }
        }

        private TEntity Map(TDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }
    }
}
