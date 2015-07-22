using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks; 

namespace CGC.DH.Order.API.Models
{   
    public class TestContext : IOrderContext 
    { 
        public TestContext() 
        { 
            this.Orders = new TestDbSet<Order>(); 
            this.OrderItems = new TestDbSet<OrderItem>();
            this.OrderItemOptions = new TestDbSet<OrderItemOption>();
        } 
 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemOption> OrderItemOptions { get; set; }

        public int SaveChangesCount { get; private set; }
        public int SaveChangesAsyncCount { get; private set; }

        public int SaveChanges() 
        { 
            this.SaveChangesCount++; 
            return 1; 
        }

        public Task<int> SaveChangesAsync()
        {
            this.SaveChangesAsyncCount++;
            return Task.FromResult(1);
        }

        public void Dispose()
        {            
        }
    } 
}