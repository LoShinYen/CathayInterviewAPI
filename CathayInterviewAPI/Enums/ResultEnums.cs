namespace CathayInterviewAPI.Enums
{
    public static class ResultEnums
    {
        public readonly static ResultBase Success = new ResultBase(0, "Success", "成功");

        #region 100 ~ 199
        public readonly static ResultBase CurrencyIsEmpty = new ResultBase(100, "Currency cannot be null", "貨幣不可為空值");
        public readonly static ResultBase NotFindCurrecy = new ResultBase(100, "Currency not find", "查無此貨幣");
        #endregion

        #region 600~699
        public readonly static ResultBase NotFindDocument = new ResultBase(600, "Not Find Document", "查詢不到文件");

        #endregion

        public readonly static ResultBase ElseError = new ResultBase(999, "Other errors.", "其他錯誤");
    }
}
