namespace BussinesLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartLines",
                c => new
                    {
                        CartLineId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserId = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CartLineId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartLines", "ProductId", "dbo.Products");
            DropIndex("dbo.CartLines", new[] { "ProductId" });
            DropTable("dbo.CartLines");
        }
    }
}
