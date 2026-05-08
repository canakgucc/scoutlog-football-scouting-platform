using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTrabzonsporDemoClubSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "LogoUrl", "Name" },
                values: new object[] { 4, "Trabzon", "Turkey", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/160x160?text=TS", "Trabzonspor Kulübü" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "AgeGroup", "ClubId", "CoachId", "Name", "Season" },
                values: new object[] { 4, "U19", 4, null, "Trabzonspor U19 Elite", "2025/26" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BirthDate", "ClubId", "FirstName", "Height", "LastName", "Nationality", "PhotoUrl", "PipelineStatus", "Position", "PreferredFoot", "Status", "TeamId", "Weight" },
                values: new object[,]
                {
                    { 1030, new DateOnly(2007, 3, 18), 4, "Uğur", 189, "Yaman", "Turkey", "https://placehold.co/320x320?text=UY", "Recommended", "Goalkeeper", "Right", "Active", 4, 83 },
                    { 1031, new DateOnly(2006, 12, 4), 4, "Ege", 186, "Karadeniz", "Turkey", "https://placehold.co/320x320?text=EK", "Rejected", "Centre Back", "Right", "Active", 4, 80 },
                    { 1032, new DateOnly(2007, 7, 22), 4, "Miraç", 177, "Aksu", "Turkey", "https://placehold.co/320x320?text=MA", "New", "Left Back", "Left", "Active", 4, 71 },
                    { 1033, new DateOnly(2007, 5, 9), 4, "Kuzey", 181, "Demirci", "Turkey", "https://placehold.co/320x320?text=KD", "Under Observation", "Defensive Midfielder", "Right", "Active", 4, 75 },
                    { 1034, new DateOnly(2008, 1, 16), 4, "Aras", 178, "Yıldırım", "Turkey", "https://placehold.co/320x320?text=AY", "Follow-up Needed", "Central Midfielder", "Left", "Active", 4, 70 },
                    { 1035, new DateOnly(2007, 9, 27), 4, "Berkay", 174, "Uzun", "Turkey", "https://placehold.co/320x320?text=BU", "Shortlisted", "Winger", "Right", "Active", 4, 67 },
                    { 1036, new DateOnly(2008, 4, 11), 4, "Eren", 176, "Kara", "Turkey", "https://placehold.co/320x320?text=EK", "Recommended", "Attacking Midfielder", "Left", "Active", 4, 68 },
                    { 1037, new DateOnly(2006, 10, 30), 4, "Taha", 184, "Bayrak", "Turkey", "https://placehold.co/320x320?text=TB", "Rejected", "Striker", "Right", "Active", 4, 78 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClubId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "RoleId", "TeamId" },
                values: new object[] { 7, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "scout@trabzonspor.local", "Trabzonspor Scout", true, "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16", 4, 4 });

            migrationBuilder.InsertData(
                table: "ScoutReports",
                columns: new[] { "Id", "AnalysisSummary", "Competition", "CreatedAt", "DevelopmentAdvice", "EventDate", "MentalScore", "MinutesPlayed", "ObservationText", "ObservedPosition", "Opponent", "OverallScore", "PhysicalScore", "PlayerId", "PotentialScore", "Recommendation", "ReportType", "ScoutId", "Strengths", "SuggestedActions", "SuggestedScore", "TacticalScore", "Tags", "TechnicalScore", "Title", "Weaknesses" },
                values: new object[,]
                {
                    { 2040, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", "Academy Training", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Uzun pas tekrarları ve baskı altında ilk kontrol çalışmaları önerilir", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 78, null, "Kaleci refleks çalışmasında çabuk reaksiyon verdi, kısa pas kalitesi ve oyun görüşüyle baskı altında sakin kaldı. Uzun pas mesafesinde istikrar artırılmalı.", "Goalkeeper", null, 77, 80, 1030, 82, "Takip Edilmeli", "Training", 7, "Refleksler; Pasla oyun kurma", "Uzun pas tekrarları ve baskı altında ilk kontrol çalışmaları önerilir", 7, 74, "teknik oyuncu; takip edilmeli", 72, "Kaleci oyun başlangıcı idman raporu", "Uzun pas istikrarı" },
                    { 2041, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", "Academy Friendly", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "İlk pas kalitesi ve baskı kırma çalışmaları önerilir", new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 76, 80, "Oyuncu hava toplarında güçlüydü, markaj temasını doğru kullandı ve savunma dönüşlerinde disiplinli kaldı. Topla çıkışta pas temposu gelişmeli.", "Centre Back", "Rizespor U19", 77, 84, 1031, 81, "Izlenmeli", "Match", 7, "Hava topu ve markaj gücü", "İlk pas kalitesi ve baskı kırma çalışmaları önerilir", 7, 78, "takip edilmeli", 68, "Stoper hava hakimiyeti maç raporu", "Pas temposu" },
                    { 2042, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", "Academy Friendly", new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma pozisyon alma ve çizgi kapatma çalışmaları önerilir", new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), 73, 81, "Sol çizgide hızlı bindirmeler yaptı, sprint tekrarlarında rakibini zorladı. Savunma dönüşü iyi başladı ancak pozisyon alma bir pozisyonda gecikti.", "Left Back", "Rizespor U19", 77, 83, 1032, 82, "Izlenmeli", "Match", 7, "Hız ve sprint aksiyonları", "Savunma pozisyon alma ve çizgi kapatma çalışmaları önerilir", 7, 72, "hizli oyuncu; gelişim gerekli", 74, "Sol bek çizgi katkısı raporu", "Savunma farkındalığı" },
                    { 2043, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", "U19 Elite League", new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Dikine pas ve geçiş hücumu başlangıçları çalışılmalı", new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), 78, 82, "Merkezde ikinci topları topladı, pas bağlantısı sade ve güvenliydi. Oyun görüşü iyi ancak dikine pas denemesi artırılmalı.", "Defensive Midfielder", "Rizespor U19", 80, 81, 1033, 84, "Izlenmeli", "Match", 7, "Pas kalitesi ve oyun görüşü; İkinci top takibi", "Dikine pas ve geçiş hücumu başlangıçları çalışılmalı", 8, 83, "teknik oyuncu; takip edilmeli", 76, "Ön libero denge ve ikinci top raporu", "Dikine pas cesareti" },
                    { 2044, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", "Academy Friendly", new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Temas sonrası denge ve tempo koruma çalışmaları önerilir", new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), 77, 83, "Pas kalitesi ve oyun görüşüyle takımın temposunu yönetti. Dar alanda sakin kaldı fakat fiziksel temaslarda daha güçlü kalmalı.", "Central Midfielder", "Rizespor U19", 80, 74, 1034, 86, "Izlenmeli", "Match", 7, "Pas kalitesi ve oyun görüşü", "Temas sonrası denge ve tempo koruma çalışmaları önerilir", 8, 80, "teknik oyuncu; takip edilmeli", 82, "Merkez orta saha tempo raporu", "Fiziksel temas dengesi" },
                    { 2045, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", "Academy Training", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma dönüşü ve final pas kalitesi çalışmaları önerilir", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 73, null, "Oyuncu sağ kanatta çok hızlı ve çabuktu. Sprint tekrarlarında rakibini geçti, pas kalitesi fena değildi. Savunma dönüşlerinde zaman zaman geç kaldı.", "Winger", null, 80, 88, 1035, 88, "Izlenmeli", "Training", 7, "Hız ve sprint aksiyonları", "Savunma dönüşü ve final pas kalitesi çalışmaları önerilir", 8, 72, "hizli oyuncu; gelişim gerekli", 79, "Kanat sprint idman raporu", "Savunma farkındalığı" },
                    { 2046, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", "U19 Elite League", new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Top kaybı sonrası pres reaksiyonu ve final kararları çalışılmalı", new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), 76, 85, "Oyuncu merkezde oyun görüşü, pas kalitesi ve asist tehdidiyle öne çıktı. Şut tercihlerinde dengeli kaldı, top kaybı sonrası reaksiyon geliştirilmeli.", "Attacking Midfielder", "Rizespor U19", 81, 73, 1036, 89, "Transfer Onerisi", "Match", 7, "Pas kalitesi ve oyun görüşü; Asist tehdidi", "Top kaybı sonrası pres reaksiyonu ve final kararları çalışılmalı", 8, 80, "teknik oyuncu; hucum katkisi; takip edilmeli", 85, "On numara yaratıcılık raporu", "Top kaybı sonrası reaksiyon" },
                    { 2047, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", "Academy Friendly", new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Sırtı dönük bağlantı oyunu ve tek temas pas çalışmaları önerilir", new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), 78, 86, "Oyuncu santrfor pozisyonunda gol tehdidi ve bitiricilik kalitesiyle dikkat çekti. Ceza sahası koşuları doğruydu ve fiziksel olarak güçlü kaldı.", "Striker", "Rizespor U19", 81, 84, 1037, 87, "Izlenmeli", "Match", 7, "Hücum ve bitiricilik tehdidi", "Sırtı dönük bağlantı oyunu ve tek temas pas çalışmaları önerilir", 8, 74, "hucum katkisi; takip edilmeli", 82, "Santrfor bitiricilik raporu", "Bağlantı oyunu" },
                    { 2048, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", "Academy Friendly", new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Son bölgede karar verme ve pas-şut tercihi çalışılmalı", new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), 72, 87, "Hızlı çıkışlar ve sprint mesafesi yine yüksek kaldı. Bir asist öncesi doğru koşu yaptı ancak karar verme anlarında riskli tercih yaptı.", "Winger", "Rizespor U19", 80, 87, 1035, 89, "Izlenmeli", "Match", 7, "Hız ve sprint aksiyonları; Asist tehdidi", "Son bölgede karar verme ve pas-şut tercihi çalışılmalı", 8, 73, "hizli oyuncu; gelişim gerekli", 81, "Kanat final aksiyon ikinci izleme", "Karar verme" },
                    { 2049, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", "U19 Elite League", new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), 76, 88, "Oyuncu pas yönü değiştirme ve oyun görüşüyle baskıyı kırdı. Kondisyon son bölümde korunmalı, fiziksel düşüş pas temposunu etkiledi.", "Central Midfielder", "Rizespor U19", 80, 72, 1034, 87, "Izlenmeli", "Match", 7, "Pas kalitesi ve oyun görüşü", "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", 7, 81, "teknik oyuncu; gelişim gerekli", 84, "Merkez oyun kurucu tekrar raporu", "Fiziksel dayanıklılık" },
                    { 2050, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", "Academy Training", new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Uzun pas ve oyun yönü değiştirme çalışmaları önerilir", new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), 77, null, "Savunma dönüşü ve markaj paylaşımı idmanda güçlüydü. Topla çıkışta sade oynadı, uzun pas kalitesi gelişmeye açık.", "Centre Back", null, 78, 84, 1031, 82, "Takip Edilmeli", "Training", 7, "Savunma geçişi; Markaj gücü", "Uzun pas ve oyun yönü değiştirme çalışmaları önerilir", 7, 80, "takip edilmeli", 69, "Stoper savunma geçiş idmanı", "Uzun pas kalitesi" },
                    { 2051, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", "Academy Friendly", new DateTime(2026, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Pres sürekliliği ve maç sonu dayanıklılık çalışmaları önerilir", new DateTime(2026, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), 77, 90, "Gol vuruşu etkiliydi, ceza sahası koşularını doğru zamanladı ve pres başlangıçlarında istekliydi. Kondisyon son dakikalarda biraz düştü.", "Striker", "Rizespor U19", 81, 83, 1037, 88, "Izlenmeli", "Match", 7, "Hücum ve bitiricilik tehdidi; Pres", "Pres sürekliliği ve maç sonu dayanıklılık çalışmaları önerilir", 8, 75, "hucum katkisi; gelişim gerekli", 83, "Santrfor pres ve koşu raporu", "Fiziksel dayanıklılık" }
                });

            migrationBuilder.InsertData(
                table: "WatchlistItems",
                columns: new[] { "Id", "ClubId", "CreatedAt", "CreatedByUserId", "PlayerId", "Priority", "Reason", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, 4, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 7, 1035, "High", "Hızlı kanat profili ve tekrar eden sprint kalitesi nedeniyle yakından izlenmeli.", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 4, new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), 7, 1036, "Medium", "Yaratıcı on numara profili, final pas kalitesi için takip edilmeli.", new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 4, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), 7, 1037, "High", "Bitiricilik ve ceza sahası koşuları nedeniyle shortlist adayı.", new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2040);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2041);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2042);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2043);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2044);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2045);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2046);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2047);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2048);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2049);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2050);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2051);

            migrationBuilder.DeleteData(
                table: "WatchlistItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "WatchlistItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "WatchlistItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
