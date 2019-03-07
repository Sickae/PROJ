using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Shared.Attributes;
using System;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public abstract class ManagerBase<TEntity, TDto> : IManager<TEntity, TDto> where TEntity : Entity where TDto : DTOBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ISession _session;

        public ManagerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _session = unitOfWork.Session;
        }

        public virtual int Create(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = Map(dto);
            return CreateInternal(entity);
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

        public virtual int Save(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            //TEntity entity = _session.Load<TEntity>(dto.Id);
            var entity = Map(dto);

            return SaveInternal(entity);
        }

        private int CreateInternal(TEntity entity)
        {
            entity.Id = 0;
            _unitOfWork.UseTransaction(() => _session.Save(entity));

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
            _unitOfWork.UseTransaction(() => _session.Delete(entity));
        }

        private int SaveInternal(TEntity entity)
        {
            _unitOfWork.UseTransaction(() =>
            {
                entity.ModificationDate = DateTime.UtcNow;
                _session.Merge(entity);
            });

            return entity.Id;
        }

        protected TEntity Map(TDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }
    }
}
