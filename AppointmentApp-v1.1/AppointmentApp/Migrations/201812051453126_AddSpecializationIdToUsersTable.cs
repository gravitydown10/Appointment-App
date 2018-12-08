namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecializationIdToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SpecializationId", c => c.Int());
            CreateIndex("dbo.Users", "SpecializationId");
            AddForeignKey("dbo.Users", "SpecializationId", "dbo.Specializations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Users", new[] { "SpecializationId" });
            DropColumn("dbo.Users", "SpecializationId");
        }
    }
}
