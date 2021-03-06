using AngularApplicationTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularApplicationTest.DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<PostLike> PostLikes { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=servername;Initial Catalog=dbName;Integrated Security=False;User Id=sa;Password=Password;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");
                entity.HasKey("Id");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Content).HasColumnName("Content").IsRequired();
                entity.Property(e => e.ImageFileName).HasColumnName("ImageFileName");
                entity.Property(e => e.PostedOn).HasColumnName("PostedOn").HasColumnType("datetime");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");
                entity.HasKey("Id");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.PostId).HasColumnName("PostId");
                entity.Property(e => e.Content).HasColumnName("Content").IsRequired();
                entity.Property(e => e.PostedOn).HasColumnName("PostedOn").HasColumnType("datetime");
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.ToTable("PostLike");
                entity.HasKey("Id");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.PostId).HasColumnName("PostId");
                entity.Property(e => e.LikedOn).HasColumnName("LikedOn").HasColumnType("datetime");
            });

        }
    }
}
