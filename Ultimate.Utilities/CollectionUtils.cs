using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Ultimate.Utilities
{
    /// <summary>
    /// Utility methods for collections.
    /// </summary>
    public static class CollectionUtils
    {
        /// <summary>
        /// Empty String Array
        /// </summary>
        public static string[] EmptyStringArray { get; } = {};

        /// <summary>
        /// Adds all elements in the enumeration to the given collection.
        /// </summary>
        /// <para>
        /// CollectionUtils.AddAll({ "Satish", "Kumar" }, { "Roronoa", "Zoro" })=  { "Satish", "Kumar", "Roronoa", "Zoro" }
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"> The collection to add to, must not be null</param>
        /// <param name="elements">  The enumeration of elements to add, must not be null</param>
        /// <returns></returns>
        public static bool AddAll<T>(ICollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                collection.Add(element);
            }

            return true;
        }

        /// <summary>
        /// Adds all non null elements in the enumeration to the given collection.
        /// </summary>
        /// <para>
        /// CollectionUtils.AddAllIgnoreNull({ "Satish", "Kumar" }, { "Roronoa", "Zoro",null })=  { "Satish", "Kumar", "Roronoa", "Zoro" }
        /// </para>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"> The collection to add to, must not be null</param>
        /// <param name="elements">  The enumeration of elements to add, must not be null</param>
        /// <returns></returns>
        public static bool AddAllIgnoreNull<T>(ICollection<T> collection, ICollection<T> elements)
        {
            foreach (var element in elements.Where(element => null != element))
            {
                collection.Add(element);
            }

            return true;
        }

        /// <summary>
        /// Adds all elements to the given collection from a specified index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"> The collection to add to, must not be null</param>
        /// <param name="elements">The list of elements to be added</param>
        /// <param name="index">index from where the elements has to be inserted</param>
        /// <param name="includeNulls">flag to add the null values also (default true)</param>
        /// <returns></returns>
        public static bool AddAllAtIndex<T>(IList<T> collection, IList<T> elements, int index, bool includeNulls = true)
        {
            var addList = includeNulls ? elements : elements.Where(element => null != element).ToList();

            foreach (var element in addList)
            {
                collection.Insert(index, element);
                index++;
            }
            return true;
        }


        /// <summary>
        /// Adds an element to the collection unless the element is null.
        /// </summary>
        /// <typeparam name="T">the type of object the Collection contains</typeparam>
        /// <param name="collection">the collection to add to, must not be null</param>
        /// <param name="element">the object to add, if null it will not be added</param>
        /// <returns></returns>
        public static bool AddIgnoreNull<T>(ICollection<T> collection, T element)
        {
            if (null == element) return false;
            collection.Add(element);
            return true;
        }

        /// <summary>
        /// Combines two lists and sorts them by default
        /// </summary>
        /// <param name="a">a - the first collection, must not be null</param>
        /// <param name="b">b - the second collection, must not be null</param>
        /// <param name="includeDuplicates">must be false if the duplicates should be removed (default true)</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static IList<T> Collate<T>(IEnumerable<T> a, IEnumerable<T> b, bool includeDuplicates = true)
        {

            return includeDuplicates ? a.Union(b).OrderBy(i => i).ToList() : a.Concat(b).OrderBy(i => i).ToList();


        }

        /// <summary>
        /// Combines two lists and sorts them using the given comparer
        /// </summary>
        /// <param name="a">a - the first collection, must not be null</param>
        /// <param name="b">b - the second collection, must not be null</param>
        /// <param name="comparer">comparer that must be used to sort the result</param>
        /// <param name="includeDuplicates">must be false if the duplicates should be removed (default true)</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static IList<T> Collate<T>(IEnumerable<T> a, IEnumerable<T> b, IComparer<T> comparer, bool includeDuplicates = true)
        {

            return includeDuplicates ? a.Union(b).OrderBy(i => i, comparer).ToList() : a.Concat(b).OrderBy(i => i, comparer).ToList();


        }

        /// <summary>
        /// Transforms all elements from input collection to the output collection of the given type.
        /// </summary>
        /// <typeparam name="TInput">input type</typeparam>
        /// <typeparam name="TOutput">output type</typeparam>
        /// <param name="inputCollection">The Collection to be converted</param>
        /// <param name="outputCollection">The output collection which is optional</param>
        /// <returns>output collection of given type</returns>
        public static ICollection<TOutput> CollectFromParent<TInput, TOutput>(IEnumerable<TInput> inputCollection, ICollection<TOutput> outputCollection = null) where TOutput : TInput
        {
            if (null == outputCollection)
                outputCollection = new List<TOutput>();
            foreach (var item in inputCollection)
            {
                outputCollection.Add((TOutput)item);
            }
            return outputCollection;
        }

        /// <summary>
        ///  Transforms all elements from input collection to the output collection of the given type.
        ///  Input collection type must extend output collection type
        /// </summary>
        /// <typeparam name="TInput">input type</typeparam>
        /// <typeparam name="TOutput">output type</typeparam>
        /// <param name="inputCollection">The Input Collection to be converted</param>
        /// <param name="outputCollection">The Output Collection </param>
        public static ICollection<TOutput> CollectFromChild<TInput, TOutput>(IEnumerable<TInput> inputCollection, ICollection<TOutput> outputCollection = null) where TInput : TOutput
        {
            if (null == outputCollection)
                outputCollection = new List<TOutput>();
            foreach (var item in inputCollection)
            {
                outputCollection.Add(item);
            }
            return outputCollection;
        }

        /// <summary>
        /// Returns true iff all elements of coll2 are also contained in coll1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll1">coll1 - the first collection, must not be null</param>
        /// <param name="coll2">coll2 - the second collection, must not be null</param>
        /// <returns>true iff the intersection of the collections has the same cardinality as the set of unique elements from the second collection</returns>
        public static bool ContainsAll<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            return coll1.Intersect(coll2).Count() == coll2.Distinct().Count();
        }

        /// <summary>
        /// Returns true iff at least one element is in both collections.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll1">coll1 - the first collection, must not be null</param>
        /// <param name="coll2">coll2 - the second collection, must not be null</param>
        /// <returns>true iff the intersection of the collections is non-empty</returns>
        public static bool ContainsAny<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            return coll1.Intersect(coll2).Any();
        }

        /// <summary>
        /// Returns a Collection containing the exclusive disjunction (symmetric difference) of the given collections.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll1">coll1 - the first collection, must not be null</param>
        /// <param name="coll2">coll2 - the second collection, must not be null</param>
        /// <returns>the symmetric difference of the two collections</returns>
        public static ICollection<T> Disjunction<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            return coll1.Concat(coll2).Except(coll1.Intersect(coll2)).ToList();

        }

        /// <summary>
        /// Returns the immutable EMPTY_COLLECTION with generic type safety.
        /// </summary>
        /// <typeparam name="T">T - the element type</typeparam>
        /// <returns>immutable empty collection</returns>
        public static ICollection<T> EmptyCollection<T>()
        {
            return new List<T>().AsReadOnly();
        }

        /// <summary>
        /// Returns an immutable empty collection if the argument is null, or the argument itself otherwise.
        /// </summary>
        /// <typeparam name="T">T - the element type</typeparam>
        /// <param name="collection">collection - the collection, possibly null</param>
        /// <returns>an empty collection if the argument is null</returns>
        public static ICollection<T> EmptyIfNull<T>(ICollection<T> collection)
        {
            return collection ?? new List<T>().AsReadOnly();
        }


        /// <summary>
        /// Returns the default collection if the argument is null, or the argument itself otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">collection - the collection, possibly null</param>
        /// /// <param name="defaultCollection">defaukt collection to be returned</param>
        /// <returns>given default collection if the argument is null</returns>
        public static ICollection<T> DefaultIfNull<T>(ICollection<T> collection,ICollection<T> defaultCollection)
        {
            return collection ?? defaultCollection;
        }

        /// <summary>
        /// Null-safe check if the specified collection is empty.
        /// Null returns true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll">coll - the collection to check, may be null</param>
        /// <returns>true if empty or null</returns>
        public static bool IsEmpty<T>(IEnumerable<T> coll)
        {
            return coll == null || !coll.Any();
        }

        /// <summary>
        /// Null-safe check if the specified collection is not empty.
        ///Null returns false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll">coll - the collection to check, may be null</param>
        /// <returns>true if non-null and non-empty</returns>
        public static bool IsNotEmpty<T>(IEnumerable<T> coll)
        {
            return !IsEmpty(coll);
        }

        /// <summary>
        /// Returns a Map mapping each unique element in the given Collection to an Integer representing the number of occurrences of that element in the Collection.
        /// Only those elements present in the collection will appear as keys in the map.
        /// </summary>
        /// <typeparam name="T">T - the type of object in the returned Map. This is a super type of .</typeparam>
        /// <param name="coll">coll - the collection to get the cardinality map for, must not be null</param>
        /// <returns>the populated cardinality map</returns>
        public static IDictionary<T, int> GetCardinalityMap<T>(IEnumerable<T> coll)
        {
            IDictionary<T, int> count = new Dictionary<T, int>();
            foreach (var obj in coll)
            {
                var c = count.Keys.Contains(obj) ? count[obj] : 0;
                if (c == 0)
                {
                    count[obj] = 1;
                }
                else
                {
                    count[obj] = c + 1;
                }
            }
            return count;
        }

        /// <summary>
        /// Returns true iff a is a sub-collection of b, that is, iff the cardinality of e in a is less than or equal to the cardinality of e in b, for each element e in a.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a">a - the first (sub?) collection, must not be null</param>
        /// <param name="b">b - the second (super?) collection, must not be null</param>
        /// <returns>true iff a is a sub-collection of b</returns>
        public static bool IsSubCollection<T>(ICollection<T> a, ICollection<T> b)
        {
            CardinalityHelper<T> helper = new CardinalityHelper<T>(a, b);
            return a.All(obj => helper.FreqA(obj) <= helper.FreqB(obj));
        }

        /// <summary>
        /// Returns true iff a is a proper sub-collection of b, that is, iff the cardinality of e in a is less than or equal to the cardinality of e in b, for each element e in a, and there is at least one element f such that the cardinality of f in b is strictly greater than the cardinality of f in a.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a">a - the first (sub?) collection, must not be null</param>
        /// <param name="b">b - the second (super?) collection, must not be null</param>
        /// <returns>true iff a is a proper sub-collection of b</returns>
        public static bool IsProperSubCollection<T>(ICollection<T> a, ICollection<T> b)
        {
            return a.Count() < b.Count() && IsSubCollection(a, b);
        }

        /// <summary>
        /// Returns true iff the given Collections contain exactly the same elements with exactly the same cardinalities.
        /// i.e.,, iff the cardinality of e in a is equal to the cardinality of e in b, for each element e in a or b.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a">a - the first collection, must not be null</param>
        /// <param name="b">b - the second collection, must not be null</param>
        /// <returns>true iff the collections contain the same elements with the same cardinalities.</returns>
        public static bool IsEqualCollection<T>(ICollection<T> a, ICollection<T> b)
        {
            if (a.Count() != b.Count())
            {
                return false;
            }
            var helper = new CardinalityHelper<T>(a, b);
            return helper.CardinalityA.Count() == helper.CardinalityB.Count() && helper.CardinalityA.Keys.All(obj => helper.FreqA(obj) == helper.FreqB(obj));
        }


        /// <summary>
        /// Returns true iff the given Collections contain exactly the same elements with exactly the same cardinalities.
        /// i.e.,, iff the cardinality of e in a is equal to the cardinality of e in b, for each element e in a or b.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a">a - the first collection, must not be null</param>
        /// <param name="b">b - the second collection, must not be null</param>
        /// <param name="predicate">the predicate passed to calculate the equality.The predicate method must be applied on the collection 'b' and should be expecting collection "a" element as parameter.(ex: (x)=>b.Any(i=>i.equals(x))) </param>
        /// <returns>true iff the collections contain the same elements matching the given predicate </returns>
        public static bool IsEqualCollection<T>(ICollection<T> a, ICollection<T> b, Predicate<T> predicate)
        {
            return a.Count() == b.Count() && a.All(predicate.Invoke);
        }

        /// <summary>
        /// Creates and returns a new collection by cloning the existing collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">collection to clone</param>
        /// <returns>a cloned copy of collection</returns>
        public static IList<T> Clone<T>(IList<T> collection)
        {
            return collection.Select(DeepClone).ToList();
        }

        /// <summary>
        /// Removes the elements in remove from collection. That is, this method returns a collection containing all the elements in c that are not in remove
        /// This method is useful if you do not wish to modify the collection itself and thus cannot call collection.RemoveAll();.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">collection - the collection from which items are removed (in the returned collection)</param>
        /// <param name="remove">remove - the items to be removed from the returned collection</param>
        /// <returns>a Collection containing all the elements of collection except any elements that also occur in remove.</returns>
        public static ICollection<T> RemoveAll<T>(IList<T> collection, IList<T> remove)
        {
            return collection.AsParallel().Where(i => !remove.Contains(i)).ToList();
        }

        /// <summary>
        /// Finds and returns the elements matching the given regular expression
        /// </summary>
        /// <param name="collection">the input collection,must not be null</param>
        /// <param name="regEx">the regular expression</param>
        /// <param name="options">options for the regular expression, defualt no options applied</param>
        /// <returns>returns the collection of elements matching the regular expression</returns>
        public static ICollection<string> FindWithRegEx(IEnumerable<string> collection, string regEx, RegexOptions options = RegexOptions.None)
        {

            return collection.Where(i => Regex.IsMatch(i, regEx, options)).ToList();
        }

        /// <summary>
        /// Gets the length of a collection. Null Safe
        /// </summary>
        /// <param name="collection">input collection</param>
        /// <returns>collection length or -1 if null</returns>
        public static int GetLength(this IEnumerable<object> collection)
        {
            return collection?.Count() ?? -1;
        }

        #region private class methods

        private class CardinalityHelper<T>
        {

            /** Contains the cardinality for each object in collection A. */
            public readonly IDictionary<T, int> CardinalityA;

            /** Contains the cardinality for each object in collection B. */
            public readonly IDictionary<T, int> CardinalityB;

            /**
             * Create a new CardinalityHelper for two collections.
             * @param a  the first collection
             * @param b  the second collection
             */
            public CardinalityHelper(IEnumerable<T> a, IEnumerable<T> b)
            {
                CardinalityA = GetCardinalityMap(a);
                CardinalityB = GetCardinalityMap(b);
            }

            /**
             * Returns the maximum frequency of an object.
             * @param obj  the object
             * @return the maximum frequency of the object
             */
/*
            public int Max(T obj)
            {
                return Math.Max(FreqA(obj), FreqB(obj));
            }
*/

            /**
             * Returns the minimum frequency of an object.
             * @param obj  the object
             * @return the minimum frequency of the object
             */
/*
            public int Min(T obj)
            {
                return Math.Min(FreqA(obj), FreqB(obj));
            }
*/

            /**
             * Returns the frequency of this object in collection A.
             * @param obj  the object
             * @return the frequency of the object in collection A
             */
            public int FreqA(T obj)
            {
                return GetFreq(obj, CardinalityA);
            }

            /**
             * Returns the frequency of this object in collection B.
             * @param obj  the object
             * @return the frequency of the object in collection B
             */
            public int FreqB(T obj)
            {
                return GetFreq(obj, CardinalityB);
            }

            private static int GetFreq(T obj, IDictionary<T, int> freqMap)
            {
                return freqMap.Keys.Contains(obj) ? freqMap[obj] : 0;

            }

        }

        private static T DeepClone<T>(T obj)
        {
            var searializedObj = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(searializedObj);
        }

        #endregion

    }


}
