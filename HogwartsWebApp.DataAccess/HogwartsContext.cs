using Microsoft.EntityFrameworkCore;

namespace HogwartsWebApp.DataAccess;

public partial class HogwartsContext : DbContext
{
    public HogwartsContext()
    {
    }

    public HogwartsContext(DbContextOptions<HogwartsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentHistory> StudentHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("school");

        modelBuilder.Entity<House>(entity =>
        {
            entity.ToTable("House");

            entity.Property(e => e.HouseId).HasColumnName("HouseID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentHistory>(entity =>
        {
            entity.ToTable("StudentHistory");

            entity.Property(e => e.StudentHistoryId).HasColumnName("StudentHistoryID");
            entity.Property(e => e.LastestUpdate).HasColumnType("datetime");
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

            entity.HasOne(d => d.HouseFK).WithMany(p => p.StudentHistories)
                .HasForeignKey(d => d.House)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentHistory_House");

            entity.HasOne(d => d.StatusFK).WithMany(p => p.StudentHistories)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentHistory_Status");

            entity.HasOne(d => d.StudentFK).WithMany(p => p.StudentHistories)
                .HasForeignKey(d => d.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentHistory_Student");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
