namespace SeleniumConsoleApp.Model
{
    public class ParameterDataModel
    {
        /// <summary>
        /// 多久檢查一次
        /// </summary>
        public int Timer { get; set; }

        /// <summary>
        /// 簽到在甚麼時間開始
        /// </summary>
        public int SignInAt { get; set; }

        /// <summary>
        /// 簽到在甚麼時間之前
        /// </summary>
        public int SignInBefore { get; set; }

        /// <summary>
        /// 簽退在甚麼時間開始
        /// </summary>
        public int SignOutAt { get; set; }

        /// <summary>
        /// 簽退在甚麼時間之前
        /// </summary>
        public int SignOutBefore { get; set; }

        /// <summary>
        /// 每周哪幾天要執行
        /// </summary>
        public int[] DayOfWeek { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 到職日
        /// </summary>
        public string JobDay { get; set; }

        /// <summary>
        /// EMail
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        public string Id { get; set; }
    }
}
