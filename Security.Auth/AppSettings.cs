using BaseAPI.Settings;

namespace OnlineAuction.Security.Auth
{
    /// <summary>
    /// Application configurations.
    /// </summary>
    public class AppSettings : BaseSettings
    {
        #region Static
        /// <summary>
        /// Start settings.
        /// </summary>
        private static AppSettings _start;

        /// <summary>
        /// (Get) Start settings.
        /// </summary>
        public static AppSettings Start
        {
            get
            {
                if (_start == null)
                    _start = new AppSettings();
                return _start;
            }
        }
        #endregion
    }
}
