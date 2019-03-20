using AutoMapper;
using NHibernate;
using PROJ.DataAccess.Entities;
using PROJ.Logic.DTOs;
using PROJ.Logic.Interfaces;
using PROJ.Logic.UnitOfWork.Interfaces;
using PROJ.Shared.Attributes;
using System;

namespace PROJ.Logic.UnitOfWork.Managers
{
    public abstract class ManagerBase<TEntity, TDto> : IManager<TEntity, TDto> where TEntity : Entity where TDto : DTOBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ISession _session;
        protected readonly IAppContext _appContext;

        public ManagerBase(IUnitOfWork unitOfWork, IAppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _session = unitOfWork.Session;
            _appContext = appContext;
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

        public virtual void Delete(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            DeleteInternal(dto.Id);
        }

        public void Delete(int id)
        {
            DeleteInternal(id);
        }

        public virtual int Save(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = Map(dto);

            return SaveInternal(entity);
        }

        private int CreateInternal(TEntity entity)
        {
            entity.Id = 0;
            _unitOfWork.UseTransaction(() => _session.Save(entity));

            return entity.Id;
        }

        private void DeleteInternal(int id)
        {
            //if (Attribute.IsDefined(entity.GetType(), typeof(DeletableEntityAttribute)))
            //{
            //    _unitOfWork.UseTransaction(() => _session.Delete(entity));
            //}
            //else
            //{
            //    entity.IsDeleted = true;
            //    SaveInternal(entity);
            //}
            _unitOfWork.UseTransaction(() =>
            {
                var entity = _session.Load<TEntity>(id);
                _session.Delete(entity);
            });
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
