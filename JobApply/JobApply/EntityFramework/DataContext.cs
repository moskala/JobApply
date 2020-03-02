using JobApply.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApply.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
      
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JobApply.Models.CompanyModel", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("City").IsRequired();

                b.Property<string>("ContactEmail").IsRequired();

                b.Property<string>("Country").IsRequired();

                b.Property<DateTime>("FoundationDate").IsRequired();

                b.Property<string>("Name").IsRequired();

                b.HasKey("Id");

                b.HasIndex("Id");

                b.ToTable("Companies");
            });

            modelBuilder.Entity("JobApply.Models.JobApplication", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<bool>("ContactAgreement").IsRequired();

                b.Property<DateTime>("Created").IsRequired();

                b.Property<string>("CvUrl");

                b.Property<string>("EmailAddress").IsRequired();

                b.Property<string>("FirstName").IsRequired();

                b.Property<string>("LastName").IsRequired();

                b.Property<int>("OfferId").IsRequired();

                b.Property<string>("PhoneNumber").IsRequired();

                b.HasKey("Id");

                b.HasIndex("OfferId");

                b.ToTable("JobApplications");
            });

            modelBuilder.Entity("JobApply.Models.JobOffer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<DateTime>("ApplicationDeadline").IsRequired();

                b.Property<string>("CompanyName").IsRequired();

                b.Property<string>("ContractLength").IsRequired();

                b.Property<DateTime>("Created").IsRequired();

                b.Property<string>("JobDescription").IsRequired();

                b.Property<string>("JobTitle").IsRequired();

                b.Property<string>("Location").IsRequired();

                b.Property<string>("SalaryDescription").IsRequired();

                b.Property<int>("SalaryFrom").IsRequired();

                b.Property<int>("SalaryTo").IsRequired();

                b.Property<DateTime>("WorkStartDate").IsRequired();

                b.HasKey("Id");

                b.HasIndex("Id");

                b.ToTable("JobOffers");
            });

            modelBuilder.Entity("JobApply.Models.JobApplication", b =>
            {
                b.HasOne("JobApply.Models.JobOffer", "JobOffer")
                    .WithMany("JobApplications")
                    .HasForeignKey("OfferId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Seed();
        }

    }

    public static class ModelBuilderExtensions
    {

        private static int GenerateId(int addition)
        {
            var time = DateTime.Now;
            return time.Year + time.Month + time.Day + time.Hour + time.Minute + time.Second + time.Millisecond + addition;
        }
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobOffer>().HasData(
                new JobOffer
                {
                    Id = GenerateId(0),
                    JobTitle = "Cook",
                    CompanyName = "Kitchen",
                    JobDescription = "Polish cuisine.",
                    ApplicationDeadline = new DateTime(2020, 02, 1),
                    WorkStartDate = new DateTime(2020, 03, 1),
                    Location = "Warsaw, Poland",
                    SalaryFrom = 100,
                    SalaryTo = 200,
                    SalaryDescription = "PLN/hour",
                    ContractLength = "1 year",
                    Created = DateTime.Now
                },
                new JobOffer
                {
                    Id = GenerateId(1000),
                    JobTitle = "Astronaut",
                    CompanyName = "NASA",
                    JobDescription = "Space travel.",
                    ApplicationDeadline = new DateTime(2020, 02, 1),
                    WorkStartDate = new DateTime(2020, 03, 1),
                    Location = "Washington D.C.,United States",
                    SalaryFrom = 10000,
                    SalaryTo = 20000,
                    SalaryDescription = "US/travel",
                    ContractLength = "1 year",
                    Created = DateTime.Now
                },
                new JobOffer
                {
                    Id = GenerateId(2000),
                    JobTitle = "C# developer",
                    CompanyName = "IT Company",
                    JobDescription = "ASP.NET core web applications.",
                    ApplicationDeadline = new DateTime(2020, 02, 1),
                    WorkStartDate = new DateTime(2020, 03, 1),
                    Location = "Cracow, Poland",
                    SalaryFrom = 4000,
                    SalaryTo = 5000,
                    SalaryDescription = "PLN/month",
                    ContractLength = "2 years",
                    Created = DateTime.Now
                }
            );
            modelBuilder.Entity<CompanyModel>().HasData(
                new CompanyModel
                {
                    Id = GenerateId(0),
                    Name = "NASA",
                    City = "Washington",
                    Country = "United States",
                    ContactEmail = "nasa@mail.com",
                    FoundationDate = new DateTime(1958, 06, 29)
                },
                new CompanyModel
                {
                    Id = GenerateId(1000),
                    Name = "IT Company",
                    City = "Warsaw",
                    Country = "Poland",
                    ContactEmail = "it@company.com",
                    FoundationDate = new DateTime(2015, 01, 29)
                },
                new CompanyModel
                {
                    Id = GenerateId(2000),
                    Name = "Reserved",
                    City = "Warsaw",
                    Country = "Poland",
                    ContactEmail = "job@reserved.pl",
                    FoundationDate = new DateTime(1958, 06, 29)
                }
            );           
        }
    }

}
