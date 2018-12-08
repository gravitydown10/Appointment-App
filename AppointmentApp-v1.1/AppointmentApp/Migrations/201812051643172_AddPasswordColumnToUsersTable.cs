namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordColumnToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
        }
    }
}
