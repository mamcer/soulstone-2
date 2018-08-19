using System;

namespace CrossCutting.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Rollback();

        void RegisterDirty(object entity);
    }
}