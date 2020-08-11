namespace WatchShop.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TheWatchShopDbContext : DbContext
    {
        public TheWatchShopDbContext()
            : base("name=TheWatchShopDbContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Orderdetail> Orderdetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Navbar> Navbars { get; set; }
        public virtual DbSet<Link> Links { get; set; }
 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(e => e.fullname)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.deliveryaddress)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.deliveryname)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.deliveryphone)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.deliveryemail)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.detai)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.metadesc)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.fullname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Navbar>()
                .Property(e => e.link)
                .IsUnicode(false);
        }
    }
}
