namespace CRMS_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Phone", c => c.String());
            AddColumn("dbo.Customers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Phone");
            DropColumn("dbo.Employees", "Phone");
        }
    }
}
