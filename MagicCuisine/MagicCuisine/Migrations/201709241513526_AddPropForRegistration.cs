namespace MagicCuisine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropForRegistration : DbMigration
    {
        public override void Up()
        {            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Address_ID");
            AddForeignKey("dbo.AspNetUsers", "Address_ID", "dbo.Addresses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Address_ID", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "Town_ID", "dbo.Towns");
            DropForeignKey("dbo.Addresses", "Country_ID", "dbo.Countries");
            DropIndex("dbo.Countries", "IX_UniqueName");
            DropIndex("dbo.Addresses", new[] { "Town_ID" });
            DropIndex("dbo.Addresses", new[] { "Country_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Address_ID" });
            DropColumn("dbo.AspNetUsers", "Address_ID");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.Towns");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
        }
    }
}
