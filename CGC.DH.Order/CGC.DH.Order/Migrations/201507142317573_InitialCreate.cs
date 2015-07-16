namespace CGC.DH.OrderSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderItemOptions");
            AddColumn("dbo.OrderItemOptions", "OptionID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderItemOptions", "OptionID");
            DropColumn("dbo.OrderItemOptions", "ItemID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItemOptions", "ItemID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.OrderItemOptions");
            DropColumn("dbo.OrderItemOptions", "OptionID");
            AddPrimaryKey("dbo.OrderItemOptions", "ItemID");
        }
    }
}
