using DDDCrashCourse.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.SharedKernel.Types
{
    public class Name : ValueObject<Name>
    {
        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Initials = FirstName[0].ToString().ToUpperInvariant() + LastName[0].ToString().ToUpperInvariant();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Initials { get; private set; }

        #region Method overrides
        public override bool Equals(Name other)
        {
            return FirstName.ToUpper() == other.FirstName.ToUpper() && LastName.ToUpper() == other.LastName.ToUpper();
        }
        #endregion

        #region Factories

        /// <summary>
        ///     Creates a new <see cref="Name"/> instance
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>A new <see cref="Name"/> instance</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when either firstName or lastName are null</exception>
        /// <exception cref="System.ArgumentException">Thrown when either firstName or lastName are empty strings, contain digits, have a length shorter than 1 or contain non-letter characters</exception>
        public static Name Create(string firstName, string lastName)
        {
            ValidateNameComponents(firstName, lastName);
            return new Name(firstName, lastName);
        }

        /// <summary>
        ///     Creates a new <see cref="Name"/> instance
        /// </summary>
        /// <param name="firstName">The desired first name</param>
        /// <param name="lastName">The desired last name</param>
        /// <param name="name">Out param to send the new Name instance to</param>
        /// <returns>True if a new name instace can be created. False if not</returns>
        public static bool TryCreate(string firstName, string lastName, out Name name)
        {
            try
            {
                ValidateNameComponents(firstName, lastName);
                name = new Name(firstName, lastName);
                return true;
            }
            catch (Exception)
            {
                name = null;
                return false;
            }
        }
        #endregion

        #region Public methods

        /// <summary>
        ///     Changes the first name of a <see cref="Name"/> value object
        /// </summary>
        /// <param name="firstName">The updated first name</param>
        /// <returns>
        ///     A new <see cref="Name"/> instance containing the updated first name, if the provided first name passed validation
        ///     The same, initial <see cref="Name"/> instance if validation has failed
        /// </returns>
        public Name ChangeFirstName(string firstName)
        {
            try
            {
                ValidateSingleNameComponent(firstName, NameComponent.FirstName);
                return new Name(firstName, LastName);
            }
            catch (Exception)
            {
                return this;
            }
        }

        /// <summary>
        ///     Changes the last name of a <see cref="Name"/> value object
        /// </summary>
        /// <param name="lastName">The updated last name</param>
        /// <returns>
        ///     A new <see cref="Name"/> instance containing the updated last name if the provided value passed validation
        ///     The same, initial <see cref="Name"/> instance if validation has failed
        /// </returns>
        public Name ChangeLastName(string lastName)
        {
            try
            {
                ValidateSingleNameComponent(lastName, NameComponent.LastName);
                return new Name(FirstName, lastName);
            }
            catch (Exception)
            {
                return this;
            }
        }

        /// <summary>
        ///     Changes the entire name
        /// </summary>
        /// <param name="firstName">Updated firstName</param>
        /// <param name="lastName">Updated last name</param>
        /// <returns>
        ///     A new <see cref="Name"/> instance containing the updated first name and last name if validation has passed
        ///     The same, initial <see cref="Name"/> instance if validation has failed
        /// </returns>
        public Name ChangeFullName(string firstName, string lastName)
        {
            try
            {
                ValidateNameComponents(firstName, lastName);
                return new Name(firstName, lastName);
            }
            catch (Exception)
            {
                return this;
            }
        }
        #endregion

        #region Private methods

        // Validates first name and last name
        private static void ValidateNameComponents(string firstName, string lastName)
        {
            if (firstName == null) throw new ArgumentNullException("firstName", "First name cannot be null");
            if (lastName == null) throw new ArgumentNullException("lastName", "Last name cannot be null");
            if (firstName == string.Empty) throw new ArgumentException("First name cannot be empty");
            if (lastName == string.Empty) throw new ArgumentException("Last name cannot be empty");
            if (firstName.Length < 2) throw new ArgumentException("First name must be at least two characters long");
            if (lastName.Length < 2) throw new ArgumentException("Last name must be at least two characters long");
            if (ContainsDigit(firstName)) throw new ArgumentException("First name cannot contain digits");
            if (ContainsDigit(firstName)) throw new ArgumentException("Last name cannot contain digits");
            if (!ContainsOnlyLetters(firstName)) throw new ArgumentException("First name can contain only letters");
            if (!ContainsOnlyLetters(lastName)) throw new ArgumentException("Last name can contain only letters");
        }

        // Validates only one of the Name properties
        private static void ValidateSingleNameComponent(string nameComponent, NameComponent componentType)
        {
            if (nameComponent == null) throw new ArgumentNullException(componentType.ToString(), "First name cannot be null");
            if (nameComponent == string.Empty) throw new ArgumentException($"{componentType.ToString()} cannot be empty");
            if (nameComponent.Length < 2) throw new ArgumentException($"{componentType.ToString()} must be at least two characters long");
            if (ContainsDigit(nameComponent)) throw new ArgumentException($"{componentType.ToString()} cannot contain digits");
            if (!ContainsOnlyLetters(nameComponent)) throw new ArgumentException($"{componentType.ToString()} can contain only letters");
        }
        private static bool ContainsDigit(string nameComponent)
        {
            foreach (var c in nameComponent.ToCharArray())
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsOnlyLetters(string source)
        {
            foreach (var c in source)
            {
                if (!char.IsLetter(c) && !char.IsSeparator(c) && !char.IsPunctuation(c)) return false;
            }

            return true;
        }
        #endregion
    }
}
