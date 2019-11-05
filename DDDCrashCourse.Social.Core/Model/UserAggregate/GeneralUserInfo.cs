using DDDCrashCourse.SharedKernel.Extensions;
using DDDCrashCourse.SharedKernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.UserAggregate
{
    public class GeneralUserInfo : ValueObject<GeneralUserInfo>
    {
        #region Constructors
        private GeneralUserInfo(Name name, string photoUrl, string email, string about = null,
            Location location = null, List<string> interests = null)
        {
            Name = name;
            PhotoUrl = photoUrl;
            Email = email;
            About = about;
            Location = location;
            if (interests == null) Interests = new List<string>();
            else Interests = interests;
        }
        #endregion

        #region Public properties
        public Name Name { get; internal set; }
        public string PhotoUrl { get; internal set; }
        public string Email { get; internal set; }
        public string About { get; internal set; }
        public Location Location { get; internal set; }
        public List<string> Interests { get; internal set; }
        #endregion

        #region Factories
        public static GeneralUserInfo Create(Name name, string photoUrl, string email, string about = null,
            Location location = null, List<string> interests = null)
        {
            var isUri = photoUrl.IsUri();
            if (!isUri) throw new ArgumentException("The provided photo URL is not a URI format");
            var isEmailValid = email.IsValidEmail();
            if (!isEmailValid) throw new ArgumentException("The provided email string is not a valid email format");

            return new GeneralUserInfo(name, photoUrl, email, about, location, interests);
        }

        public static bool TryCreate(Name name, string photoUrl, string email, out GeneralUserInfo userInfo, string about = null,
            Location location = null, List<string> interests = null)
        {
            try
            {
                var isUri = photoUrl.IsUri();
                if (!isUri) throw new ArgumentException("The provided photo URL is not a URI format");

                var isEmailValid = email.IsValidEmail();
                if (!isEmailValid) throw new ArgumentException("The provided email string is not a valid email format");

                userInfo = new GeneralUserInfo(name, photoUrl, email, about, location, interests);
                return true;
            }
            catch (Exception)
            {
                userInfo = null;
                return false;
            }
        }
        #endregion

        #region Public methods
        public GeneralUserInfo ChangeName(Name name)
        {
            return new GeneralUserInfo(name, PhotoUrl, Email, About, Location, Interests);
        }

        public GeneralUserInfo ChangePhotoUrl(string photoUrl)
        {
            if (photoUrl.IsUri()) return Create(Name, photoUrl, Email, About, Location, Interests);

            return this;
        }

        public GeneralUserInfo ChangeEmail(string email)
        {
            if (email.IsValidEmail()) return Create(Name, PhotoUrl, email, About, Location, Interests);

            return this;
        }

        public GeneralUserInfo ChangeAbout(string about)
        {
            return Create(Name, PhotoUrl, Email, about, Location, Interests);
        }

        public GeneralUserInfo ChangeLocation(Location location)
        {
            return Create(Name, PhotoUrl, Email, About, location, Interests);
        }

        public bool TryAddInterest(string interest)
        {
            if (interest == null || interest == string.Empty)
                return false;

            Interests.Add(interest);
            return true;
        }

        public bool TryRemoveInterest(string interest)
        {
            var searchResult = Interests.Find(i => i.ToLowerInvariant().Equals(interest.ToLowerInvariant()));
            if (searchResult == null) return false;

            Interests.Remove(searchResult);
            return true;
        }

        public void ClearInterests()
        {
            Interests.Clear();
        }
        #endregion
    }
}
