using DDDCrashCourse.SharedKernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.UserAggregate
{
    public class UserSettings : ValueObject<UserSettings>
    {
        #region Constructors
        public UserSettings(bool allowConnection, bool allowMessaging, bool allowMentions, bool allowNotifications)
        {
            AllowConnectionRequests = allowConnection;
            AllowMessaging = allowMessaging;
            AllowMentions = allowMentions;
            AllowNotifications = allowNotifications;
        }
        #endregion

        #region Properties
        public bool AllowConnectionRequests { get; private set; }
        public bool AllowMessaging { get; private set; }
        public bool AllowMentions { get; private set; }
        public bool AllowNotifications { get; private set; }
        #endregion

        #region Overrides
        public override bool Equals(UserSettings other)
        {
            return AllowConnectionRequests == other.AllowConnectionRequests &&
                AllowMessaging == other.AllowMessaging &&
                AllowMentions == other.AllowMentions &&
                AllowNotifications == other.AllowNotifications;
        }
        #endregion

        #region Public methods
        /// <summary>
        ///     Changes the value of AllowConnectionRequests to either true or false
        /// </summary>
        /// <returns>
        ///     New <see cref="UserSettings"/> object containing the toggled setting
        /// </returns>
        public UserSettings ToggleAllowConnectionRequests()
        {
            var newSetting = !AllowConnectionRequests;
            return new UserSettings(newSetting, AllowMessaging, AllowMentions, AllowNotifications);
        }

        /// <summary>
        ///     Changes the value of AllowMessaging to either true or false
        /// </summary>
        /// <returns>A new <see cref="UserSettings"/> object containing the toggled value</returns>
        public UserSettings ToggleAllowMessaging()
        {
            var newSetting = !AllowMessaging;
            return new UserSettings(AllowConnectionRequests, newSetting, AllowMentions, AllowNotifications);
        }

        /// <summary>
        ///     Changes the value of AllowMentions to either true or false
        /// </summary>
        /// <returns>
        ///     A new <see cref="UserSettings"/> object containing the toggled value
        /// </returns>
        public UserSettings ToggleAllowMentions()
        {
            var newSetting = !AllowMentions;
            return new UserSettings(AllowConnectionRequests, AllowMessaging, newSetting, AllowNotifications);
        }

        /// <summary>
        ///     Changes the value of AllowNotifications to eiter true or false
        /// </summary>
        /// <returns>
        ///     A new <see cref="UserSettings"/> object containing the toggled value
        /// </returns>
        public UserSettings ToggleAllowNotifications()
        {
            var newSetting = !AllowNotifications;
            return new UserSettings(AllowConnectionRequests, AllowMessaging, AllowMentions, newSetting);
        }
        #endregion
    }
}
