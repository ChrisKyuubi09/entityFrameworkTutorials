using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace efTutorialCore.Models
{
    public partial class ClinicContext : DbContext
    {
        public ClinicContext()
        {
        }

        public ClinicContext(DbContextOptions<ClinicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<View1> View1s { get; set; }
        public virtual DbSet<View2> View2s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SHARKNATOS\\SQLEXPRESS;Initial Catalog=Clinic;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Dcode);

                entity.ToTable("Doctor");

                entity.Property(e => e.Dcode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dcode");

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dname");

                entity.Property(e => e.Specialtycode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("specialtycode");

                entity.HasOne(d => d.SpecialtycodeNavigation)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Specialtycode)
                    .HasConstraintName("FK_Doctor_Specialty");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exam");

                entity.Property(e => e.Examid)
                    .HasColumnType("numeric(7, 0)")
                    .HasColumnName("examid");

                entity.Property(e => e.Doctorcode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("doctorcode");

                entity.Property(e => e.Examdate)
                    .HasColumnType("date")
                    .HasColumnName("examdate");

                entity.Property(e => e.Patientcode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("patientcode");

                entity.HasOne(d => d.DoctorcodeNavigation)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Doctorcode)
                    .HasConstraintName("FK_Exam_Doctor");

                entity.HasOne(d => d.PatientcodeNavigation)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.Patientcode)
                    .HasConstraintName("FK_Exam_Patient");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Pcode);

                entity.ToTable("Patient");

                entity.Property(e => e.Pcode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("pcode");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Paddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paddress");

                entity.Property(e => e.Pname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pname");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(e => e.Scode2);

                entity.ToTable("Specialty");

                entity.Property(e => e.Scode2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("scode2");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View1");

                entity.Property(e => e.Paddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paddress");

                entity.Property(e => e.Pname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pname");
            });

            modelBuilder.Entity<View2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View2");

                entity.Property(e => e.Examdate)
                    .HasColumnType("date")
                    .HasColumnName("examdate");

                entity.Property(e => e.Pname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
