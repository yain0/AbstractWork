namespace AbstractShopService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        RemontId = c.Int(nullable: false),
                        WorkerId = c.Int(),
                        Koll = c.Int(nullable: false),
                        Summa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateWork = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Сustomer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Remonts", t => t.RemontId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId)
                .Index(t => t.CustomerId)
                .Index(t => t.RemontId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.Сustomer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Remonts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RemontName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RemontMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RemontId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Koll = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Remonts", t => t.RemontId, cascadeDelete: true)
                .Index(t => t.RemontId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkladMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkladId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Koll = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Sklads", t => t.SkladId, cascadeDelete: true)
                .Index(t => t.SkladId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Sklads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkladName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.RemontMaterials", "RemontId", "dbo.Remonts");
            DropForeignKey("dbo.SkladMaterials", "SkladId", "dbo.Sklads");
            DropForeignKey("dbo.SkladMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.RemontMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Activities", "RemontId", "dbo.Remonts");
            DropForeignKey("dbo.Activities", "CustomerId", "dbo.Сustomer");
            DropIndex("dbo.SkladMaterials", new[] { "MaterialId" });
            DropIndex("dbo.SkladMaterials", new[] { "SkladId" });
            DropIndex("dbo.RemontMaterials", new[] { "MaterialId" });
            DropIndex("dbo.RemontMaterials", new[] { "RemontId" });
            DropIndex("dbo.Activities", new[] { "WorkerId" });
            DropIndex("dbo.Activities", new[] { "RemontId" });
            DropIndex("dbo.Activities", new[] { "CustomerId" });
            DropTable("dbo.Workers");
            DropTable("dbo.Sklads");
            DropTable("dbo.SkladMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.RemontMaterials");
            DropTable("dbo.Remonts");
            DropTable("dbo.Сustomer");
            DropTable("dbo.Activities");
        }
    }
}
