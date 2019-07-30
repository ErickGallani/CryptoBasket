namespace CryptoBasket.Domain.Core.ValueObjects
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ValueObject<T>
    {
        public abstract IEnumerable<object> GetValues();

        public override bool Equals(object other)
        {
            if (other == null || other.GetType() != GetType())
            {
                return false;
            }

            var otherObject = other as ValueObject<T>;

            IEnumerator<object> thisValues = GetValues().GetEnumerator();
            IEnumerator<object> otherValues = otherObject.GetValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode() =>
            GetValues()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);

        public static bool operator ==(ValueObject<T> leftObject, ValueObject<T> rightObject) =>
            leftObject.Equals(rightObject);

        public static bool operator !=(ValueObject<T> leftObject, ValueObject<T> rightObject) =>
            !leftObject.Equals(rightObject);
    }
}
