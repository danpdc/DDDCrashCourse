using DDDCrashCourse.SharedKernel.Types;
using DDDCrashCourse.Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.UserAggregate
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        #region Constructors0
        public User(Guid id) : base(id)
        {
        }
        #endregion

        #region Properties

        public GeneralUserInfo UserInfo { get; protected set; }

        public UserSettings GeneralSettings { get; protected set; }

        public List<Guid> Friends { get; protected set; }

        public List<Guid> PendingFriendRequests { get; protected set; }

        public int NumberOfFriends => Friends.Count;
        #endregion

        #region Public methods
        /// <summary>
        ///     Sends a connection request to the current user
        /// </summary>
        /// <param name="userId">The GUID of the user who requested a connection</param>
        public void SendFriendRequestToUser(Guid userId)
        {
            PendingFriendRequests.Add(userId);
        }

        public void AcceptFriendRequest(Guid userId)
        {
            var user = PendingFriendRequests.Find(u => u == userId);
            PendingFriendRequests.Remove(user);
            Friends.Add(user);
        }

        public void RejectFriendRequest(Guid userId)
        {
            var user = PendingFriendRequests.Find(u => u == userId);
            PendingFriendRequests.Remove(user);
        }

        public void ChangeUserInfo(GeneralUserInfo userInfo)
        {
            UserInfo = userInfo;
        }

        public void ChangeAccountSettings(UserSettings settings)
        {
            GeneralSettings = settings;
        }
        #endregion
    }
}
