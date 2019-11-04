using System;
using System.Collections.Generic;
using System.Text;
using DDDCrashCourse.SharedKernel.Exceptions;

namespace DDDCrashCourse.SharedKernel.Types
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        /// <summary>
        ///     Protected constructors. 
        /// </summary>
        /// <param name="id">The identifier type used for a specific entity. E.g. int, long, Guid</param>
        /// <exception cref="InvalidIdentifierException">Exception triggered when the provided parameter quals to the type's default value</exception>
        protected Entity(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new InvalidIdentifierException("The ID cannot be the type's default value.", "id");
            }

            Id = id;
        }

        /// <summary>
        ///     Gets the entitie's identifier
        /// </summary>
        public TId Id { get; private set; }

        #region Implement Equality

        //Override the Equals method on the object class
        //That method returns reference equality. We are interested in value equality. More precise identifier equality
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        //Implementing IEquatable<T>.
        //Two entities are equal only if they are of the same type and if there is identifier equality
        public bool Equals(Entity<TId> other)
        {

            return Id.GetHashCode() == other.Id.GetHashCode();
        }

        //Overriding GetHashCode as per best practice when we override the Equals method on the object class
        public override int GetHashCode()
        {
            //Since Identifiers will have numberic values we don't need to create a custom hashcode
            return Id.GetHashCode();
        }

        //Overriding the == operator
        public static bool operator ==(Entity<TId> lhs, Entity<TId> rhs)
        {
            return lhs.Equals(rhs);
        }

        //Overriding the != operator
        public static bool operator !=(Entity<TId> lhs, Entity<TId> rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
