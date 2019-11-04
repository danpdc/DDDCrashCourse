using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DDDCrashCourse.SharedKernel.Types
{
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        #region Implementing equality
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            T other = obj as T;

            return Equals(other);
        }

        public virtual bool Equals(T other)
        {
            if (other is null)
                return false;

            Type t = GetType();
            Type otherType = other.GetType();

            if (t != otherType)
                return false;

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 17;
            int multiplier = 59;

            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        public static bool operator ==(ValueObject<T> lhs, ValueObject<T> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ValueObject<T> lhs, ValueObject<T> rhs)
        {
            return !(lhs == rhs);
        }
        #endregion

        #region Private members

        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            List<FieldInfo> fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }
        #endregion
    }
}
