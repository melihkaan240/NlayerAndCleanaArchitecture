
namespace App.Repositories;
public class UnitOfWork(AppDBContext context) : IUnitOfWork
{

    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();


}
