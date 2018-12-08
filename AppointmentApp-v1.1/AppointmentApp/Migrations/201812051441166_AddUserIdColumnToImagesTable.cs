namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdColumnToImagesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "UserId");
            AddForeignKey("dbo.Images", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "UserId", "dbo.Users");
            DropIndex("dbo.Images", new[] { "UserId" });
            DropColumn("dbo.Images", "UserId");
        }
    }
}
