using NHibernate;
using PROJ.Logic.UnitOfWork.Interfaces;
using System;

namespace PROJ.Logic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISession Session { get; private set; }

        private ITransaction _transaction;

        public UnitOfWork(ISession session)
        {
            Session = session;
        }

        public void UseTransaction(Action action)
        {
            if (_transaction == null)
            {
                _transaction = Session.BeginTransaction();
            }
            action();
        }

        public void SaveChanges()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("There is no transaction to commit.");
            }

            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                SaveChanges();
            }
        }
    }
}
