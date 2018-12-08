namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationInAddressColumnInUsersTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false));
        }
    }
}
