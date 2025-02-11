using System;
using System.Collections.Generic;

namespace CathayInterviewAPI.Models;

/// <summary>
/// 存儲貨幣的多語系名稱（貨幣與語言的關聯）
/// </summary>
public partial class CurrencyTranslation
{
    /// <summary>
    /// 貨幣翻譯的唯一識別碼（主鍵）
    /// </summary>
    public int CurrencyTranslationId { get; set; }

    /// <summary>
    /// 關聯的貨幣 ID（對應 Currency.CurrencyId）
    /// </summary>
    public int CurrencyId { get; set; }

    /// <summary>
    /// 關聯的語言 ID（對應 Language.LanguageId）
    /// </summary>
    public int LanguageId { get; set; }

    /// <summary>
    /// 該語言對應的貨幣名稱
    /// </summary>
    public string CurrencyName { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
