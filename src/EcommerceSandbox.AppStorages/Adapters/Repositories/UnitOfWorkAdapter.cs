using DomainRepositories = EcommerceSandbox.DomainServices.Interfaces.Storages.Repositories;
using IUnitOfWork = EcommerceSandbox.AppStorages.Interfaces.Repositories.IUnitOfWork;

namespace EcommerceSandbox.AppStorages.Adapters.Repositories;

public class UnitOfWorkAdapter : DomainRepositories.IUnitOfWork
{
    private readonly IUnitOfWork _persistentUnitOfWork;

    public UnitOfWorkAdapter(IUnitOfWork persistentUnitOfWork)
    {
        _persistentUnitOfWork = persistentUnitOfWork;
    }

    public void BeginTransaction()
    {
        _persistentUnitOfWork.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _persistentUnitOfWork.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _persistentUnitOfWork.RollbackTransaction();
    }
}