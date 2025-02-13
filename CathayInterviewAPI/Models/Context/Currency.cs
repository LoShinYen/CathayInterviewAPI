using System;
using System.Collections.Generic;

namespace CathayInterviewAPI.Models.Context;

/// <summary>
/// 存儲貨幣的基本資訊，如貨幣代碼
/// </summary>
public partial class Currency
{
    /// <summary>
    /// 貨幣唯一識別碼（主鍵）
    /// </summary>
    public int CurrencyId { get; set; }

    /// <summary>
    /// 貨幣代碼（例如 USD, EUR, JPY），符合 ISO 4217 標準
    /// </summary>
    public string CurrencyCode { get; set; } = null!;

    /// <summary>
    /// 貨幣創建時間，預設為當前時間
    /// </summary>
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CurrencyTranslation> CurrencyTranslations { get; set; } = new List<CurrencyTranslation>();
}
