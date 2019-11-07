namespace TestAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProductsAndBillingsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        TimeAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            AddColumn("dbo.Customers", "City", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Customers", "Address", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Customers", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "DateCreated");
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.Customers", "City");
            DropTable("dbo.Products");
            DropTable("dbo.Billings");
        }
    }
}
