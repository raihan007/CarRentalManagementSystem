namespace CRMS_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editReservation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "PickUpTime", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "DropOffTime", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "DropOffTime", c => c.Time(nullable: false, precision: 4));
            AlterColumn("dbo.Reservations", "PickUpTime", c => c.Time(nullable: false, precision: 4));
        }
    }
}
