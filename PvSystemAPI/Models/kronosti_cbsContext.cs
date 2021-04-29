using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PvSystemAPI.Models
{
    public partial class kronosti_cbsContext : DbContext
    {
        public kronosti_cbsContext()
        {
        }

        public kronosti_cbsContext(DbContextOptions<kronosti_cbsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbMessage> TbMessages { get; set; }
        public virtual DbSet<TbTwilioCredential> TbTwilioCredentials { get; set; }
        public virtual DbSet<Tbsendedmessage> Tbsendedmessages { get; set; }
        public virtual DbSet<VsSendedMessage> VsSendedMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("kronosti_paviconuser")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TbMessage>(entity =>
            {
                entity.ToTable("tbMessage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Crdate)
                    .HasColumnType("datetime")
                    .HasColumnName("crdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Messagetxt)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("messagetxt");

                entity.Property(e => e.Tomsg)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tomsg");
            });

            modelBuilder.Entity<TbTwilioCredential>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbTwilioCredential");

                entity.Property(e => e.AccountSid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_SID");

                entity.Property(e => e.AuthToken)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AUTH_TOKEN");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NUMBER");
            });

            modelBuilder.Entity<Tbsendedmessage>(entity =>
            {
                entity.ToTable("tbsendedmessage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Confimationcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("confimationcode");

                entity.Property(e => e.Idmsg).HasColumnName("idmsg");

                entity.HasOne(d => d.IdmsgNavigation)
                    .WithMany(p => p.Tbsendedmessages)
                    .HasForeignKey(d => d.Idmsg)
                    .HasConstraintName("FK_tbsendedmessage_tbMessage");
            });

            modelBuilder.Entity<VsSendedMessage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vsSendedMessage");

                entity.Property(e => e.Confimationcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("confimationcode");

                entity.Property(e => e.Crdate)
                    .HasColumnType("datetime")
                    .HasColumnName("crdate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Messagetxt)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("messagetxt");

                entity.Property(e => e.Tomsg)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tomsg");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
