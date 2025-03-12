using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using API_Exam.Test3;

namespace API_Exam.Test3
{
    [Table("blogs")]
    public class BlogEntity
    {
        // Do not change names of these properties
        [Key]
        [Column("blog_id")]
        public int BlogId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; }

        [NotMapped]
        public bool IsActive { get; set; }

        public List<PostEntity> Articles { get; set; }
    }

    [Table("articles")]
    public class PostEntity
    {
        // Do not change names of these properties
        [Key]
        [Column("post_id")]
        public int PostId { get; set; }

        [Required]
        [Column("blog_id")]
        public int ParentId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        [ForeignKey(nameof(ParentId))]
        public BlogEntity Blog { get; set; }
    }

    public class BlogsContext : DbContext
    {
        // Do not change names of these properties
        public DbSet<BlogEntity> BlogsEntities { get; set; }
        public DbSet<PostEntity> PostsEntities { get; set; }

        public BlogsContext(DbContextOptions<BlogsContext> options)
            : base(options)
        {
            // Do not remove
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO
            modelBuilder.Entity<BlogEntity>(entity =>
            {
                entity.HasKey(x => x.BlogId);
                entity.Property(x => x.BlogId).ValueGeneratedOnAdd();
                entity.Property(x => x.Name).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<PostEntity>(entity =>
            {
                entity.HasKey(x => x.PostId);
                entity.Property(x => x.PostId).ValueGeneratedOnAdd();
                entity.Property(x => x.Name).IsRequired().HasMaxLength(50);
                entity.Property(x => x.Content).IsRequired().HasMaxLength(1000);
            });
        }

        public override int SaveChanges()
        {
            // TODO
            return base.SaveChanges();
        }
    }
}

public class BlogService
{
    private readonly BlogsContext _context;

    public BlogService(BlogsContext context)
    {
        _context = context;
    }

    public async Task<int> AddBlog(string name, bool isActive)
    {
        if (name.Length > 50 || name.Length < 10)
            throw new ArgumentException("Name is invalid");

        var blog = new BlogEntity
        {
            Name = name,
            IsActive = isActive
        };

        await _context.AddAsync(blog);
        return blog.BlogId;
    }
}