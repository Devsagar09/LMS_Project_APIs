using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project_APIs.Models;

public partial class LearningManagementSystemContext : DbContext
{
    public LearningManagementSystemContext()
    {
    }

    public LearningManagementSystemContext(DbContextOptions<LearningManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblApproval> TblApprovals { get; set; }

    public virtual DbSet<TblAssign> TblAssigns { get; set; }

    public virtual DbSet<TblCompanyLogo> TblCompanyLogos { get; set; }

    public virtual DbSet<TblConfiguration> TblConfigurations { get; set; }

    public virtual DbSet<TblEnrollment> TblEnrollments { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblStatus> TblStatuses { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblTraining> TblTrainings { get; set; }

    public virtual DbSet<TblTrainingTranscript> TblTrainingTranscripts { get; set; }

    public virtual DbSet<TblTrainingType> TblTrainingTypes { get; set; }

   public virtual DbSet<CourseCatalog> CourseCatalogs { get; set; }

   public virtual DbSet<DisplayIDP> DisplayIDPs { get; set; } 
   public virtual DbSet<TrainingTrascriptData> TrainingTrascriptDatas { get; set; }
    public virtual DbSet<IDPSearching> IDPSearchings { get; set; }
    public virtual DbSet<TranscriptSearching> TranscriptSearchings { get; set; }
    public virtual DbSet<DisplayEnrollment> DisplayEnrollments { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=192.168.150.242;Initial Catalog=Learning_Management_System;User ID=admin;Password=admin;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseCatalog>().HasNoKey().ToView(null);

        modelBuilder.Entity<DisplayIDP>().HasNoKey().ToView(null);

        modelBuilder.Entity<TrainingTrascriptData>().HasNoKey().ToView(null);

        modelBuilder.Entity<IDPSearching>().HasNoKey().ToView(null);

        modelBuilder.Entity<TranscriptSearching>().HasNoKey().ToView(null);

        modelBuilder.Entity<DisplayEnrollment>().HasNoKey().ToView(null);

        modelBuilder.Entity<TblApproval>(entity =>
        {
            entity.HasKey(e => e.ApprovalId).HasName("PK__tbl_Appr__C94AE61A713378A8");

            entity.ToTable("tbl_Approval");

            entity.Property(e => e.ApprovalId).HasColumnName("approval_id");
            entity.Property(e => e.DecisionDate)
                .HasColumnType("datetime")
                .HasColumnName("decision_date");
            entity.Property(e => e.RequireApproval)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("require_approval");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.StudentName)
                .HasMaxLength(255)
                .HasColumnName("student_name");
            entity.Property(e => e.TrainingId).HasColumnName("training_id");
            entity.Property(e => e.TrainingName)
                .HasMaxLength(255)
                .HasColumnName("training_name");
            entity.Property(e => e.TrainingtypeId).HasColumnName("trainingtype_id");
            entity.Property(e => e.TrainingtypeName)
                .HasMaxLength(100)
                .HasColumnName("trainingtype_name");

            entity.HasOne(d => d.Student).WithMany(p => p.TblApprovals)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__tbl_Appro__stude__75035A77");

            entity.HasOne(d => d.Training).WithMany(p => p.TblApprovals)
                .HasForeignKey(d => d.TrainingId)
                .HasConstraintName("FK__tbl_Appro__train__740F363E");

            entity.HasOne(d => d.Trainingtype).WithMany(p => p.TblApprovals)
                .HasForeignKey(d => d.TrainingtypeId)
                .HasConstraintName("FK__tbl_Appro__train__75F77EB0");
        });

        modelBuilder.Entity<TblAssign>(entity =>
        {
            entity.HasKey(e => e.AssignId).HasName("PK_tbl_Person_Training");

            entity.ToTable("tbl_Assign");

            entity.Property(e => e.AssignId).HasColumnName("assign_id");
            entity.Property(e => e.AssignDate)
                .HasColumnType("datetime")
                .HasColumnName("assign_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TrainingId).HasColumnName("training_id");
            entity.Property(e => e.TrainingName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("training_name");

            entity.HasOne(d => d.Status).WithMany(p => p.TblAssigns)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Status");

            entity.HasOne(d => d.Student).WithMany(p => p.TblAssigns)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_tbl_Assign_tbl_Student");

            entity.HasOne(d => d.Training).WithMany(p => p.TblAssigns)
                .HasForeignKey(d => d.TrainingId)
                .HasConstraintName("FK_tbl_Assign_tbl_Training");
        });

        modelBuilder.Entity<TblCompanyLogo>(entity =>
        {
            entity.HasKey(e => e.CompanylogoId).HasName("PK_tbl_ComapnyLogo");

            entity.ToTable("tbl_CompanyLogo");

            entity.Property(e => e.CompanylogoId).HasColumnName("companylogo_id");
            entity.Property(e => e.CompanyImage)
                .IsUnicode(false)
                .HasColumnName("company_image");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<TblConfiguration>(entity =>
        {
            entity.HasKey(e => e.ConfigId);

            entity.ToTable("tbl_Configuration");

            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.ConfigKey)
                .IsUnicode(false)
                .HasColumnName("config_key");
            entity.Property(e => e.ConfigValue).HasColumnName("config_value");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<TblEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollId).HasName("PK__tbl_Enro__A068F23720A16222");

            entity.ToTable("tbl_Enrollment");

            entity.Property(e => e.EnrollId).HasColumnName("enroll_id");
            entity.Property(e => e.EnrollDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("enroll_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TrainingId).HasColumnName("training_id");
        });

     

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tbl_Role__760965CC096D028A");

            entity.ToTable("tbl_Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("role_name");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        modelBuilder.Entity<TblStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("tbl_Status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__tbl_Pers__543848DF7B9557BD");

            entity.ToTable("tbl_Student");

            entity.HasIndex(e => e.Email, "UQ__tbl_Pers__AB6E61640DAA001B").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.ArchiveDate).HasColumnName("archive_date");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("middlename");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone_no");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_image");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.StudentNo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("student_no");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_tbl_Person_tbl_Role");
        });

        modelBuilder.Entity<TblTraining>(entity =>
        {
            entity.HasKey(e => e.TrainingId).HasName("PK__tbl_Trai__2F28D08F1FFE3D5A");

            entity.ToTable("tbl_Training");

            entity.Property(e => e.TrainingId).HasColumnName("training_id");
            entity.Property(e => e.ArchiveDate).HasColumnName("archive_date");
            entity.Property(e => e.CendDate).HasColumnName("cend_date");
            entity.Property(e => e.CourseCatalog)
                .HasDefaultValue(true)
                .HasColumnName("course_catalog");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.CstartDate).HasColumnName("cstart_date");
            entity.Property(e => e.DocumentFile).HasColumnName("document_file");
            entity.Property(e => e.ExternalLinkUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("external_link_URL");
            entity.Property(e => e.RequiresApproval)
                .HasDefaultValue(false)
                .HasColumnName("requires_approval");
            entity.Property(e => e.Summary)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("summary");
            entity.Property(e => e.ThumbnailImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("thumbnail_image");
            entity.Property(e => e.TrainingCode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("training_code");
            entity.Property(e => e.TrainingHours)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("training_hours");
            entity.Property(e => e.TrainingName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("training_name");
            entity.Property(e => e.TrainingtypeId).HasColumnName("trainingtype_id");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Trainingtype).WithMany(p => p.TblTrainings)
                .HasForeignKey(d => d.TrainingtypeId)
                .HasConstraintName("FK_tbl_Training_tbl_TrainingType");
        });

        modelBuilder.Entity<TblTrainingTranscript>(entity =>
        {
            entity.HasKey(e => e.TranscriptId).HasName("PK__tbl_Trai__3D043C38AE9D7DB6");

            entity.ToTable("tbl_TrainingTranscript");

            entity.Property(e => e.TranscriptId).HasColumnName("transcript_id");
            entity.Property(e => e.CompletionDate).HasColumnName("completion_date");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("create_date");
            entity.Property(e => e.EnrollDate).HasColumnName("enroll_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TrainingId).HasColumnName("training_id");
            entity.Property(e => e.TrainingName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("training_name");

            entity.HasOne(d => d.Status).WithMany(p => p.TblTrainingTranscripts)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_tbl_TrainingTranscript_tbl_Status");

            entity.HasOne(d => d.Student).WithMany(p => p.TblTrainingTranscripts)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_tbl_TrainingTranscript_tbl_Student");

            entity.HasOne(d => d.Training).WithMany(p => p.TblTrainingTranscripts)
                .HasForeignKey(d => d.TrainingId)
                .HasConstraintName("FK_tbl_TrainingTranscript_tbl_Training");
        });

        modelBuilder.Entity<TblTrainingType>(entity =>
        {
            entity.HasKey(e => e.TrainingtypeId).HasName("PK__tbl_Trai__7EEAAD047E5B6571");

            entity.ToTable("tbl_TrainingType");

            entity.Property(e => e.TrainingtypeId).HasColumnName("trainingtype_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.TrainingtypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("trainingtype_name");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("update_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
