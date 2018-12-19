namespace SocialNetWorkv1._0.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Soc_NetWorkCF : DbContext
    {
        public Soc_NetWorkCF()
            : base("name=Soc_NetWorkCF")
        {
        }

        public virtual DbSet<BadWord> BadWord { get; set; }
        public virtual DbSet<FriendRequest> FriendRequest { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserFriends> UserFriends { get; set; }
        public virtual DbSet<UserListFriend> UserListFriend { get; set; }
        public virtual DbSet<UserMessege> UserMessege { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BadWord>()
                .Property(e => e.word)
                .IsUnicode(false);

            modelBuilder.Entity<Logins>()
                .Property(e => e.LoginUser)
                .IsUnicode(false);

            modelBuilder.Entity<Logins>()
                .Property(e => e.PasswordUser)
                .IsUnicode(false);

            modelBuilder.Entity<Logins>()
                .HasMany(e => e.FriendRequest)
                .WithOptional(e => e.Logins)
                .HasForeignKey(e => e.FriendID);

            modelBuilder.Entity<Logins>()
                .HasMany(e => e.FriendRequest1)
                .WithOptional(e => e.Logins1)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Logins>()
                .HasMany(e => e.UserFriends)
                .WithRequired(e => e.Logins)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Logins>()
                .HasOptional(e => e.UserInfo)
                .WithRequired(e => e.Logins);

            modelBuilder.Entity<Logins>()
                .HasMany(e => e.UserListFriend)
                .WithOptional(e => e.Logins)
                .HasForeignKey(e => e.IdFriend);

            modelBuilder.Entity<Logins>()
                .HasMany(e => e.UserMessege)
                .WithOptional(e => e.Logins)
                .HasForeignKey(e => e.IDCatcher);

            modelBuilder.Entity<Logins>()
                .HasMany(e => e.UserMessege1)
                .WithOptional(e => e.Logins1)
                .HasForeignKey(e => e.IDrSender);

            modelBuilder.Entity<Roles>()
                .Property(e => e.NameRole)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Logins)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.RoleUser);

            modelBuilder.Entity<UserFriends>()
                .HasMany(e => e.UserListFriend)
                .WithOptional(e => e.UserFriends)
                .HasForeignKey(e => e.UserFrinds);

            modelBuilder.Entity<UserMessege>()
                .Property(e => e.MessageText)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.ImageUser)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
