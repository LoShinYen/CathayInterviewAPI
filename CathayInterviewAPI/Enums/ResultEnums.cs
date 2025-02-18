﻿namespace CathayInterviewAPI.Enums
{
    public static class ResultEnums
    {
        public readonly static ResultBase Success = new ResultBase(0, "Success", "成功");

        #region 100 ~ 199
        public readonly static ResultBase CurrencyIsEmpty = new ResultBase(100, "Currency cannot be null", "貨幣不可為空值");
        public readonly static ResultBase NotFindCurrecy = new ResultBase(100, "Currency not find", "查無此貨幣");
        public readonly static ResultBase CurrencyDuplicated = new ResultBase(100, "The currency is duplicated", "該貨幣已重複");
        #endregion

        #region 600~699
        public readonly static ResultBase NotFindDocument = new ResultBase(600, "Not Find Document", "查詢不到文件");
        public readonly static ResultBase CoindeskApiError = new ResultBase(601, "Coindesk API Error", "Coindesk API 失敗");

        #endregion

        public readonly static ResultBase ElseError = new ResultBase(999, "Other errors.", "其他錯誤");
    }
}
