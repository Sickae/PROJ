using NHibernate;
using System;

namespace PROJ.Logic.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISession Session { get; }

        void UseTransaction(Action action);
        void SaveChanges();
    }
}
