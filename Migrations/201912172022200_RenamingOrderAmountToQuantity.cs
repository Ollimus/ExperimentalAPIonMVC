namespace TestAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamingOrderAmountToQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderAmount", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Quantity");
        }
    }
}
