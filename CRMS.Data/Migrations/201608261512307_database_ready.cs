namespace CRMS_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database_ready : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        BillAmount = c.Double(nullable: false),
                        BillDate = c.DateTime(nullable: false),
                        Tax = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        ReservationId = c.Int(nullable: false),
                        BillStatus = c.String(nullable: false, maxLength: 10),
                        BillCreated = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Employees", t => t.BillCreated)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .Index(t => t.ReservationId)
                .Index(t => t.BillCreated);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Photo = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        NationalId = c.String(nullable: false),
                        PassportNo = c.String(nullable: false),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Logins", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 8),
                        Password = c.String(nullable: false, maxLength: 8),
                        LastLoginTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Photo = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        NationalId = c.String(nullable: false),
                        PassportNo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Logins", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        ReservedBy = c.Int(nullable: false),
                        PickUpLocation = c.String(nullable: false, maxLength: 50),
                        PickUpDate = c.DateTime(nullable: false, storeType: "date"),
                        PickUpTime = c.Time(nullable: false, precision: 4),
                        DropOffLocation = c.String(nullable: false, maxLength: 50),
                        DropOffDate = c.DateTime(nullable: false, storeType: "date"),
                        DropOffTime = c.Time(nullable: false, precision: 4),
                        ReservationAmount = c.Double(nullable: false),
                        ReservationStatus = c.String(nullable: false, maxLength: 15),
                        ReservationVehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Vehicles", t => t.ReservationVehicleId)
                .ForeignKey("dbo.Customers", t => t.ReservedBy)
                .Index(t => t.ReservedBy)
                .Index(t => t.ReservationVehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        VehicleName = c.String(nullable: false, maxLength: 50),
                        Brand = c.String(nullable: false, maxLength: 20),
                        Photo = c.String(nullable: false, maxLength: 50),
                        Details = c.String(nullable: false),
                        Capacity = c.String(nullable: false, maxLength: 50),
                        CostPerHour = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "UserId", "dbo.Logins");
            DropForeignKey("dbo.Customers", "UserId", "dbo.Logins");
            DropForeignKey("dbo.Reservations", "ReservedBy", "dbo.Customers");
            DropForeignKey("dbo.Reservations", "ReservationVehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Bills", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Bills", "BillCreated", "dbo.Employees");
            DropIndex("dbo.Reservations", new[] { "ReservationVehicleId" });
            DropIndex("dbo.Reservations", new[] { "ReservedBy" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.Bills", new[] { "BillCreated" });
            DropIndex("dbo.Bills", new[] { "ReservationId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Reservations");
            DropTable("dbo.Customers");
            DropTable("dbo.Logins");
            DropTable("dbo.Employees");
            DropTable("dbo.Bills");
        }
    }
}
