namespace CGC.DH.Order.API.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class OrderEDMX : DbContext, IOrderContext
    {
        // Your context has been configured to use a 'OrderEDMX' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CGC.DH.Order.Models.OrderEDMX' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OrderEDMX' 
        // connection string in the application configuration file.
        public OrderEDMX()
            : base("name=OrderEDMX")
        {            
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public virtual DbSet<OrderItemOption> OrderItemOptions { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.DisplayName)
        //        .HasColumnName("display_name");
        //} 


        //public System.Data.Entity.DbSet<CGC.DH.OrderSys.Models.Order> Orders { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}