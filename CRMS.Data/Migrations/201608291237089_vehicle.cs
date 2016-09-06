namespace CRMS_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vehicle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "Photo", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "Photo", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
