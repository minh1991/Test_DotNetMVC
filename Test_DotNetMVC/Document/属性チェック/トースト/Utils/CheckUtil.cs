using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Test_DotNetMVC.Utils
{
    public class CheckUtil
    {
        // 文字コードを指定
        static Encoding sjisEnc = Encoding.GetEncoding("Shift-JIS");

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CheckUtil(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="_propertyValue">object</param>
        /// <returns>問題ない場合はtrueを返します。</returns>
        #region 必須チェック
        public static bool Chk_Required(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()) == true
                || string.IsNullOrEmpty(_propertyValue?.ToString()) == true)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 長さチェック
        /// </summary>
        /// <param name="_propertyValue">object</param>
        /// <param name="_maxLength">最大文字数</param>
        /// <returns>問題ない場合はtrueを返します。</returns>
        #region 長さチェック
        public static bool Chk_MaxLength(object _propertyValue, int _maxLength)
        {
            if (_propertyValue is string stringValue)
            {
                if (string.IsNullOrEmpty(stringValue) == true)
                {
                    return true;
                }
                if ((stringValue?.Length ?? 0) > _maxLength)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 固定桁数チェック
        /// </summary>
        /// <param name="_propertyValue">object</param>
        /// <param name="_length">固定桁数</param>
        /// <returns>問題ない場合はtrueを返します。</returns>
        #region 固定桁数チェック
        public static bool Chk_FixedLength(object _propertyValue, int _length)
        {
            if (_propertyValue is string stringValue)
            {
                if (string.IsNullOrEmpty(stringValue) == true)
                {
                    return true;
                }
                if ((stringValue?.Trim().Length ?? 0) != _length)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 数値チェック
        /// </summary>
        /// <param name="_propertyValue">object</param>
        /// <param name="prmValue">チェック対象</param>
        /// <param name="acceptNegative">true : 負の数チェック</param>
        /// <param name="prmInt">整数部の最大桁数</param>
        /// <param name="prmDecimal">小数部の最大桁数</param>
        /// <returns>エラーフラグ</returns>
        ///     0：エラー無し（OK）
        ///     1：エラー：　入力値が数字ではない
        ///     2：エラー：　マイナスで入力した
        ///     3：エラー：　桁数超過
        #region 数値チェック
        public static char Chk_NumberFormat(object _propertyValue, string prmValue, int prmInt, int prmDecimal, bool acceptNegative = false)
        {
            //未入力時はチェックを行わない。
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return '0';
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                // 文字列の先頭から末尾までが、英数字のみとマッチするかを調べる。
                if (Regex.IsMatch(chkValue, @"^-?(?=(\d|\.\d))\d*(\.\d*)?$") == false)
                {
                    return '1';
                }
                //文字列の先頭をチェック。
                if (chkValue.StartsWith("0") && chkValue.Length > 1)
                {
                    return '1';
                }
                //負の数チェック
                if (acceptNegative == false && prmValue!.StartsWith("-"))
                {
                    return '2';
                }
                //数値の桁数チェック
                var parts = prmValue!.Replace("-", "").Split('.');  //整数部と小数部に分ける
                if (parts[0].Length > prmInt)
                {
                    return '3';
                }
                if (parts.Length == 2 && parts[1].Length > prmDecimal)
                {
                    return '3';
                }
            }
            return '0';
        }
        #endregion

        /// <summary>
        /// 日時チェック
        /// <param name="_propertyValue">プロパティ</param>
        /// <param name="_formatDateTime">日時フォーマット</param>
        /// <returns>問題ない場合はtrueを返します。</returns>
        /// </summary>
        /// 日時フォーマット
        /// ・yyyy/MM
        /// ・yyyy/MM/dd
        /// ・yyyy/MM/dd H:m:ss
        /// ・H:m:ss
        /// <returns></returns>
        #region 日時チェック
        public static bool Chk_DataTimeFormat(object _propertyValue, string _formatDateTime)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                DateTime result;
                if (!DateTime.TryParseExact(chkValue, _formatDateTime, null, System.Globalization.DateTimeStyles.None, out result))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 開始日と終了日の大小チェック
        /// <param name="_startDate">開始日</param>
        /// <param name="_endDate">終了日</param>
        /// </summary>
        /// <returns>問題ない場合はtrueを返します。</returns>
        #region 開始日と終了日の大小チェック
        public static bool Chk_StartDateAndEndDate(DateTime _startDate, DateTime _endDate)
        {
            if (_endDate >= _startDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 半角チェック
        /// </summary>
        /// <param name="_propertyValue">プロパティ</param>
        /// <returns>問題ない場合はtrueを返します。</returns>
        #region 半角チェック
        public bool Chk_HankakuFormat(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                int byteLenght = sjisEnc.GetByteCount(chkValue);
                if (byteLenght != chkValue.Length)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 全角チェック
        /// </summary>
        /// <param name="_propertyValue">プロパティ</param>
        /// <returns>問題ない場合はtrueを返します。</returns>
        #region 全角チェック
        public bool Chk_ZenkakuFormat(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                int byteLenght = sjisEnc.GetByteCount(chkValue);
                if (byteLenght != chkValue.Length * 2)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 範囲数値チェック
        /// </summary>
        /// <param name="_propertyValue">プロパティ</param>
        /// <param name="_minNum">最小数値</param>
        /// <param name="_maxNum">最大数値</param>
        /// <returns>エラーフラグ</returns>
        ///     0：エラー無し（OK）
        ///     1：エラー：　入力値が数字ではない
        ///     2：エラー：　範囲以外の場合
        #region 範囲数値チェック
        public static char Chk_RangeNumber(object _propertyValue, int _minNum, int _maxNum)
        {
            //未入力時はチェックを行わない。
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return '0';
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                // 文字列の先頭から末尾までが、英数字のみとマッチするかを調べる。
                if (Regex.IsMatch(chkValue, @"^-?(?=(\d|\.\d))\d*(\.\d*)?$") == false)
                {
                    return '1';
                }
                // 範囲数値チェック
                int chkValueInt = int.Parse(chkValue);
                if ((chkValueInt > _maxNum) || (chkValueInt < _minNum))
                {
                    return '2';
                }
            }
            return '0';
        }
        #endregion


#region プロパティ情報取得
        /// <summary>
        /// プロパティの値を取得します。
        /// </summary>
        /// <param name="model">モデル</param>
        /// <param name="propertyName">プロパティ</param>
        /// <returns>プロパティの表示名</returns>
        #region プロパティの値　取得
        public static object GetPropertyValue(object model, string propertyName)
        {
            var propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model, null);

            return propertyValue!;
        }
        #endregion
        /// <summary>
        /// プロパティの表示名を取得します。
        /// </summary>
        /// <param name="model">モデル</param>
        /// <param name="propertyName">プロパティ</param>
        /// <returns>プロパティの表示名</returns>
        #region プロパティの表示名　取得
        public static string GetDisplayName(object model, string propertyName)
        {
            var displayAttribute = model.GetType().GetProperty(propertyName)?.GetCustomAttributes(typeof(DisplayAttribute), true)
                                  .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? propertyName;
        }
        #endregion
#endregion
    }
}
