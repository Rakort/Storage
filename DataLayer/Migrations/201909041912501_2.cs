namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ComingProducts", newName: "ProductCounts");
            DropForeignKey("dbo.WriteoffProducts", "Writeoff_Id", "dbo.Writeoffs");
            DropIndex("dbo.WriteoffProducts", new[] { "Product_Id" });
            DropIndex("dbo.WriteoffProducts", new[] { "Writeoff_Id" });
            AddColumn("dbo.ProductCounts", "Writeoff_Id", c => c.Int());
            CreateIndex("dbo.ProductCounts", "Writeoff_Id");
            AddForeignKey("dbo.ProductCounts", "Writeoff_Id", "dbo.Writeoffs", "Id");
            DropTable("dbo.WriteoffProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WriteoffProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Product_Id = c.Int(),
                        Writeoff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ProductCounts", "Writeoff_Id", "dbo.Writeoffs");
            DropIndex("dbo.ProductCounts", new[] { "Writeoff_Id" });
            DropColumn("dbo.ProductCounts", "Writeoff_Id");
            CreateIndex("dbo.WriteoffProducts", "Writeoff_Id");
            CreateIndex("dbo.WriteoffProducts", "Product_Id");
            AddForeignKey("dbo.WriteoffProducts", "Writeoff_Id", "dbo.Writeoffs", "Id");
            RenameTable(name: "dbo.ProductCounts", newName: "ComingProducts");
        }
    }
}
