using TableBooking.Models;

namespace TableBooking.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GeAllAsync();
        public Task<T> GetByIdAsync(Guid id);
        public Task Create(T dataEntry);
        public void Edit(T Entity);
        public void Delete(T Entity);
        public Task SaveChanges();
        
    }
}
