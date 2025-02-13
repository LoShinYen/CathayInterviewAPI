using System;
using System.Collections.Generic;

namespace CathayInterviewAPI.Models.Context;

/// <summary>
/// 存儲系統支援的語言代碼及名稱
/// </summary>
public partial class Language
{
    /// <summary>
    /// 語言唯一識別碼（主鍵）
    /// </summary>
    public int LanguageId { get; set; }

    /// <summary>
    /// 語言代碼（例如 en, zh-TW, ja），符合 ISO 639 標準
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// 語言的可讀名稱（例如 English、繁體中文、日本語）
    /// </summary>
    public string DisplayName { get; set; } = null!;

    public virtual ICollection<CurrencyTranslation> CurrencyTranslations { get; set; } = new List<CurrencyTranslation>();
}
