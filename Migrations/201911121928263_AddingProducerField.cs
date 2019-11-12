namespace TestAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProducerField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Producer", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Producer");
        }
    }
}
