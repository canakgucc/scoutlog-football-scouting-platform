using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboardSummaryAndDemoSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BirthDate", "ClubId", "FirstName", "Height", "LastName", "Nationality", "PhotoUrl", "Position", "PreferredFoot", "Status", "TeamId", "Weight" },
                values: new object[,]
                {
                    { 1001, new DateOnly(2007, 2, 3), 1, "Mert", 188, "Aydin", "Turkey", "https://placehold.co/320x320?text=MA", "Goalkeeper", "Right", "Active", 1, 82 },
                    { 1002, new DateOnly(2006, 11, 18), 1, "Kerem", 186, "Arslan", "Turkey", "https://placehold.co/320x320?text=KA", "Centre Back", "Right", "Active", 1, 80 },
                    { 1003, new DateOnly(2007, 6, 7), 1, "Bora", 184, "Demir", "Turkey", "https://placehold.co/320x320?text=BD", "Centre Back", "Left", "Active", 1, 78 },
                    { 1004, new DateOnly(2008, 1, 22), 1, "Efe", 176, "Koc", "Turkey", "https://placehold.co/320x320?text=EK", "Left Back", "Left", "Active", 1, 70 },
                    { 1005, new DateOnly(2007, 9, 14), 1, "Deniz", 177, "Sahin", "Turkey", "https://placehold.co/320x320?text=DS", "Right Back", "Right", "Active", 1, 72 },
                    { 1006, new DateOnly(2006, 8, 9), 1, "Ali", 180, "Kaya", "Turkey", "https://placehold.co/320x320?text=AK", "Defensive Midfielder", "Right", "Active", 1, 74 },
                    { 1007, new DateOnly(2007, 12, 1), 1, "Yigit", 178, "Ozkan", "Turkey", "https://placehold.co/320x320?text=YO", "Central Midfielder", "Right", "Active", 1, 71 },
                    { 1008, new DateOnly(2008, 3, 5), 1, "Can", 174, "Polat", "Turkey", "https://placehold.co/320x320?text=CP", "Attacking Midfielder", "Left", "Active", 1, 67 },
                    { 1009, new DateOnly(2007, 5, 16), 1, "Ozan", 175, "Er", "Turkey", "https://placehold.co/320x320?text=OE", "Winger", "Right", "Active", 1, 68 },
                    { 1010, new DateOnly(2008, 4, 25), 1, "Arda", 173, "Gunes", "Turkey", "https://placehold.co/320x320?text=AG", "Winger", "Left", "Active", 1, 66 },
                    { 1011, new DateOnly(2006, 10, 30), 1, "Baran", 183, "Yilmaz", "Turkey", "https://placehold.co/320x320?text=BY", "Striker", "Right", "Active", 1, 77 },
                    { 1012, new DateOnly(2007, 7, 11), 1, "Kaan", 181, "Celik", "Turkey", "https://placehold.co/320x320?text=KC", "Striker", "Left", "Active", 1, 75 },
                    { 1013, new DateOnly(2006, 12, 20), 1, "Umut", 190, "Sari", "Turkey", "https://placehold.co/320x320?text=US", "Goalkeeper", "Left", "Active", 1, 84 },
                    { 1014, new DateOnly(2007, 1, 13), 1, "Selim", 187, "Kurt", "Turkey", "https://placehold.co/320x320?text=SK", "Centre Back", "Right", "Active", 1, 81 },
                    { 1015, new DateOnly(2007, 8, 2), 1, "Atakan", 182, "Aslan", "Turkey", "https://placehold.co/320x320?text=AA", "Defensive Midfielder", "Right", "Active", 1, 76 },
                    { 1016, new DateOnly(2008, 2, 17), 1, "Tuna", 177, "Acar", "Turkey", "https://placehold.co/320x320?text=TA", "Central Midfielder", "Left", "Active", 1, 69 },
                    { 1017, new DateOnly(2007, 3, 29), 1, "Doruk", 176, "Eren", "Turkey", "https://placehold.co/320x320?text=DE", "Attacking Midfielder", "Right", "Active", 1, 68 },
                    { 1018, new DateOnly(2008, 6, 10), 1, "Berk", 172, "Kaplan", "Turkey", "https://placehold.co/320x320?text=BK", "Winger", "Right", "Active", 1, 65 },
                    { 1019, new DateOnly(2007, 9, 3), 1, "Ruzgar", 174, "Cinar", "Turkey", "https://placehold.co/320x320?text=RC", "Winger", "Left", "Active", 1, 66 },
                    { 1020, new DateOnly(2006, 5, 28), 1, "Eren", 185, "Bulut", "Turkey", "https://placehold.co/320x320?text=EB", "Striker", "Right", "Active", 1, 79 },
                    { 1021, new DateOnly(2007, 4, 19), 1, "Batuhan", 179, "Sen", "Turkey", "https://placehold.co/320x320?text=BS", "Left Back", "Left", "Active", 1, 73 },
                    { 1022, new DateOnly(2008, 8, 8), 1, "Sarp", 178, "Yuce", "Turkey", "https://placehold.co/320x320?text=SY", "Right Back", "Right", "Active", 1, 72 },
                    { 1023, new DateOnly(2006, 9, 24), 1, "Onur", 181, "Tekin", "Turkey", "https://placehold.co/320x320?text=OT", "Central Midfielder", "Right", "Active", 1, 74 },
                    { 1024, new DateOnly(2007, 11, 6), 1, "Furkan", 184, "Aksoy", "Turkey", "https://placehold.co/320x320?text=FA", "Striker", "Right", "Active", 1, 78 }
                });

            migrationBuilder.InsertData(
                table: "ScoutReports",
                columns: new[] { "Id", "AnalysisSummary", "CreatedAt", "DevelopmentAdvice", "MentalScore", "ObservationText", "OverallScore", "PhysicalScore", "PlayerId", "PotentialScore", "Recommendation", "ScoutId", "Strengths", "SuggestedActions", "SuggestedScore", "TacticalScore", "Tags", "TechnicalScore", "Title", "Weaknesses" },
                values: new object[,]
                {
                    { 2001, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", 76, "Kaleci yan toplarda güven verdi, oyun görüşü ve kısa pas başlangıçlarında sakindi. Kondisyon son bölümde korunmalı.", 73, 77, 1001, 74, "Takip Edilmeli", 4, "Refleksler; Pasla oyun kurma", "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", 6, 72, "gelişim gerekli", 68, "Kaleci refleks ve oyun kurulum raporu", "Fiziksel dayanıklılık" },
                    { 2002, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma pozisyon alma ve markaj takip çalışmaları önerilir", 68, "Oyuncu ikili mücadelede güçlüydü ancak markaj takibinde bir pozisyon hatası yaptı. Savunma dönüşü daha erken başlamalı.", 72, 82, 1002, 76, "Gelistirilmeli", 4, "İkili mücadele gücü", "Savunma pozisyon alma ve markaj takip çalışmaları önerilir", 5, 70, "gelişim gerekli", 66, "Stoper markaj raporu", "Savunma farkındalığı" },
                    { 2003, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Pas temposu üst seviye maçlarda test edilmeli", 74, "Sol ayak pas kalitesi iyi, oyun görüşü sayesinde ilk baskıyı kırdı. Riskli tercih sayısı düşük kaldı.", 77, 78, 1003, 82, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü", "Pas temposu üst seviye maçlarda test edilmeli", 8, 77, "teknik oyuncu; takip edilmeli", 76, "Sol stoper pas açısı raporu", "Belirgin kritik gelişim alanı yok" },
                    { 2004, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma dönüşü ve pozisyon alma çalışmaları önerilir", 70, "Sol çizgide hızlı bindirmeler yaptı, sprint temposu etkiliydi. Savunma dönüşlerinde zaman zaman geç kaldı.", 75, 84, 1004, 81, "Izlenmeli", 4, "Hız ve sprint aksiyonları", "Savunma dönüşü ve pozisyon alma çalışmaları önerilir", 7, 69, "hizli oyuncu; gelişim gerekli", 72, "Sol bek sprint raporu", "Savunma farkındalığı" },
                    { 2005, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Baskı altında karar verme ve risksiz pas tercihleri çalışılmalı", 66, "Topla çıkışta pas bağlantısı iyi fakat baskı altında karar verme konusunda riskli tercih yaptı.", 73, 79, 1005, 75, "Takip Edilmeli", 4, "Pas bağlantısı", "Baskı altında karar verme ve risksiz pas tercihleri çalışılmalı", 6, 71, "teknik oyuncu; gelişim gerekli", 73, "Sağ bek karar verme raporu", "Karar verme" },
                    { 2006, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Daha fazla dikine pas denemesiyle gelişim desteklenmeli", 76, "Merkezde top kapma ve pas istasyonu rolünü iyi oynadı. Oyun görüşü gelişmiş, top kaybı sayısı düşük.", 80, 80, 1006, 84, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü", "Daha fazla dikine pas denemesiyle gelişim desteklenmeli", 8, 82, "teknik oyuncu; takip edilmeli", 78, "Ön libero denge raporu", "Belirgin kritik gelişim alanı yok" },
                    { 2007, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Fiziksel dayanıklılık ve maç sonu tempo koruma çalışmaları önerilir", 75, "Oyuncu pas ritmini iyi ayarladı, asist öncesi pası değerliydi. Kondisyon olarak son 15 dakikada yoruldu.", 78, 72, 1007, 83, "Izlenmeli", 4, "Pas kalitesi; Asist hazırlığı", "Fiziksel dayanıklılık ve maç sonu tempo koruma çalışmaları önerilir", 7, 78, "teknik oyuncu; gelişim gerekli", 80, "Merkez orta saha tempo raporu", "Fiziksel dayanıklılık" },
                    { 2008, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Final kararları ve şut-pas tercihi çalışılmalı", 76, "Oyun görüşü üst düzey, dar alanda pas ve asist tehdidi yarattı. Şut tercihleri daha dengeli olmalı.", 79, 70, 1008, 88, "Transfer Onerisi", 4, "Pas kalitesi ve oyun görüşü; Hücum üretkenliği", "Final kararları ve şut-pas tercihi çalışılmalı", 9, 79, "teknik oyuncu; hucum katkisi; takip edilmeli", 84, "On numara yaratıcılık raporu", "Şut tercihi dengesi" },
                    { 2009, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma dönüşü ve ters kanat kapatma çalışmaları önerilir", 72, "Oyuncu çok hızlı, sprint tekrarlarında etkili ve bire birde rakibi eksiltti. Savunma dönüşü geç başladı.", 79, 86, 1009, 87, "Izlenmeli", 4, "Hız ve sprint aksiyonları", "Savunma dönüşü ve ters kanat kapatma çalışmaları önerilir", 7, 70, "hizli oyuncu; gelişim gerekli", 79, "Sağ kanat hız raporu", "Savunma farkındalığı" },
                    { 2010, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Son bölgede karar verme ve pas-şut tercihi çalışılmalı", 70, "Sol kanatta çabuk yön değiştirdi, şut kalitesi ve bitiricilik dikkat çekti. Karar verme bazı hücumlarda gecikti.", 78, 78, 1010, 86, "Izlenmeli", 4, "Hız; Hücum ve bitiricilik tehdidi", "Son bölgede karar verme ve pas-şut tercihi çalışılmalı", 7, 73, "hizli oyuncu; hucum katkisi; gelişim gerekli", 82, "Sol kanat bitiricilik raporu", "Karar verme" },
                    { 2011, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Ön alan presinde süreklilik çalışmaları önerilir", 75, "Ceza sahasında gol kokusu yüksek, bitiricilik ve ilk temas kalitesi iyi. Pres sürekliliği artırılmalı.", 79, 80, 1011, 85, "Izlenmeli", 4, "Hücum ve bitiricilik tehdidi", "Ön alan presinde süreklilik çalışmaları önerilir", 8, 72, "hucum katkisi; takip edilmeli", 81, "Santrfor bitiricilik raporu", "Pres sürekliliği" },
                    { 2012, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Gövde kullanımı ve temas sonrası devam oyunu çalışılmalı", 74, "Sırtı dönük oyunda pas bağlantısı kurdu, asist tehdidi yarattı. Fiziksel temaslarda daha dengeli kalmalı.", 76, 76, 1012, 80, "Takip Edilmeli", 4, "Pas bağlantısı; Asist tehdidi", "Gövde kullanımı ve temas sonrası devam oyunu çalışılmalı", 7, 75, "teknik oyuncu; takip edilmeli", 77, "Santrfor bağlantı oyunu raporu", "Fiziksel temas dengesi" },
                    { 2013, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Baskı altında karar verme ve güvenli oyun başlangıcı çalışılmalı", 69, "Yan toplarda çıkış zamanlaması iyi, uzun pas denemeleri isabetliydi. Riskli tercih sayısı bir pozisyonda arttı.", 74, 81, 1013, 78, "Gelistirilmeli", 4, "Uzun pas kalitesi", "Baskı altında karar verme ve güvenli oyun başlangıcı çalışılmalı", 6, 72, "teknik oyuncu; gelişim gerekli", 70, "Kaleci yan top raporu", "Karar verme" },
                    { 2014, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "İlk pas ve baskı kırma çalışmaları önerilir", 76, "Hava toplarında etkili, markaj temasını iyi kullandı. Topla çıkışta pas kalitesi gelişmeli.", 75, 84, 1014, 79, "Takip Edilmeli", 4, "Hava topu ve markaj gücü", "İlk pas ve baskı kırma çalışmaları önerilir", 6, 74, "gelişim gerekli", 64, "Stoper hava hakimiyeti raporu", "Pas kalitesi" },
                    { 2015, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Tek temas pas temposu çalışmaları önerilir", 77, "Savunma dönüşü disiplinli, ikinci topları topladı. Pas temposu daha hızlı olabilir.", 79, 82, 1015, 82, "Izlenmeli", 4, "Savunma geçişi; İkinci top takibi", "Tek temas pas temposu çalışmaları önerilir", 8, 80, "takip edilmeli", 72, "Ön libero savunma geçişi raporu", "Pas temposu" },
                    { 2016, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", 76, "Oyun görüşü ve pas açısı çok iyi, iki asist hazırladı. Kondisyon olarak son bölümde fiziksel düşüş yaşadı.", 79, 70, 1016, 87, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü", "Fiziksel dayanıklılık ve maç sonu tempo çalışmaları önerilir", 7, 80, "teknik oyuncu; gelişim gerekli", 83, "Merkez orta saha oyun görüşü raporu", "Fiziksel dayanıklılık" },
                    { 2017, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Top kaybı sonrası reaksiyon ve final kararı çalışılmalı", 74, "Dar alanda pas, asist ve şut tehdidi yarattı. Top kaybı sonrası reaksiyonunu hızlandırmalı.", 80, 72, 1017, 89, "Transfer Onerisi", 4, "Pas kalitesi; Hücum ve bitiricilik tehdidi", "Top kaybı sonrası reaksiyon ve final kararı çalışılmalı", 8, 78, "teknik oyuncu; hucum katkisi; gelişim gerekli", 85, "On numara final pas raporu", "Karar verme" },
                    { 2018, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Son aksiyon karar verme ve pas kalitesi çalışılmalı", 68, "Çabuk ilk adımı ve sprint çıkışı iyi. Bire bir sonrası pas tercihi zaman zaman riskli tercih oldu.", 77, 85, 1018, 84, "Izlenmeli", 4, "Hız ve sprint aksiyonları", "Son aksiyon karar verme ve pas kalitesi çalışılmalı", 7, 70, "hizli oyuncu; gelişim gerekli", 78, "Sağ kanat bire bir raporu", "Karar verme" },
                    { 2019, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma pozisyon alma ve markaj takibi çalışılmalı", 70, "İçe kat edip şut tehdidi yarattı, gol pozisyonuna girdi. Savunma dönüşü ve markaj takibi gelişmeli.", 77, 82, 1019, 85, "Izlenmeli", 4, "Hücum ve bitiricilik tehdidi; Hız", "Savunma pozisyon alma ve markaj takibi çalışılmalı", 7, 69, "hizli oyuncu; hucum katkisi; gelişim gerekli", 80, "Sol kanat ters ayak raporu", "Savunma farkındalığı" },
                    { 2020, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Fiziksel dayanıklılık ve pres sürekliliği çalışılmalı", 75, "Ön alan presi ve gol vuruşu güçlüydü. Kondisyon düşüşü nedeniyle son bölümde baskısı azaldı.", 79, 83, 1020, 84, "Takip Edilmeli", 4, "Hücum ve bitiricilik tehdidi; Pres", "Fiziksel dayanıklılık ve pres sürekliliği çalışılmalı", 7, 73, "hucum katkisi; gelişim gerekli", 79, "Santrfor pres raporu", "Fiziksel dayanıklılık" },
                    { 2021, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Pozisyon alma ve çizgi savunması çalışılmalı", 72, "Savunma dönüşü genel olarak iyi ancak bir pozisyon hatası yaptı. Pas çıkışları sade ve doğruydu.", 75, 78, 1021, 77, "Gelistirilmeli", 4, "Sade pas oyunu", "Pozisyon alma ve çizgi savunması çalışılmalı", 6, 76, "gelişim gerekli", 70, "Sol bek savunma raporu", "Savunma farkındalığı" },
                    { 2022, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Kanat orta kalitesi ve final pas çalışmaları önerilir", 73, "Hızlı bindirmeler ve sprint tekrarları iyi. Orta kalitesi gelişirse asist katkısı artar.", 76, 84, 1022, 81, "Izlenmeli", 4, "Hız ve sprint aksiyonları", "Kanat orta kalitesi ve final pas çalışmaları önerilir", 8, 72, "hizli oyuncu; takip edilmeli", 72, "Sağ bek bindirme raporu", "Orta kalitesi" },
                    { 2023, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Üst yaş grubu temposunda test edilmeli", 82, "Pas yönü değiştirme ve oyun görüşü iyi. Karar verme genelde doğru, top kaybı az.", 82, 78, 1023, 86, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü; Liderlik", "Üst yaş grubu temposunda test edilmeli", 8, 84, "teknik oyuncu; takip edilmeli", 82, "Merkez orta saha liderlik raporu", "Belirgin kritik gelişim alanı yok" },
                    { 2024, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Sırtı dönük oyun ve duvar pası çalışılmalı", 73, "Gol vuruşu ve bitiricilik net şekilde öne çıktı. Sırtı dönük pas bağlantısı geliştirilmeli.", 78, 82, 1024, 83, "Takip Edilmeli", 4, "Hücum ve bitiricilik tehdidi", "Sırtı dönük oyun ve duvar pası çalışılmalı", 8, 71, "hucum katkisi; takip edilmeli", 80, "Santrfor gol raporu", "Bağlantı oyunu" },
                    { 2025, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Baskı altında hızlı karar verme çalışmaları önerilir", 76, "Oyun görüşü, asist ve pas kalitesi yine güçlüydü. Karar verme baskı altında daha da hızlanmalı.", 81, 72, 1008, 90, "Transfer Onerisi", 4, "Pas kalitesi ve oyun görüşü; Asist tehdidi", "Baskı altında hızlı karar verme çalışmaları önerilir", 8, 81, "teknik oyuncu; gelişim gerekli", 86, "Yaratıcı orta saha tekrar izleme", "Karar verme" },
                    { 2026, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma dönüşü ve bek yardımı çalışılmalı", 73, "Hızlı ve çabuk driplinglerle çizgiyi kullandı. Savunma dönüşü geç kaldığı anlar var.", 80, 87, 1009, 88, "Izlenmeli", 4, "Hız ve sprint aksiyonları", "Savunma dönüşü ve bek yardımı çalışılmalı", 7, 72, "hizli oyuncu; gelişim gerekli", 80, "Kanat oyuncusu ikinci rapor", "Savunma farkındalığı" },
                    { 2027, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Final pas ve şut seçimi çalışılmalı", 71, "Şut ve gol tehdidi güçlü, pas tercihi bazı anlarda daha doğru olabilirdi.", 79, 79, 1010, 87, "Izlenmeli", 4, "Hücum ve bitiricilik tehdidi", "Final pas ve şut seçimi çalışılmalı", 7, 74, "hucum katkisi; gelişim gerekli", 83, "Sol kanat final aksiyon raporu", "Karar verme" },
                    { 2028, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Maç sonu fiziksel dayanıklılık çalışmaları önerilir", 76, "Gol vuruşu iyi, pres başlangıçlarını doğru yaptı. Kondisyon son dakikalarda düştü.", 80, 81, 1011, 86, "Izlenmeli", 4, "Hücum ve bitiricilik tehdidi", "Maç sonu fiziksel dayanıklılık çalışmaları önerilir", 7, 74, "hucum katkisi; gelişim gerekli", 82, "Santrfor ikinci izleme", "Fiziksel dayanıklılık" },
                    { 2029, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Tempo altında pas güvenliği ve dayanıklılık çalışılmalı", 74, "Pas kalitesi yüksek, oyun görüşü sayesinde yön değiştirdi. Fiziksel düşüş sonrası top kaybı yaptı.", 79, 71, 1016, 86, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü", "Tempo altında pas güvenliği ve dayanıklılık çalışılmalı", 6, 81, "teknik oyuncu; gelişim gerekli", 84, "Merkez oyuncu tempo testi", "Fiziksel dayanıklılık; Karar verme" },
                    { 2030, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Son bölgede doğru tercih ve tempo kontrolü çalışılmalı", 75, "Asist ve şut tehdidi üretti, gol pozisyonu hazırladı. Riskli tercih sayısı azaltılmalı.", 81, 73, 1017, 90, "Transfer Onerisi", 4, "Pas kalitesi; Hücum ve bitiricilik tehdidi", "Son bölgede doğru tercih ve tempo kontrolü çalışılmalı", 8, 80, "teknik oyuncu; hucum katkisi; gelişim gerekli", 86, "On numara şut-pas dengesi", "Karar verme" },
                    { 2031, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Final pas kalitesi ve orta çalışmaları önerilir", 70, "Sprint mesafeleri etkili, hızlı çıkışlarla savunma arkasına koştu. Pas kalitesi orta seviyede.", 77, 86, 1018, 84, "Izlenmeli", 4, "Hız ve sprint aksiyonları", "Final pas kalitesi ve orta çalışmaları önerilir", 7, 70, "hizli oyuncu; takip edilmeli", 76, "Kanat hız tekrar raporu", "Pas kalitesi" },
                    { 2032, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma pozisyon alma tekrarları ve çizgi takibi çalışılmalı", 69, "Markaj temasları güçlüydü fakat savunma dönüşü sırasında pozisyon hatası tekrarlandı.", 73, 83, 1002, 77, "Gelistirilmeli", 4, "İkili mücadele gücü", "Savunma pozisyon alma tekrarları ve çizgi takibi çalışılmalı", 5, 71, "gelişim gerekli", 67, "Stoper ikinci savunma raporu", "Savunma farkındalığı" },
                    { 2033, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Dikine pas cesareti kontrollü artırılmalı", 79, "Pas temposu iyi, top kaybı az ve karar verme dengeliydi. Oyun görüşü takımı rahatlattı.", 81, 80, 1006, 85, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü", "Dikine pas cesareti kontrollü artırılmalı", 8, 83, "teknik oyuncu; takip edilmeli", 80, "Ön libero karar kalitesi raporu", "Belirgin kritik gelişim alanı yok" },
                    { 2034, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Dayanıklılık, savunma dönüşü ve pozisyon alma çalışılmalı", 69, "Hızlı bindirmeler yaptı ancak ikinci yarıda yoruldu ve savunma dönüşü aksadı.", 74, 80, 1004, 79, "Gelistirilmeli", 4, "Hız ve sprint aksiyonları", "Dayanıklılık, savunma dönüşü ve pozisyon alma çalışılmalı", 5, 68, "hizli oyuncu; gelişim gerekli", 73, "Sol bek kondisyon raporu", "Fiziksel dayanıklılık; Savunma farkındalığı" },
                    { 2035, "Oyuncu yüksek potansiyel sinyali veriyor ve yakından takip edilmeli.", new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Üst seviye maç temposunda liderlik rolü verilmeli", 83, "Pas yönü değiştirme, oyun görüşü ve tempo kontrolü çok iyi. Riskli tercih çok az.", 84, 79, 1023, 88, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü; Liderlik", "Üst seviye maç temposunda liderlik rolü verilmeli", 9, 85, "teknik oyuncu; takip edilmeli", 84, "Merkez liderlik ikinci rapor", "Belirgin kritik gelişim alanı yok" },
                    { 2036, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Pres sürekliliği ve dayanıklılık çalışmaları önerilir", 73, "Gol tehdidi ve ceza sahası koşuları iyi. Kondisyon ve pres sürekliliği gelişmeli.", 78, 82, 1020, 84, "Takip Edilmeli", 4, "Hücum ve bitiricilik tehdidi", "Pres sürekliliği ve dayanıklılık çalışmaları önerilir", 7, 72, "hucum katkisi; gelişim gerekli", 80, "Santrfor fiziksel rapor", "Fiziksel dayanıklılık" },
                    { 2037, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Markaj paylaşımı ve savunma pozisyon alma çalışılmalı", 71, "Pas bağlantısı ve bindirme zamanlaması iyi. Markaj değişiminde geç kaldı.", 75, 80, 1005, 79, "Takip Edilmeli", 4, "Pas bağlantısı", "Markaj paylaşımı ve savunma pozisyon alma çalışılmalı", 6, 73, "teknik oyuncu; gelişim gerekli", 74, "Sağ bek üçüncü bölge raporu", "Savunma farkındalığı" },
                    { 2038, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Baskı altında karar verme ve top güvenliği çalışılmalı", 70, "Pas kalitesi ve oyun görüşü iyi, ancak baskı altında top kaybı ve riskli tercih yaptı.", 77, 73, 1007, 83, "Izlenmeli", 4, "Pas kalitesi ve oyun görüşü", "Baskı altında karar verme ve top güvenliği çalışılmalı", 7, 79, "teknik oyuncu; gelişim gerekli", 81, "Merkez orta saha karar raporu", "Karar verme" },
                    { 2039, "Oyuncu olumlu sinyaller veriyor ancak belirli gelişim alanları takip edilmeli.", new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Savunma dönüşü ve ters kademe çalışmaları önerilir", 72, "Hızlı sprintler, şut tehdidi ve gol koşusu çok etkiliydi. Savunma dönüşü daha disiplinli olmalı.", 79, 84, 1019, 88, "Izlenmeli", 4, "Hız ve sprint aksiyonları; Hücum ve bitiricilik tehdidi", "Savunma dönüşü ve ters kademe çalışmaları önerilir", 7, 71, "hizli oyuncu; hucum katkisi; gelişim gerekli", 82, "Sol kanat hücum tekrar raporu", "Savunma farkındalığı" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2003);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2004);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2009);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2015);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2016);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2017);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2018);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2019);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2020);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2022);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2023);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2025);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2026);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2027);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2028);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2029);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2030);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2031);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2032);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2033);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2034);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2035);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2036);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2037);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2038);

            migrationBuilder.DeleteData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2039);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1024);
        }
    }
}
