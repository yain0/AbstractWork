namespace AbstractShopService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableMessageInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Сustomer", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Сustomer", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "CustomerId", "dbo.Сustomer");
            DropIndex("dbo.MessageInfoes", new[] { "CustomerId" });
            DropColumn("dbo.Сustomer", "Mail");
            DropTable("dbo.MessageInfoes");
        }
    }
}
