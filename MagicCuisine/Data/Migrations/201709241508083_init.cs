namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false, maxLength: 150),
                        Building = c.String(maxLength: 10),
                        Entrance = c.String(maxLength: 10),
                        Floor = c.String(maxLength: 10),
                        Flat = c.String(maxLength: 10),
                        PostalCode = c.String(maxLength: 10),
                        Country_ID = c.Int(),
                        Town_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.Country_ID)
                .ForeignKey("dbo.Towns", t => t.Town_ID)
                .Index(t => t.Country_ID)
                .Index(t => t.Town_ID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true, name: "IX_UniqueName");
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Town_ID", "dbo.Towns");
            DropForeignKey("dbo.Addresses", "Country_ID", "dbo.Countries");
            DropIndex("dbo.Countries", "IX_UniqueName");
            DropIndex("dbo.Addresses", new[] { "Town_ID" });
            DropIndex("dbo.Addresses", new[] { "Country_ID" });
            DropTable("dbo.Towns");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
        }
    }
}
