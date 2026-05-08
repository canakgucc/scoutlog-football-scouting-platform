using Microsoft.EntityFrameworkCore;
using ScoutLog.Domain.Entities;

namespace ScoutLog.Infrastructure.Persistence;

public class ScoutLogDbContext(DbContextOptions<ScoutLogDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Club> Clubs => Set<Club>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<ScoutReport> ScoutReports => Set<ScoutReport>();
    public DbSet<PerformanceMetric> PerformanceMetrics => Set<PerformanceMetric>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(role => role.Id);
            entity.HasIndex(role => role.Name).IsUnique();
            entity.Property(role => role.Name).HasMaxLength(50).IsRequired();
            entity.Property(role => role.Description).HasMaxLength(250);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(user => user.Id);
            entity.HasIndex(user => user.Email).IsUnique();
            entity.Property(user => user.FullName).HasMaxLength(120).IsRequired();
            entity.Property(user => user.Email).HasMaxLength(160).IsRequired();
            entity.Property(user => user.PasswordHash).HasMaxLength(500).IsRequired();
            entity.Property(user => user.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(user => user.Club)
                .WithMany(club => club.Users)
                .HasForeignKey(user => user.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(user => user.Team)
                .WithMany(team => team.Users)
                .HasForeignKey(user => user.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.ToTable("Clubs");
            entity.HasKey(club => club.Id);
            entity.Property(club => club.Name).HasMaxLength(120).IsRequired();
            entity.Property(club => club.City).HasMaxLength(80).IsRequired();
            entity.Property(club => club.Country).HasMaxLength(80).IsRequired();
            entity.Property(club => club.LogoUrl).HasMaxLength(500);
            entity.Property(club => club.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("Teams");
            entity.HasKey(team => team.Id);
            entity.Property(team => team.Name).HasMaxLength(120).IsRequired();
            entity.Property(team => team.AgeGroup).HasMaxLength(40).IsRequired();
            entity.Property(team => team.Season).HasMaxLength(20).IsRequired();

            entity.HasOne(team => team.Club)
                .WithMany(club => club.Teams)
                .HasForeignKey(team => team.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(team => team.Coach)
                .WithMany()
                .HasForeignKey(team => team.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Players");
            entity.HasKey(player => player.Id);
            entity.Property(player => player.FirstName).HasMaxLength(80).IsRequired();
            entity.Property(player => player.LastName).HasMaxLength(80).IsRequired();
            entity.Property(player => player.Position).HasMaxLength(40).IsRequired();
            entity.Property(player => player.PreferredFoot).HasMaxLength(20).IsRequired();
            entity.Property(player => player.Nationality).HasMaxLength(80).IsRequired();
            entity.Property(player => player.PhotoUrl).HasMaxLength(500);
            entity.Property(player => player.Status).HasMaxLength(40).IsRequired();

            entity.HasOne(player => player.Club)
                .WithMany(club => club.Players)
                .HasForeignKey(player => player.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(player => player.Team)
                .WithMany(team => team.Players)
                .HasForeignKey(player => player.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ScoutReport>(entity =>
        {
            entity.ToTable("ScoutReports");
            entity.HasKey(report => report.Id);
            entity.Property(report => report.Title).HasMaxLength(160).IsRequired();
            entity.Property(report => report.ObservationText).IsRequired();
            entity.Property(report => report.Recommendation).HasMaxLength(80).IsRequired();
            entity.Property(report => report.Tags).HasMaxLength(500);
            entity.Property(report => report.DevelopmentAdvice).HasMaxLength(1000);
            entity.Property(report => report.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(report => report.Player)
                .WithMany(player => player.ScoutReports)
                .HasForeignKey(report => report.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(report => report.Scout)
                .WithMany(user => user.ScoutReports)
                .HasForeignKey(report => report.ScoutId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PerformanceMetric>(entity =>
        {
            entity.ToTable("PerformanceMetrics");
            entity.HasKey(metric => metric.Id);
            entity.Property(metric => metric.MatchName).HasMaxLength(160).IsRequired();

            entity.HasOne(metric => metric.Player)
                .WithMany(player => player.PerformanceMetrics)
                .HasForeignKey(metric => metric.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(notification => notification.Id);
            entity.Property(notification => notification.Title).HasMaxLength(160).IsRequired();
            entity.Property(notification => notification.Message).HasMaxLength(500).IsRequired();
            entity.Property(notification => notification.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(notification => notification.User)
                .WithMany(user => user.Notifications)
                .HasForeignKey(notification => notification.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        SeedDemoData(modelBuilder);
    }

    private static void SeedDemoData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "System administrator" },
            new Role { Id = 2, Name = "ClubManager", Description = "Club manager" },
            new Role { Id = 3, Name = "Coach", Description = "Team coach" },
            new Role { Id = 4, Name = "Scout", Description = "Scout user" });

        modelBuilder.Entity<Club>().HasData(
            new Club
            {
                Id = 1,
                Name = "Fenerbahçe Futbol Kulübü",
                City = "Istanbul",
                Country = "Turkey",
                LogoUrl = "https://placehold.co/160x160?text=FB",
                CreatedAt = seedDate
            },
            new Club
            {
                Id = 2,
                Name = "Galatasaray Spor Kulübü",
                City = "Istanbul",
                Country = "Turkey",
                LogoUrl = "https://placehold.co/160x160?text=GS",
                CreatedAt = seedDate
            },
            new Club
            {
                Id = 3,
                Name = "Beşiktaş Jimnastik Kulübü",
                City = "Istanbul",
                Country = "Turkey",
                LogoUrl = "https://placehold.co/160x160?text=BJK",
                CreatedAt = seedDate
            });

        modelBuilder.Entity<Team>().HasData(
            new
            {
                Id = 1,
                ClubId = 1,
                Name = "Fenerbahçe U19 Elite",
                AgeGroup = "U19",
                Season = "2025/26",
                CoachId = (int?)null
            },
            new
            {
                Id = 2,
                ClubId = 2,
                Name = "Galatasaray U19 Elite",
                AgeGroup = "U19",
                Season = "2025/26",
                CoachId = (int?)null
            },
            new
            {
                Id = 3,
                ClubId = 3,
                Name = "Beşiktaş U19 Elite",
                AgeGroup = "U19",
                Season = "2025/26",
                CoachId = (int?)null
            });

        modelBuilder.Entity<User>().HasData(
            new
            {
                Id = 1,
                FullName = "Fenerbahçe Admin",
                Email = "admin@fenerbahce.local",
                PasswordHash = "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16",
                IsActive = true,
                CreatedAt = seedDate,
                RoleId = 1,
                ClubId = (int?)1,
                TeamId = (int?)null
            },
            new
            {
                Id = 2,
                FullName = "Fenerbahçe Manager",
                Email = "manager@fenerbahce.local",
                PasswordHash = "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16",
                IsActive = true,
                CreatedAt = seedDate,
                RoleId = 2,
                ClubId = (int?)1,
                TeamId = (int?)null
            },
            new
            {
                Id = 3,
                FullName = "Fenerbahçe Coach",
                Email = "coach@fenerbahce.local",
                PasswordHash = "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16",
                IsActive = true,
                CreatedAt = seedDate,
                RoleId = 3,
                ClubId = (int?)1,
                TeamId = (int?)1
            },
            new
            {
                Id = 4,
                FullName = "Fenerbahçe Scout",
                Email = "scout@fenerbahce.local",
                PasswordHash = "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16",
                IsActive = true,
                CreatedAt = seedDate,
                RoleId = 4,
                ClubId = (int?)1,
                TeamId = (int?)1
            },
            new
            {
                Id = 5,
                FullName = "Galatasaray Scout",
                Email = "scout@galatasaray.local",
                PasswordHash = "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16",
                IsActive = true,
                CreatedAt = seedDate,
                RoleId = 4,
                ClubId = (int?)2,
                TeamId = (int?)2
            },
            new
            {
                Id = 6,
                FullName = "Beşiktaş Scout",
                Email = "scout@besiktas.local",
                PasswordHash = "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16",
                IsActive = true,
                CreatedAt = seedDate,
                RoleId = 4,
                ClubId = (int?)3,
                TeamId = (int?)3
            });

        modelBuilder.Entity<Player>().HasData(new
        {
            Id = 1,
            ClubId = 1,
            TeamId = (int?)1,
            FirstName = "Emir",
            LastName = "Yildiz",
            BirthDate = new DateOnly(2007, 4, 12),
            Position = "Winger",
            PreferredFoot = "Right",
            Height = 178,
            Weight = 70,
            Nationality = "Turkey",
            PhotoUrl = "https://placehold.co/320x320?text=EY",
            Status = "Active"
        });

        modelBuilder.Entity<ScoutReport>().HasData(new
        {
            Id = 1,
            PlayerId = 1,
            ScoutId = 4,
            Title = "Initial wing prospect report",
            ObservationText = "Oyuncu hız, pres ve pas bağlantılarında etkili göründü. Pozisyon alma kararları zaman zaman geliştirilmeli.",
            TechnicalScore = 78,
            PhysicalScore = 82,
            TacticalScore = 70,
            MentalScore = 74,
            PotentialScore = 86,
            OverallScore = 78,
            Recommendation = "Izlenmeli",
            AnalysisSummary = (string?)null,
            Strengths = (string?)null,
            Weaknesses = (string?)null,
            SuggestedActions = (string?)null,
            SuggestedScore = (int?)null,
            Tags = (string?)null,
            DevelopmentAdvice = (string?)null,
            CreatedAt = seedDate
        });

        modelBuilder.Entity<Player>().HasData(CreateAdditionalDemoPlayers());
        modelBuilder.Entity<ScoutReport>().HasData(CreateAdditionalDemoReports(seedDate));
    }

    private static Player[] CreateAdditionalDemoPlayers()
    {
        return
        [
            DemoPlayer(1001, "Mert", "Aydin", "Goalkeeper", "Right", 188, 82, new DateOnly(2007, 2, 3)),
            DemoPlayer(1002, "Kerem", "Arslan", "Centre Back", "Right", 186, 80, new DateOnly(2006, 11, 18)),
            DemoPlayer(1003, "Bora", "Demir", "Centre Back", "Left", 184, 78, new DateOnly(2007, 6, 7)),
            DemoPlayer(1004, "Efe", "Koc", "Left Back", "Left", 176, 70, new DateOnly(2008, 1, 22)),
            DemoPlayer(1005, "Deniz", "Sahin", "Right Back", "Right", 177, 72, new DateOnly(2007, 9, 14)),
            DemoPlayer(1006, "Ali", "Kaya", "Defensive Midfielder", "Right", 180, 74, new DateOnly(2006, 8, 9)),
            DemoPlayer(1007, "Yigit", "Ozkan", "Central Midfielder", "Right", 178, 71, new DateOnly(2007, 12, 1)),
            DemoPlayer(1008, "Can", "Polat", "Attacking Midfielder", "Left", 174, 67, new DateOnly(2008, 3, 5)),
            DemoPlayer(1009, "Ozan", "Er", "Winger", "Right", 175, 68, new DateOnly(2007, 5, 16)),
            DemoPlayer(1010, "Arda", "Gunes", "Winger", "Left", 173, 66, new DateOnly(2008, 4, 25)),
            DemoPlayer(1011, "Baran", "Yilmaz", "Striker", "Right", 183, 77, new DateOnly(2006, 10, 30)),
            DemoPlayer(1012, "Kaan", "Celik", "Striker", "Left", 181, 75, new DateOnly(2007, 7, 11)),
            DemoPlayer(1013, "Umut", "Sari", "Goalkeeper", "Left", 190, 84, new DateOnly(2006, 12, 20)),
            DemoPlayer(1014, "Selim", "Kurt", "Centre Back", "Right", 187, 81, new DateOnly(2007, 1, 13)),
            DemoPlayer(1015, "Atakan", "Aslan", "Defensive Midfielder", "Right", 182, 76, new DateOnly(2007, 8, 2)),
            DemoPlayer(1016, "Tuna", "Acar", "Central Midfielder", "Left", 177, 69, new DateOnly(2008, 2, 17)),
            DemoPlayer(1017, "Doruk", "Eren", "Attacking Midfielder", "Right", 176, 68, new DateOnly(2007, 3, 29)),
            DemoPlayer(1018, "Berk", "Kaplan", "Winger", "Right", 172, 65, new DateOnly(2008, 6, 10)),
            DemoPlayer(1019, "Ruzgar", "Cinar", "Winger", "Left", 174, 66, new DateOnly(2007, 9, 3)),
            DemoPlayer(1020, "Eren", "Bulut", "Striker", "Right", 185, 79, new DateOnly(2006, 5, 28)),
            DemoPlayer(1021, "Batuhan", "Sen", "Left Back", "Left", 179, 73, new DateOnly(2007, 4, 19)),
            DemoPlayer(1022, "Sarp", "Yuce", "Right Back", "Right", 178, 72, new DateOnly(2008, 8, 8)),
            DemoPlayer(1023, "Onur", "Tekin", "Central Midfielder", "Right", 181, 74, new DateOnly(2006, 9, 24)),
            DemoPlayer(1024, "Furkan", "Aksoy", "Striker", "Right", 184, 78, new DateOnly(2007, 11, 6))
        ];
    }

    private static ScoutReport[] CreateAdditionalDemoReports(DateTime seedDate)
    {
        return
        [
            DemoReport(2001, 1001, "Kaleci refleks ve oyun kurulum raporu", "Kaleci yan toplarda güven verdi, oyun görüşü ve kısa pas başlangıçlarında sakindi. Kondisyon son bölümde korunmalı.", 68, 77, 72, 76, 74, "Takip Edilmeli", "Refleksler; Pasla oyun kurma", "Fiziksel dayanıklılık", 6, "gelişim gerekli", "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", seedDate.AddDays(1)),
            DemoReport(2002, 1002, "Stoper markaj raporu", "Oyuncu ikili mücadelede güçlüydü ancak markaj takibinde bir pozisyon hatası yaptı. Savunma dönüşü daha erken başlamalı.", 66, 82, 70, 68, 76, "Gelistirilmeli", "İkili mücadele gücü", "Savunma farkındalığı", 5, "gelişim gerekli", "Savunma pozisyon alma ve markaj takip çalışmaları önerilir", seedDate.AddDays(2)),
            DemoReport(2003, 1003, "Sol stoper pas açısı raporu", "Sol ayak pas kalitesi iyi, oyun görüşü sayesinde ilk baskıyı kırdı. Riskli tercih sayısı düşük kaldı.", 76, 78, 77, 74, 82, "Izlenmeli", "Pas kalitesi ve oyun görüşü", "Belirgin kritik gelişim alanı yok", 8, "teknik oyuncu; takip edilmeli", "Pas temposu üst seviye maçlarda test edilmeli", seedDate.AddDays(3)),
            DemoReport(2004, 1004, "Sol bek sprint raporu", "Sol çizgide hızlı bindirmeler yaptı, sprint temposu etkiliydi. Savunma dönüşlerinde zaman zaman geç kaldı.", 72, 84, 69, 70, 81, "Izlenmeli", "Hız ve sprint aksiyonları", "Savunma farkındalığı", 7, "hizli oyuncu; gelişim gerekli", "Savunma dönüşü ve pozisyon alma çalışmaları önerilir", seedDate.AddDays(4)),
            DemoReport(2005, 1005, "Sağ bek karar verme raporu", "Topla çıkışta pas bağlantısı iyi fakat baskı altında karar verme konusunda riskli tercih yaptı.", 73, 79, 71, 66, 75, "Takip Edilmeli", "Pas bağlantısı", "Karar verme", 6, "teknik oyuncu; gelişim gerekli", "Baskı altında karar verme ve risksiz pas tercihleri çalışılmalı", seedDate.AddDays(5)),
            DemoReport(2006, 1006, "Ön libero denge raporu", "Merkezde top kapma ve pas istasyonu rolünü iyi oynadı. Oyun görüşü gelişmiş, top kaybı sayısı düşük.", 78, 80, 82, 76, 84, "Izlenmeli", "Pas kalitesi ve oyun görüşü", "Belirgin kritik gelişim alanı yok", 8, "teknik oyuncu; takip edilmeli", "Daha fazla dikine pas denemesiyle gelişim desteklenmeli", seedDate.AddDays(6)),
            DemoReport(2007, 1007, "Merkez orta saha tempo raporu", "Oyuncu pas ritmini iyi ayarladı, asist öncesi pası değerliydi. Kondisyon olarak son 15 dakikada yoruldu.", 80, 72, 78, 75, 83, "Izlenmeli", "Pas kalitesi; Asist hazırlığı", "Fiziksel dayanıklılık", 7, "teknik oyuncu; gelişim gerekli", "Fiziksel dayanıklılık ve maç sonu tempo koruma çalışmaları önerilir", seedDate.AddDays(7)),
            DemoReport(2008, 1008, "On numara yaratıcılık raporu", "Oyun görüşü üst düzey, dar alanda pas ve asist tehdidi yarattı. Şut tercihleri daha dengeli olmalı.", 84, 70, 79, 76, 88, "Transfer Onerisi", "Pas kalitesi ve oyun görüşü; Hücum üretkenliği", "Şut tercihi dengesi", 9, "teknik oyuncu; hucum katkisi; takip edilmeli", "Final kararları ve şut-pas tercihi çalışılmalı", seedDate.AddDays(8)),
            DemoReport(2009, 1009, "Sağ kanat hız raporu", "Oyuncu çok hızlı, sprint tekrarlarında etkili ve bire birde rakibi eksiltti. Savunma dönüşü geç başladı.", 79, 86, 70, 72, 87, "Izlenmeli", "Hız ve sprint aksiyonları", "Savunma farkındalığı", 7, "hizli oyuncu; gelişim gerekli", "Savunma dönüşü ve ters kanat kapatma çalışmaları önerilir", seedDate.AddDays(9)),
            DemoReport(2010, 1010, "Sol kanat bitiricilik raporu", "Sol kanatta çabuk yön değiştirdi, şut kalitesi ve bitiricilik dikkat çekti. Karar verme bazı hücumlarda gecikti.", 82, 78, 73, 70, 86, "Izlenmeli", "Hız; Hücum ve bitiricilik tehdidi", "Karar verme", 7, "hizli oyuncu; hucum katkisi; gelişim gerekli", "Son bölgede karar verme ve pas-şut tercihi çalışılmalı", seedDate.AddDays(10)),
            DemoReport(2011, 1011, "Santrfor bitiricilik raporu", "Ceza sahasında gol kokusu yüksek, bitiricilik ve ilk temas kalitesi iyi. Pres sürekliliği artırılmalı.", 81, 80, 72, 75, 85, "Izlenmeli", "Hücum ve bitiricilik tehdidi", "Pres sürekliliği", 8, "hucum katkisi; takip edilmeli", "Ön alan presinde süreklilik çalışmaları önerilir", seedDate.AddDays(11)),
            DemoReport(2012, 1012, "Santrfor bağlantı oyunu raporu", "Sırtı dönük oyunda pas bağlantısı kurdu, asist tehdidi yarattı. Fiziksel temaslarda daha dengeli kalmalı.", 77, 76, 75, 74, 80, "Takip Edilmeli", "Pas bağlantısı; Asist tehdidi", "Fiziksel temas dengesi", 7, "teknik oyuncu; takip edilmeli", "Gövde kullanımı ve temas sonrası devam oyunu çalışılmalı", seedDate.AddDays(12)),
            DemoReport(2013, 1013, "Kaleci yan top raporu", "Yan toplarda çıkış zamanlaması iyi, uzun pas denemeleri isabetliydi. Riskli tercih sayısı bir pozisyonda arttı.", 70, 81, 72, 69, 78, "Gelistirilmeli", "Uzun pas kalitesi", "Karar verme", 6, "teknik oyuncu; gelişim gerekli", "Baskı altında karar verme ve güvenli oyun başlangıcı çalışılmalı", seedDate.AddDays(13)),
            DemoReport(2014, 1014, "Stoper hava hakimiyeti raporu", "Hava toplarında etkili, markaj temasını iyi kullandı. Topla çıkışta pas kalitesi gelişmeli.", 64, 84, 74, 76, 79, "Takip Edilmeli", "Hava topu ve markaj gücü", "Pas kalitesi", 6, "gelişim gerekli", "İlk pas ve baskı kırma çalışmaları önerilir", seedDate.AddDays(14)),
            DemoReport(2015, 1015, "Ön libero savunma geçişi raporu", "Savunma dönüşü disiplinli, ikinci topları topladı. Pas temposu daha hızlı olabilir.", 72, 82, 80, 77, 82, "Izlenmeli", "Savunma geçişi; İkinci top takibi", "Pas temposu", 8, "takip edilmeli", "Tek temas pas temposu çalışmaları önerilir", seedDate.AddDays(15)),
            DemoReport(2016, 1016, "Merkez orta saha oyun görüşü raporu", "Oyun görüşü ve pas açısı çok iyi, iki asist hazırladı. Kondisyon olarak son bölümde fiziksel düşüş yaşadı.", 83, 70, 80, 76, 87, "Izlenmeli", "Pas kalitesi ve oyun görüşü", "Fiziksel dayanıklılık", 7, "teknik oyuncu; gelişim gerekli", "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", seedDate.AddDays(16)),
            DemoReport(2017, 1017, "On numara final pas raporu", "Dar alanda pas, asist ve şut tehdidi yarattı. Top kaybı sonrası reaksiyonunu hızlandırmalı.", 85, 72, 78, 74, 89, "Transfer Onerisi", "Pas kalitesi; Hücum ve bitiricilik tehdidi", "Karar verme", 8, "teknik oyuncu; hucum katkisi; gelişim gerekli", "Top kaybı sonrası reaksiyon ve final kararı çalışılmalı", seedDate.AddDays(17)),
            DemoReport(2018, 1018, "Sağ kanat bire bir raporu", "Çabuk ilk adımı ve sprint çıkışı iyi. Bire bir sonrası pas tercihi zaman zaman riskli tercih oldu.", 78, 85, 70, 68, 84, "Izlenmeli", "Hız ve sprint aksiyonları", "Karar verme", 7, "hizli oyuncu; gelişim gerekli", "Son aksiyon karar verme ve pas kalitesi çalışılmalı", seedDate.AddDays(18)),
            DemoReport(2019, 1019, "Sol kanat ters ayak raporu", "İçe kat edip şut tehdidi yarattı, gol pozisyonuna girdi. Savunma dönüşü ve markaj takibi gelişmeli.", 80, 82, 69, 70, 85, "Izlenmeli", "Hücum ve bitiricilik tehdidi; Hız", "Savunma farkındalığı", 7, "hizli oyuncu; hucum katkisi; gelişim gerekli", "Savunma pozisyon alma ve markaj takibi çalışılmalı", seedDate.AddDays(19)),
            DemoReport(2020, 1020, "Santrfor pres raporu", "Ön alan presi ve gol vuruşu güçlüydü. Kondisyon düşüşü nedeniyle son bölümde baskısı azaldı.", 79, 83, 73, 75, 84, "Takip Edilmeli", "Hücum ve bitiricilik tehdidi; Pres", "Fiziksel dayanıklılık", 7, "hucum katkisi; gelişim gerekli", "Fiziksel dayanıklılık ve pres sürekliliği çalışılmalı", seedDate.AddDays(20)),
            DemoReport(2021, 1021, "Sol bek savunma raporu", "Savunma dönüşü genel olarak iyi ancak bir pozisyon hatası yaptı. Pas çıkışları sade ve doğruydu.", 70, 78, 76, 72, 77, "Gelistirilmeli", "Sade pas oyunu", "Savunma farkındalığı", 6, "gelişim gerekli", "Pozisyon alma ve çizgi savunması çalışılmalı", seedDate.AddDays(21)),
            DemoReport(2022, 1022, "Sağ bek bindirme raporu", "Hızlı bindirmeler ve sprint tekrarları iyi. Orta kalitesi gelişirse asist katkısı artar.", 72, 84, 72, 73, 81, "Izlenmeli", "Hız ve sprint aksiyonları", "Orta kalitesi", 8, "hizli oyuncu; takip edilmeli", "Kanat orta kalitesi ve final pas çalışmaları önerilir", seedDate.AddDays(22)),
            DemoReport(2023, 1023, "Merkez orta saha liderlik raporu", "Pas yönü değiştirme ve oyun görüşü iyi. Karar verme genelde doğru, top kaybı az.", 82, 78, 84, 82, 86, "Izlenmeli", "Pas kalitesi ve oyun görüşü; Liderlik", "Belirgin kritik gelişim alanı yok", 8, "teknik oyuncu; takip edilmeli", "Üst yaş grubu temposunda test edilmeli", seedDate.AddDays(23)),
            DemoReport(2024, 1024, "Santrfor gol raporu", "Gol vuruşu ve bitiricilik net şekilde öne çıktı. Sırtı dönük pas bağlantısı geliştirilmeli.", 80, 82, 71, 73, 83, "Takip Edilmeli", "Hücum ve bitiricilik tehdidi", "Bağlantı oyunu", 8, "hucum katkisi; takip edilmeli", "Sırtı dönük oyun ve duvar pası çalışılmalı", seedDate.AddDays(24)),
            DemoReport(2025, 1008, "Yaratıcı orta saha tekrar izleme", "Oyun görüşü, asist ve pas kalitesi yine güçlüydü. Karar verme baskı altında daha da hızlanmalı.", 86, 72, 81, 76, 90, "Transfer Onerisi", "Pas kalitesi ve oyun görüşü; Asist tehdidi", "Karar verme", 8, "teknik oyuncu; gelişim gerekli", "Baskı altında hızlı karar verme çalışmaları önerilir", seedDate.AddDays(25)),
            DemoReport(2026, 1009, "Kanat oyuncusu ikinci rapor", "Hızlı ve çabuk driplinglerle çizgiyi kullandı. Savunma dönüşü geç kaldığı anlar var.", 80, 87, 72, 73, 88, "Izlenmeli", "Hız ve sprint aksiyonları", "Savunma farkındalığı", 7, "hizli oyuncu; gelişim gerekli", "Savunma dönüşü ve bek yardımı çalışılmalı", seedDate.AddDays(26)),
            DemoReport(2027, 1010, "Sol kanat final aksiyon raporu", "Şut ve gol tehdidi güçlü, pas tercihi bazı anlarda daha doğru olabilirdi.", 83, 79, 74, 71, 87, "Izlenmeli", "Hücum ve bitiricilik tehdidi", "Karar verme", 7, "hucum katkisi; gelişim gerekli", "Final pas ve şut seçimi çalışılmalı", seedDate.AddDays(27)),
            DemoReport(2028, 1011, "Santrfor ikinci izleme", "Gol vuruşu iyi, pres başlangıçlarını doğru yaptı. Kondisyon son dakikalarda düştü.", 82, 81, 74, 76, 86, "Izlenmeli", "Hücum ve bitiricilik tehdidi", "Fiziksel dayanıklılık", 7, "hucum katkisi; gelişim gerekli", "Maç sonu fiziksel dayanıklılık çalışmaları önerilir", seedDate.AddDays(28)),
            DemoReport(2029, 1016, "Merkez oyuncu tempo testi", "Pas kalitesi yüksek, oyun görüşü sayesinde yön değiştirdi. Fiziksel düşüş sonrası top kaybı yaptı.", 84, 71, 81, 74, 86, "Izlenmeli", "Pas kalitesi ve oyun görüşü", "Fiziksel dayanıklılık; Karar verme", 6, "teknik oyuncu; gelişim gerekli", "Tempo altında pas güvenliği ve dayanıklılık çalışılmalı", seedDate.AddDays(29)),
            DemoReport(2030, 1017, "On numara şut-pas dengesi", "Asist ve şut tehdidi üretti, gol pozisyonu hazırladı. Riskli tercih sayısı azaltılmalı.", 86, 73, 80, 75, 90, "Transfer Onerisi", "Pas kalitesi; Hücum ve bitiricilik tehdidi", "Karar verme", 8, "teknik oyuncu; hucum katkisi; gelişim gerekli", "Son bölgede doğru tercih ve tempo kontrolü çalışılmalı", seedDate.AddDays(30)),
            DemoReport(2031, 1018, "Kanat hız tekrar raporu", "Sprint mesafeleri etkili, hızlı çıkışlarla savunma arkasına koştu. Pas kalitesi orta seviyede.", 76, 86, 70, 70, 84, "Izlenmeli", "Hız ve sprint aksiyonları", "Pas kalitesi", 7, "hizli oyuncu; takip edilmeli", "Final pas kalitesi ve orta çalışmaları önerilir", seedDate.AddDays(31)),
            DemoReport(2032, 1002, "Stoper ikinci savunma raporu", "Markaj temasları güçlüydü fakat savunma dönüşü sırasında pozisyon hatası tekrarlandı.", 67, 83, 71, 69, 77, "Gelistirilmeli", "İkili mücadele gücü", "Savunma farkındalığı", 5, "gelişim gerekli", "Savunma pozisyon alma tekrarları ve çizgi takibi çalışılmalı", seedDate.AddDays(32)),
            DemoReport(2033, 1006, "Ön libero karar kalitesi raporu", "Pas temposu iyi, top kaybı az ve karar verme dengeliydi. Oyun görüşü takımı rahatlattı.", 80, 80, 83, 79, 85, "Izlenmeli", "Pas kalitesi ve oyun görüşü", "Belirgin kritik gelişim alanı yok", 8, "teknik oyuncu; takip edilmeli", "Dikine pas cesareti kontrollü artırılmalı", seedDate.AddDays(33)),
            DemoReport(2034, 1004, "Sol bek kondisyon raporu", "Hızlı bindirmeler yaptı ancak ikinci yarıda yoruldu ve savunma dönüşü aksadı.", 73, 80, 68, 69, 79, "Gelistirilmeli", "Hız ve sprint aksiyonları", "Fiziksel dayanıklılık; Savunma farkındalığı", 5, "hizli oyuncu; gelişim gerekli", "Dayanıklılık, savunma dönüşü ve pozisyon alma çalışılmalı", seedDate.AddDays(34)),
            DemoReport(2035, 1023, "Merkez liderlik ikinci rapor", "Pas yönü değiştirme, oyun görüşü ve tempo kontrolü çok iyi. Riskli tercih çok az.", 84, 79, 85, 83, 88, "Izlenmeli", "Pas kalitesi ve oyun görüşü; Liderlik", "Belirgin kritik gelişim alanı yok", 9, "teknik oyuncu; takip edilmeli", "Üst seviye maç temposunda liderlik rolü verilmeli", seedDate.AddDays(35)),
            DemoReport(2036, 1020, "Santrfor fiziksel rapor", "Gol tehdidi ve ceza sahası koşuları iyi. Kondisyon ve pres sürekliliği gelişmeli.", 80, 82, 72, 73, 84, "Takip Edilmeli", "Hücum ve bitiricilik tehdidi", "Fiziksel dayanıklılık", 7, "hucum katkisi; gelişim gerekli", "Pres sürekliliği ve dayanıklılık çalışmaları önerilir", seedDate.AddDays(36)),
            DemoReport(2037, 1005, "Sağ bek üçüncü bölge raporu", "Pas bağlantısı ve bindirme zamanlaması iyi. Markaj değişiminde geç kaldı.", 74, 80, 73, 71, 79, "Takip Edilmeli", "Pas bağlantısı", "Savunma farkındalığı", 6, "teknik oyuncu; gelişim gerekli", "Markaj paylaşımı ve savunma pozisyon alma çalışılmalı", seedDate.AddDays(37)),
            DemoReport(2038, 1007, "Merkez orta saha karar raporu", "Pas kalitesi ve oyun görüşü iyi, ancak baskı altında top kaybı ve riskli tercih yaptı.", 81, 73, 79, 70, 83, "Izlenmeli", "Pas kalitesi ve oyun görüşü", "Karar verme", 7, "teknik oyuncu; gelişim gerekli", "Baskı altında karar verme ve top güvenliği çalışılmalı", seedDate.AddDays(38)),
            DemoReport(2039, 1019, "Sol kanat hücum tekrar raporu", "Hızlı sprintler, şut tehdidi ve gol koşusu çok etkiliydi. Savunma dönüşü daha disiplinli olmalı.", 82, 84, 71, 72, 88, "Izlenmeli", "Hız ve sprint aksiyonları; Hücum ve bitiricilik tehdidi", "Savunma farkındalığı", 7, "hizli oyuncu; hucum katkisi; gelişim gerekli", "Savunma dönüşü ve ters kademe çalışmaları önerilir", seedDate.AddDays(39))
        ];
    }

    private static Player DemoPlayer(
        int id,
        string firstName,
        string lastName,
        string position,
        string preferredFoot,
        int height,
        int weight,
        DateOnly birthDate)
    {
        var clubId = ClubIdForPlayer(id);

        return new Player
        {
            Id = id,
            ClubId = clubId,
            TeamId = TeamIdForClub(clubId),
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
            Position = position,
            PreferredFoot = preferredFoot,
            Height = height,
            Weight = weight,
            Nationality = "Turkey",
            PhotoUrl = $"https://placehold.co/320x320?text={firstName[0]}{lastName[0]}",
            Status = "Active"
        };
    }

    private static ScoutReport DemoReport(
        int id,
        int playerId,
        string title,
        string observationText,
        int technicalScore,
        int physicalScore,
        int tacticalScore,
        int mentalScore,
        int potentialScore,
        string recommendation,
        string strengths,
        string weaknesses,
        int suggestedScore,
        string tags,
        string developmentAdvice,
        DateTime createdAt)
    {
        return new ScoutReport
        {
            Id = id,
            PlayerId = playerId,
            ScoutId = ScoutIdForPlayer(playerId),
            Title = title,
            ObservationText = observationText,
            TechnicalScore = technicalScore,
            PhysicalScore = physicalScore,
            TacticalScore = tacticalScore,
            MentalScore = mentalScore,
            PotentialScore = potentialScore,
            OverallScore = (int)Math.Round(
                (technicalScore + physicalScore + tacticalScore + mentalScore + potentialScore) / 5.0,
                MidpointRounding.AwayFromZero),
            Recommendation = recommendation,
            AnalysisSummary = suggestedScore >= 8
                ? "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli."
                : "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.",
            Strengths = strengths,
            Weaknesses = weaknesses,
            SuggestedActions = developmentAdvice,
            SuggestedScore = suggestedScore,
            Tags = tags,
            DevelopmentAdvice = developmentAdvice,
            CreatedAt = createdAt
        };
    }

    private static int ClubIdForPlayer(int playerId)
    {
        if (playerId is 1 or >= 1001 and <= 1008)
        {
            return 1;
        }

        return playerId is >= 1009 and <= 1016 ? 2 : 3;
    }

    private static int TeamIdForClub(int clubId)
    {
        return clubId;
    }

    private static int ScoutIdForPlayer(int playerId)
    {
        return ClubIdForPlayer(playerId) switch
        {
            1 => 4,
            2 => 5,
            _ => 6
        };
    }
}
