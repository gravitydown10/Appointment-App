namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRolesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("insert into Roles(RoleName)values('User')");
            Sql("insert into Roles(RoleName)values('Doctor')");
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Roles");
        }
    }
}
