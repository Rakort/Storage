namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.Int(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Provider_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.Provider_Id)
                .Index(t => t.Provider_Id);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ComingProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Coming_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comings", t => t.Coming_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Coming_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Description = c.String(),
                        Code = c.String(),
                        Article = c.String(),
                        Count = c.Int(nullable: false),
                        MinCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Writeoffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WriteoffProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Product_Id = c.Int(),
                        Writeoff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Writeoffs", t => t.Writeoff_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Writeoff_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WriteoffProducts", "Writeoff_Id", "dbo.Writeoffs");
            DropForeignKey("dbo.WriteoffProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ComingProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ComingProducts", "Coming_Id", "dbo.Comings");
            DropForeignKey("dbo.Comings", "Provider_Id", "dbo.Providers");
            DropIndex("dbo.WriteoffProducts", new[] { "Writeoff_Id" });
            DropIndex("dbo.WriteoffProducts", new[] { "Product_Id" });
            DropIndex("dbo.ComingProducts", new[] { "Product_Id" });
            DropIndex("dbo.ComingProducts", new[] { "Coming_Id" });
            DropIndex("dbo.Comings", new[] { "Provider_Id" });
            DropTable("dbo.WriteoffProducts");
            DropTable("dbo.Writeoffs");
            DropTable("dbo.Products");
            DropTable("dbo.ComingProducts");
            DropTable("dbo.Providers");
            DropTable("dbo.Comings");
        }
    }
}
