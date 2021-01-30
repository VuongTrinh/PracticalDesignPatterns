using System;

namespace CORAndSpecification
{
    /// <summary>
    /// Specification pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T o);
    }

    public class Specification<T> : ISpecification<T>
    {
        private Func<T, bool> expression;
        public Specification(Func<T, bool> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;
        }

        public bool IsSatisfiedBy(T o)
        {
            return this.expression(o);
        }
    }


    public static class SpecificationExtensions
    {
        public static Specification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            if (left != null && right != null)
            {
                return new Specification<T>(o => left.IsSatisfiedBy(o) && right.IsSatisfiedBy(o));
            }
            return null;
        }
        public static Specification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            if (left != null && right != null)
            {
                return new Specification<T>(o => left.IsSatisfiedBy(o) || right.IsSatisfiedBy(o));
            }
            return null;
        }
        public static Specification<T> Not<T>(this ISpecification<T> left)
        {
            if (left != null)
            {
                return new Specification<T>(o => !left.IsSatisfiedBy(o));
            }
            return null;
        }
    }
}
