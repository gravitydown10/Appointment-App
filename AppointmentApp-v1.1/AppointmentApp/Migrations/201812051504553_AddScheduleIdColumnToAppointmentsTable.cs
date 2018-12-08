namespace AppointmentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScheduleIdColumnToAppointmentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "ScheduleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "ScheduleId");
            AddForeignKey("dbo.Appointments", "ScheduleId", "dbo.Schedules", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "ScheduleId", "dbo.Schedules");
            DropIndex("dbo.Appointments", new[] { "ScheduleId" });
            DropColumn("dbo.Appointments", "ScheduleId");
        }
    }
}
