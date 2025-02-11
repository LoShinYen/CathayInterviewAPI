using Microsoft.EntityFrameworkCore;

namespace CathayInterviewAPI.Models;

public partial class CathayInterviewDBContext : DbContext
{
    public CathayInterviewDBContext()
    {
    }

    public CathayInterviewDBContext(DbContextOptions<CathayInterviewDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CurrencyTranslation> CurrencyTranslations { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("PK__Currency__14470AF07C7A191B");

            entity.ToTable("Currency", tb => tb.HasComment("存儲貨幣的基本資訊，如貨幣代碼"));

            entity.HasIndex(e => e.CurrencyCode, "UQ__Currency__408426BFBD0B7F51").IsUnique();

            entity.Property(e => e.CurrencyId).HasComment("貨幣唯一識別碼（主鍵）");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasComment("貨幣創建時間，預設為當前時間");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasComment("貨幣代碼（例如 USD, EUR, JPY），符合 ISO 4217 標準");
        });

        modelBuilder.Entity<CurrencyTranslation>(entity =>
        {
            entity.HasKey(e => e.CurrencyTranslationId).HasName("PK__Currency__7027FB895F82A00B");

            entity.ToTable("CurrencyTranslation", tb => tb.HasComment("存儲貨幣的多語系名稱（貨幣與語言的關聯）"));

            entity.HasIndex(e => new { e.CurrencyId, e.LanguageId }, "UQ__Currency__BFD48FAB5FACFF09").IsUnique();

            entity.Property(e => e.CurrencyTranslationId).HasComment("貨幣翻譯的唯一識別碼（主鍵）");
            entity.Property(e => e.CurrencyId).HasComment("關聯的貨幣 ID（對應 Currency.CurrencyId）");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(100)
                .HasComment("該語言對應的貨幣名稱");
            entity.Property(e => e.LanguageId).HasComment("關聯的語言 ID（對應 Language.LanguageId）");

            entity.HasOne(d => d.Currency).WithMany(p => p.CurrencyTranslations)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_CurrencyTranslation_Currency");

            entity.HasOne(d => d.Language).WithMany(p => p.CurrencyTranslations)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("FK_CurrencyTranslation_Language");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PK__Language__B93855AB92E5AA54");

            entity.ToTable("Language", tb => tb.HasComment("存儲系統支援的語言代碼及名稱"));

            entity.HasIndex(e => e.Code, "UQ__Language__A25C5AA75549D32B").IsUnique();

            entity.Property(e => e.LanguageId).HasComment("語言唯一識別碼（主鍵）");
            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .HasComment("語言代碼（例如 en, zh-TW, ja），符合 ISO 639 標準");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasComment("語言的可讀名稱（例如 English、繁體中文、日本語）");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
