using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class T202312Context : DbContext
    {
        public T202312Context()
        {
        }

        public T202312Context(DbContextOptions<T202312Context> options)
            : base(options)
        {
        }

        public virtual DbSet<MAdv> MAdvs { get; set; } = null!;
        public virtual DbSet<MCategory> MCategories { get; set; } = null!;
        public virtual DbSet<MDrawing> MDrawings { get; set; } = null!;
        public virtual DbSet<MEmployee> MEmployees { get; set; } = null!;
        public virtual DbSet<MNumbering> MNumberings { get; set; } = null!;
        public virtual DbSet<MSchool> MSchools { get; set; } = null!;
        public virtual DbSet<MSex> MSexes { get; set; } = null!;
        public virtual DbSet<MSystemSetting> MSystemSettings { get; set; } = null!;
        public virtual DbSet<TCategory> TCategories { get; set; } = null!;
        public virtual DbSet<TContact> TContacts { get; set; } = null!;
        public virtual DbSet<TMtmr001> TMtmr001s { get; set; } = null!;
        public virtual DbSet<TMtmr002> TMtmr002s { get; set; } = null!;
        public virtual DbSet<TMtmr003> TMtmr003s { get; set; } = null!;
        public virtual DbSet<TNew> TNews { get; set; } = null!;
        public virtual DbSet<TOrder> TOrders { get; set; } = null!;
        public virtual DbSet<TOrderDetail> TOrderDetails { get; set; } = null!;
        public virtual DbSet<TPost> TPosts { get; set; } = null!;
        public virtual DbSet<TProduct> TProducts { get; set; } = null!;
        public virtual DbSet<TProductCategory> TProductCategories { get; set; } = null!;
        public virtual DbSet<TSubscrice> TSubscrices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MAdv>(entity =>
            {
                entity.ToTable("M_Adv");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Link).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MCategory>(entity =>
            {
                entity.ToTable("M_Category");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.SeoDescription).HasMaxLength(550);

                entity.Property(e => e.SeoKeyWords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MDrawing>(entity =>
            {
                entity.HasKey(e => e.DrawingFileNo);

                entity.ToTable("M_Drawing");

                entity.Property(e => e.DrawingFileNo)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.DrawingFileExt).HasMaxLength(4);

                entity.Property(e => e.DrawingFileName).HasMaxLength(50);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MEmployee>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("M_Employee");

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.AuthorityCode)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.UserNm).HasMaxLength(50);
            });

            modelBuilder.Entity<MNumbering>(entity =>
            {
                entity.HasKey(e => e.NumberingNo);

                entity.ToTable("M_Numbering");

                entity.Property(e => e.NumberingNo)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Numbering).HasMaxLength(10);

                entity.Property(e => e.NumberingName).HasMaxLength(50);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MSchool>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("m_Schools");

                entity.Property(e => e.DeleteFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.SchoolId)
                    .HasMaxLength(1)
                    .HasColumnName("SchoolID")
                    .IsFixedLength();

                entity.Property(e => e.SchoolName).HasMaxLength(50);
            });

            modelBuilder.Entity<MSex>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("m_Sex");

                entity.Property(e => e.DeleteFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.SexId)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.SexName).HasMaxLength(50);
            });

            modelBuilder.Entity<MSystemSetting>(entity =>
            {
                entity.HasKey(e => e.SettingKey);

                entity.ToTable("M_SystemSetting");

                entity.Property(e => e.SettingKey).HasMaxLength(50);

                entity.Property(e => e.SettingDescription).HasMaxLength(500);
            });

            modelBuilder.Entity<TCategory>(entity =>
            {
                entity.ToTable("T_Category");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Detail).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.SeoDescription).HasMaxLength(550);

                entity.Property(e => e.SeoKeyWords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TContact>(entity =>
            {
                entity.ToTable("T_Contact");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Message).HasMaxLength(4000);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Website).HasMaxLength(150);
            });

            modelBuilder.Entity<TMtmr001>(entity =>
            {
                entity.HasKey(e => e.Mtmr001No);

                entity.ToTable("T_MTMR001");

                entity.Property(e => e.Mtmr001No)
                    .HasMaxLength(10)
                    .HasColumnName("MTMR001_No")
                    .IsFixedLength();

                entity.Property(e => e.CustomerAd).HasMaxLength(50);

                entity.Property(e => e.CustomerCd)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.CustomerContact).HasMaxLength(50);

                entity.Property(e => e.Deadline)
                    .HasMaxLength(50)
                    .HasColumnName("deadline");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.DeliveryLocation).HasMaxLength(50);

                entity.Property(e => e.EstimatedDate).HasColumnType("date");

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Payment).HasMaxLength(50);

                entity.Property(e => e.QuoteExpirationDate).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.Subject)
                    .HasMaxLength(50)
                    .HasColumnName("subject");

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.UserId)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TMtmr002>(entity =>
            {
                entity.HasKey(e => new { e.Mtmr002No, e.Mtmr002Index });

                entity.ToTable("T_MTMR002");

                entity.Property(e => e.Mtmr002No)
                    .HasMaxLength(10)
                    .HasColumnName("MTMR002_no")
                    .IsFixedLength();

                entity.Property(e => e.Mtmr002Index).HasColumnName("MTMR002_Index");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.DrawingFileName).HasMaxLength(50);

                entity.Property(e => e.DrawingFileNo)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.DrawingNo).HasMaxLength(50);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.OrderTxt).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .HasColumnName("remarks");

                entity.Property(e => e.SetFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TMtmr003>(entity =>
            {
                entity.HasKey(e => new { e.Mtmr003No, e.Mtmr003Index });

                entity.ToTable("T_MTMR003");

                entity.Property(e => e.Mtmr003No)
                    .HasMaxLength(10)
                    .HasColumnName("MTMR003_no")
                    .IsFixedLength();

                entity.Property(e => e.Mtmr003Index).HasColumnName("MTMR003_Index");

                entity.Property(e => e.Content).HasMaxLength(50);

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TNew>(entity =>
            {
                entity.ToTable("T_New");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Detail).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.SeoDescription).HasMaxLength(550);

                entity.Property(e => e.SeoKeyWords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.ToTable("T_Order");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(150);

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TOrderDetail>(entity =>
            {
                entity.ToTable("T_OrderDetail");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TPost>(entity =>
            {
                entity.ToTable("T_Post");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Detail).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.SeoDescription).HasMaxLength(550);

                entity.Property(e => e.SeoKeyWords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.ToTable("T_Product");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Detail).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceSale).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.SeoDescription).HasMaxLength(550);

                entity.Property(e => e.SeoKeyWords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TProductCategory>(entity =>
            {
                entity.ToTable("T_ProductCategory");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TSubscrice>(entity =>
            {
                entity.ToTable("T_Subscrice");

                entity.Property(e => e.DelFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.InsCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.InsDate).HasColumnType("datetime");

                entity.Property(e => e.InsUser)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.UpdCd)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdUser)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.DeleteFlg)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
