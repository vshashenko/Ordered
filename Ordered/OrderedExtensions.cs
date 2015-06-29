using System;
using System.Collections.Generic;

namespace Ordered
{
    /// <summary>
    /// Algorithms for ordered collections.
    /// Thus all input collections must be sorted to get correct result.
    /// </summary>
    public static class OrderedExtensions
    {
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> seq)
        {
            return Distinct(seq, EqualityComparer<T>.Default);
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> seq, IEqualityComparer<T> comparer)
        {
            var currentValue = default(T);
            var enum1 = seq.GetEnumerator();

            if (enum1.MoveNext())
            {
                currentValue = enum1.Current;
                yield return currentValue;

                while (enum1.MoveNext())
                {
                    if (!comparer.Equals(currentValue, enum1.Current))
                    {
                        currentValue = enum1.Current;
                        yield return currentValue;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> seq)
        {
            return Distinct(seq, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> seq, IEqualityComparer<T> comparer)
        {
            var currentValue = default(T);
            var enum1 = seq.GetEnumerator();

            if (enum1.MoveNext())
            {
                currentValue = enum1.Current;
                yield return currentValue;

                while (enum1.MoveNext())
                {
                    if (!comparer.Equals(currentValue, enum1.Current))
                    {
                        currentValue = enum1.Current;
                        yield return currentValue;
                    }
                }
            }
        }

        /// <summary>
        /// Subtracts second collection from first collection.
        /// The complexity is O(n).
        /// </summary>
        public static IEnumerable<T> Difference<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
        {
            return Difference(seq1, seq2, Comparer<T>.Default);
        }

        /// <summary>
        /// Subtracts second collection from first collection.
        /// The complexity is O(n).
        /// </summary>
        public static IEnumerable<T> Difference<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, IComparer<T> comparer)
        {
            var enum1 = seq1.GetEnumerator();
            var enum2 = seq2.GetEnumerator();

            var b1 = enum1.MoveNext();
            var b2 = enum2.MoveNext();

            while (b1 && b2)
            {
                if (comparer.Compare(enum1.Current, enum2.Current) < 0)
                {
                    yield return enum1.Current;
                    b1 = enum1.MoveNext();
                }
                else if (comparer.Compare(enum2.Current, enum1.Current) < 0)
                {
                    b2 = enum2.MoveNext();
                }
                else
                {
                    b1 = enum1.MoveNext();
                    b2 = enum2.MoveNext();
                }
            }

            // return the rest of the first collection
            while (b1)
            {
                yield return enum1.Current;
                b1 = enum1.MoveNext();
            }
        }

        /// <summary>
        /// Intersects two collections. Equal elements output from the first collection.
        /// The complexity is O(n).
        /// </summary>
        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
        {
            return Intersect(seq1, seq2, Comparer<T>.Default);
        }

        /// <summary>
        /// Intersects two collections. Equal elements output from the first collection.
        /// The complexity is O(n).
        /// </summary>
        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, IComparer<T> comparer)
        {
            var enum1 = seq1.GetEnumerator();
            var enum2 = seq2.GetEnumerator();

            var b1 = enum1.MoveNext();
            var b2 = enum2.MoveNext();

            while (b1 && b2)
            {
                if (comparer.Compare(enum1.Current, enum2.Current) < 0)
                {
                    b1 = enum1.MoveNext();
                }
                else if (comparer.Compare(enum2.Current, enum1.Current) < 0)
                {
                    b2 = enum2.MoveNext();
                }
                else
                {
                    yield return enum1.Current;

                    b1 = enum1.MoveNext();
                    b2 = enum2.MoveNext();
                }
            }
        }

        /// <summary>
        /// Unions two collections. Equal elements output from the first collection.
        /// The complexity is O(n).
        /// </summary>
        public static IEnumerable<T> Union<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2)
        {
            return Union(seq1, seq2, Comparer<T>.Default);
        }

        /// <summary>
        /// Unions two collections. Equal elements output from the first collection.
        /// The complexity is O(n).
        /// </summary>
        public static IEnumerable<T> Union<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, IComparer<T> comparer)
        {
            var enum1 = seq1.GetEnumerator();
            var enum2 = seq2.GetEnumerator();

            var b1 = enum1.MoveNext();
            var b2 = enum2.MoveNext();

            while (b1 && b2)
            {
                if (comparer.Compare(enum1.Current, enum2.Current) < 0)
                {
                    yield return enum1.Current;
                    b1 = enum1.MoveNext();
                }
                else if (comparer.Compare(enum2.Current, enum1.Current) < 0)
                {
                    yield return enum2.Current;
                    b2 = enum2.MoveNext();
                }
                else
                {
                    yield return enum1.Current;

                    b1 = enum1.MoveNext();
                    b2 = enum2.MoveNext();
                }
            }

            while (b1)
            {
                yield return enum1.Current;
                b1 = enum1.MoveNext();
            }

            while (b2)
            {
                yield return enum2.Current;
                b2 = enum2.MoveNext();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOuter"></typeparam>
        /// <typeparam name="TInner"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="outer"></param>
        /// <param name="inner"></param>
        /// <param name="outerKeySelector"></param>
        /// <param name="innerKeySelector"></param>
        /// <param name="joinAction"></param>
        public static void GroupJoin<TOuter, TInner, TKey>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Action<TOuter, TInner> joinAction)
        {
            GroupJoin(outer, inner, outerKeySelector, innerKeySelector, joinAction, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Joins two collections as child-parent relationship by the specified key.
        /// Collections must be sorted by the key on which the join is performed.
        /// The complexity is O(n).
        /// </summary>
        /// <param name="outer">Outer collection. Contains parent elements.</param>
        /// <param name="inner">Inner collection. Contains child elements.</param>
        /// <param name="outerKeySelector">Key selector for the outer collection.</param>
        /// <param name="innerKeySelector">Key selection for the inner collection.</param>
        /// <param name="joinAction">Action performed on key equality between outer and inner collections.</param>
        /// <param name="comparer"></param>
        public static void GroupJoin<TOuter, TInner, TKey>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Action<TOuter, TInner> joinAction,
            IComparer<TKey> comparer)
        {
            var enum1 = outer.GetEnumerator();
            var enum2 = inner.GetEnumerator();

            var b1 = enum1.MoveNext();
            var b2 = enum2.MoveNext();

            while (b1 && b2)
            {
                var item1 = enum1.Current;
                var item2 = enum2.Current;

                var order = comparer.Compare(outerKeySelector(item1), innerKeySelector(item2));

                if (order < 0)
                {
                    b1 = enum1.MoveNext();
                }
                else if (order > 0)
                {
                    b2 = enum2.MoveNext();
                }
                else
                {
                    joinAction(item1, item2);
                    b2 = enum2.MoveNext();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int BinarySearchFirst<T>(this IList<T> arr, T val)
        {
            return arr.BinarySearchFirst(val, Comparer<T>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int BinarySearchFirst<T>(this IList<T> arr, T val, IComparer<T> comparer)
        {
            var lo = 0;
            var hi = arr.Count;

            while (lo < hi)
            {
                var mid = lo + (hi - lo) / 2;
                if (comparer.Compare(arr[mid], val) < 0)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid;
                }
            }

            if (lo < arr.Count && comparer.Compare(arr[lo], val) == 0)
            {
                return lo;
            }
            else
            {
                return ~lo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int BinarySearchLast<T>(this IList<T> arr, T val)
        {
            return arr.BinarySearchLast(val, Comparer<T>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int BinarySearchLast<T>(this IList<T> arr, T val, IComparer<T> comparer)
        {
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        public static void InsertOrdered<T>(this IList<T> list, T value, IComparer<T> comparer)
        {
            var i = list.BinarySearchFirst(value, comparer);
            i = i < 0 ? ~i : i;

            list.Insert(i, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        public static void RemoveOrdered<T>(this IList<T> list, T value, IComparer<T> comparer)
        {
            var i = list.BinarySearchFirst(value, comparer);
            if (i >= 0)
            {
                list.RemoveAt(i);
            }
        }
    }
}
