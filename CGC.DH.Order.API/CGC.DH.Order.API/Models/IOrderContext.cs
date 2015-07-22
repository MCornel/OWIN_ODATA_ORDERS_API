using System.Data.Entity;
using System.Threading.Tasks;

namespace CGC.DH.Order.API.Models
{
    public interface IOrderContext
    {
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<OrderItemOption> OrderItemOptions { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
    } 
}