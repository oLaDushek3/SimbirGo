using Microsoft.EntityFrameworkCore;
using TestApi.Repositories;

namespace TestApi.Models;

public partial class SimbirGoContext : DbContext
{
    private readonly DbConnectionString _dbConnectionString;
    public SimbirGoContext(DbConnectionString dbConnectionString)
    {
        _dbConnectionString = dbConnectionString;
    }

    public SimbirGoContext(DbContextOptions<SimbirGoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }
    
    public virtual DbSet<UsersSessions> UsersSessions { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<PriceType> PriceTypes { get; set; }

    public virtual DbSet<Rent> Rents { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<TransportModel> TransportModels { get; set; }

    public virtual DbSet<TransportPriceType> TransportPriceTypes { get; set; }

    public virtual DbSet<TransportType> TransportTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(_dbConnectionString.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pkey");

            entity.ToTable("account");

            entity.HasIndex(e => e.Username, "account_username_key").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UsersSessions>(entity =>
        {
            entity.HasKey(e => e.UsersSessionsId).HasName("users_sessions_pkey");

            entity.ToTable("users_sessions");

            entity.Property(e => e.UsersSessionsId).HasColumnName("users_sessions_id");
            entity.Property(e => e.ValidSession).HasColumnName("valid_session");
            entity.Property(e => e.AccountId ).HasColumnName("account_id");
 
            entity.HasOne(d => d.Account).WithMany(p => p.UsersSessions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("users_sessions_account_id_fkey");
        });
        
        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("color_pkey");

            entity.ToTable("color");

            entity.HasIndex(e => e.Name, "color_name_key").IsUnique();

            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PriceType>(entity =>
        {
            entity.HasKey(e => e.PriceTypeId).HasName("price_type_pkey");

            entity.ToTable("price_type");

            entity.Property(e => e.PriceTypeId).HasColumnName("price_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(75)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Rent>(entity =>
        {
            entity.HasKey(e => e.RentId).HasName("rent_pkey");

            entity.ToTable("rent");

            entity.Property(e => e.RentId).HasColumnName("rent_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.FinalPrice).HasColumnName("final_price");
            entity.Property(e => e.PriceOfUnit).HasColumnName("price_of_unit");
            entity.Property(e => e.TimeEnd).HasColumnName("time_end");
            entity.Property(e => e.TimeStart).HasColumnName("time_start");
            entity.Property(e => e.TransportPriceTypeId).HasColumnName("transport_price_type_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Rents)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("rent_account_id_fkey");

            entity.HasOne(d => d.TransportPriceType).WithMany(p => p.Rents)
                .HasForeignKey(d => d.TransportPriceTypeId)
                .HasConstraintName("rent_transport_price_type_id_fkey");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportId).HasName("transport_pkey");

            entity.ToTable("transport");

            entity.HasIndex(e => e.CanBeRented, "transport_can_be_rented_key").IsUnique();

            entity.HasIndex(e => e.Description, "transport_description_key").IsUnique();

            entity.HasIndex(e => e.Identifier, "transport_identifier_key").IsUnique();

            entity.HasIndex(e => e.Latitude, "transport_latitude_key").IsUnique();

            entity.HasIndex(e => e.Longitude, "transport_longitude_key").IsUnique();

            entity.Property(e => e.TransportId).HasColumnName("transport_id");
            entity.Property(e => e.CanBeRented).HasColumnName("can_be_rented");
            entity.Property(e => e.ColorId).HasColumnName("color_id");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Identifier)
                .HasMaxLength(9)
                .HasColumnName("identifier");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.TransportModelId).HasColumnName("transport_model_id");

            entity.HasOne(d => d.Color).WithMany(p => p.Transports)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("transport_color_id_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.Transports)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("transport_owner_id_fkey");

            entity.HasOne(d => d.TransportModel).WithMany(p => p.Transports)
                .HasForeignKey(d => d.TransportModelId)
                .HasConstraintName("transport_transport_model_id_fkey");
        });

        modelBuilder.Entity<TransportModel>(entity =>
        {
            entity.HasKey(e => e.TransportModelId).HasName("transport_model_pkey");

            entity.ToTable("transport_model");

            entity.HasIndex(e => e.Name, "transport_model_name_key").IsUnique();

            entity.Property(e => e.TransportModelId).HasColumnName("transport_model_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.TransportTypeId).HasColumnName("transport_type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.TransportModels)
                .HasForeignKey(d => d.TransportTypeId)
                .HasConstraintName("transport_model_type_id_fkey");
        });

        modelBuilder.Entity<TransportPriceType>(entity =>
        {
            entity.HasKey(e => e.TransportPriceTypeId).HasName("transport_price_type_pkey");

            entity.ToTable("transport_price_type");

            entity.Property(e => e.TransportPriceTypeId).HasColumnName("transport_price_type_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PriceTypeId).HasColumnName("price_type_id");
            entity.Property(e => e.TransportId).HasColumnName("transport_id");

            entity.HasOne(d => d.PriceType).WithMany(p => p.TransportPriceTypes)
                .HasForeignKey(d => d.PriceTypeId)
                .HasConstraintName("transport_price_type_price_type_id_fkey");

            entity.HasOne(d => d.Transport).WithMany(p => p.TransportPriceTypes)
                .HasForeignKey(d => d.TransportId)
                .HasConstraintName("transport_price_type_transport_id_fkey");
        });

        modelBuilder.Entity<TransportType>(entity =>
        {
            entity.HasKey(e => e.TransportTypeId).HasName("transport_type_pkey");

            entity.ToTable("transport_type");

            entity.HasIndex(e => e.Name, "transport_type_name_key").IsUnique();

            entity.Property(e => e.TransportTypeId).HasColumnName("transport_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
