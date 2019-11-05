using DDDCrashCourse.SharedKernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.UserAggregate
{
    public class Location : ValueObject<Location>
    {
        #region Constructors and properties

        public Location(string city, string region, string country)
        {
            City = city;
            Region = region;
            Country = country;
        }

        public Location(string city, string region, string country, double lat, double longitude)
        {
            City = city;
            Region = region;
            Country = country;
            Lat = lat;
            Long = longitude;
        }

        public string City { get; private set; }
        public string Region { get; private set; }
        public string Country { get; private set; }
        public double Lat { get; private set; }
        public double Long { get; private set; }
        #endregion

        #region Overrides

        public override bool Equals(Location other)
        {
            // Two location instances are equal if city, region and country are equal
            // Also to avoid equality failures due to casing, all string ar transformed to lowercase culture invariant strings
            return City.ToLowerInvariant() == other.City.ToLowerInvariant()
                   && Region.ToLowerInvariant() == other.Region.ToLowerInvariant()
                   && Country.ToLowerInvariant() == other.Country.ToLowerInvariant();
        }
        #endregion

        #region Value object methods
        public Location ChangeCity(string city)
        {
            if (string.IsNullOrEmpty(city))
                return this;
            return new Location(city, Region, Country);
        }

        public Location ChangeCountry(string newCountry)
        {
            if (string.IsNullOrEmpty(newCountry))
                return this;
            return new Location(City, Region, newCountry);
        }

        public Location ChangeRegion(string newRegion)
        {
            if (string.IsNullOrEmpty(newRegion))
                return this;
            return new Location(City, newRegion, Country);
        }

        #endregion
    }
}
