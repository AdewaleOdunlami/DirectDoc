namespace DirectDoc2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SponsorID = c.Int(),
                        Title = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 500),
                        Initials = c.String(),
                        LastName = c.String(nullable: false, maxLength: 500),
                        DateOfBirth = c.DateTime(nullable: false),
                        IsDependant = c.Boolean(nullable: false),
                        FullName = c.String(),
                        MedicalAid_MedicalAidID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.SponsorID)
                .ForeignKey("dbo.MedicalAid", t => t.MedicalAid_MedicalAidID)
                .Index(t => t.SponsorID)
                .Index(t => t.MedicalAid_MedicalAidID);
            
            CreateTable(
                "dbo.Consultation",
                c => new
                    {
                        ConsultationID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        ConsultationDate = c.DateTime(nullable: false),
                        ConsultationTime = c.DateTime(nullable: false),
                        ModalityID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ModalityDescription = c.String(),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        SubTotal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ConsultationID)
                .ForeignKey("dbo.Modality", t => t.ModalityID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.ModalityID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Modality",
                c => new
                    {
                        ModalityID = c.Int(nullable: false, identity: true),
                        TariffID = c.Int(nullable: false),
                        NappiCode = c.String(nullable: false, maxLength: 500),
                        ModalityCode = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false, maxLength: 500),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        CodeDescription = c.String(),
                    })
                .PrimaryKey(t => t.ModalityID)
                .ForeignKey("dbo.Tariff", t => t.TariffID, cascadeDelete: true)
                .Index(t => t.TariffID);
            
            CreateTable(
                "dbo.Tariff",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TariffType = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        PhoneID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(),
                        PhoneType = c.String(nullable: false),
                        AreaCode = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneID)
                .ForeignKey("dbo.Person", t => t.PersonID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.PostalAddress",
                c => new
                    {
                        PostalAddressID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(),
                        BoxNumber = c.Int(),
                        City = c.String(nullable: false, maxLength: 500),
                        Country = c.String(),
                        FullAddress = c.String(),
                    })
                .PrimaryKey(t => t.PostalAddressID)
                .ForeignKey("dbo.Person", t => t.PersonID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(),
                        InvoiceNumber = c.Int(),
                        InvoiceDate = c.DateTime(nullable: false),
                        NameOfAid = c.String(),
                        PolicyNumber = c.String(),
                        InvoiceTo = c.String(),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Person", t => t.PersonID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        InvoiceID = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateOnCheck = c.DateTime(),
                        CheckNumber = c.Int(),
                        BankName = c.String(),
                        PayableTo = c.String(),
                        NameOnCard = c.String(),
                        CardNumber = c.Int(),
                        CardType = c.String(),
                        ExpiryDate = c.DateTime(),
                        CardIssuer = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Invoice", t => t.InvoiceID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.InvoiceID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.MedicalAid",
                c => new
                    {
                        MedicalAidID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        NameOfAid = c.String(nullable: false),
                        PolicyNumber = c.Int(nullable: false),
                        TariffID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicalAidID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .ForeignKey("dbo.Tariff", t => t.TariffID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.TariffID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalAid", "TariffID", "dbo.Tariff");
            DropForeignKey("dbo.Person", "MedicalAid_MedicalAidID", "dbo.MedicalAid");
            DropForeignKey("dbo.MedicalAid", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Invoice", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Payment", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Payment", "InvoiceID", "dbo.Invoice");
            DropForeignKey("dbo.PostalAddress", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Phone", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Person", "SponsorID", "dbo.Person");
            DropForeignKey("dbo.Consultation", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Consultation", "ModalityID", "dbo.Modality");
            DropForeignKey("dbo.Modality", "TariffID", "dbo.Tariff");
            DropIndex("dbo.MedicalAid", new[] { "TariffID" });
            DropIndex("dbo.Person", new[] { "MedicalAid_MedicalAidID" });
            DropIndex("dbo.MedicalAid", new[] { "PersonID" });
            DropIndex("dbo.Invoice", new[] { "PersonID" });
            DropIndex("dbo.Payment", new[] { "PersonID" });
            DropIndex("dbo.Payment", new[] { "InvoiceID" });
            DropIndex("dbo.PostalAddress", new[] { "PersonID" });
            DropIndex("dbo.Phone", new[] { "PersonID" });
            DropIndex("dbo.Person", new[] { "SponsorID" });
            DropIndex("dbo.Consultation", new[] { "PersonID" });
            DropIndex("dbo.Consultation", new[] { "ModalityID" });
            DropIndex("dbo.Modality", new[] { "TariffID" });
            DropTable("dbo.MedicalAid");
            DropTable("dbo.Payment");
            DropTable("dbo.Invoice");
            DropTable("dbo.PostalAddress");
            DropTable("dbo.Phone");
            DropTable("dbo.Tariff");
            DropTable("dbo.Modality");
            DropTable("dbo.Consultation");
            DropTable("dbo.Person");
        }
    }
}
