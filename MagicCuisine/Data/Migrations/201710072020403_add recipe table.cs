namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrecipetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Avatar = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recipes");
        }
    }
}
