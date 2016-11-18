using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq;

namespace Ultimate.Utilities
{
    /// <summary>
    /// Utility methods for Objects
    /// </summary>
    public static class ObjectUtils
    {

        private static readonly Dictionary<Type, IEnumerable<PropertyInfo>> PropertyInfoDictionary = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        private static readonly Dictionary<string, IEnumerable<CommonProperties>> CommonPropsDict = new Dictionary<string, IEnumerable<CommonProperties>>();

        /// <summary>
        ///  Returns the sub element of an object if the object is not null.
        /// </summary>
        /// <typeparam name="TInput">input object type</typeparam>
        /// <typeparam name="TOutput">ouput object type</typeparam>
        /// <param name="input">input object</param>
        /// <param name="expression">Lambda expression to return the output</param>
        /// <returns>If the input is null then returns the default value of output type else returns the output object</returns>
        public static TOutput IfNotNull<TInput, TOutput>(this TInput input, Func<TInput, TOutput> expression)
        {
            return input == null ? default(TOutput) : expression(input);
        }

        /// <summary>
        /// Deep Clones an object and returns a new object
        /// </summary>
        /// <typeparam name="T">Type of input object</typeparam>
        /// <param name="input">input object</param>
        /// <returns>A new object cloned from the input</returns>
        public static T DeepClone<T>(T input)
        {
            var searializedObj = JsonConvert.SerializeObject(input);
            return JsonConvert.DeserializeObject<T>(searializedObj);
        }

        /// <summary>
        /// Maps source object properties to destination object that have identical name and type.Mapping is not case sensitive.
        /// </summary>
        /// <description>
        /// Uses dictionary to cache the properties info which makes the mapping faster.
        /// </description>
        /// <param name="source">source object to map from</param>
        /// <param name="destination">destination object to map to</param>
        public static void MapObjects(object source, object destination)
        {
            if (source == null || destination == null)
                return;

            var commonproperties = GetCommonproperties(source.GetType(), destination.GetType());

            foreach (var match in commonproperties)
            {
                match.Dp.SetValue(destination, match.Sp.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Maps all values from the source list to a destination object type and returns a destination object list
        /// </summary>
        /// <typeparam name="TIn">source object type</typeparam>
        /// <typeparam name="TOut">destination object type</typeparam>
        /// <param name="source">source list</param>
        /// <returns>destination object list</returns>
        public static IList<TOut> MapObjectsList<TIn, TOut>(IEnumerable<TIn> source) where TIn : class where TOut : class
        {
            if (source == null)
                return null;
            var destination = new List<TOut>();
            var sourcetype = typeof(TIn);
            var destinationtype = typeof(TOut);

            var commonproperties = GetCommonproperties(sourcetype, destinationtype);

            var enumerable = source as TIn[] ?? source.ToArray();
            var count = enumerable.Length;
            for (var i = 0; i < count; i++)
            {
                var input = enumerable[i];
                var output = Activator.CreateInstance<TOut>();
                foreach (var match in commonproperties)
                {
                    match.Dp.SetValue(output, match.Sp.GetValue(input, null), null);
                }

                destination.Add(output);
            }
            return destination;

        }

        private static IQueryable<CommonProperties> GetCommonproperties(Type sourcetype, Type destinationtype)
        {
            var key = sourcetype.Name + "::" + destinationtype.Name;
            if (CommonPropsDict.ContainsKey(key))
                return CommonPropsDict[key].AsQueryable();

            if (!PropertyInfoDictionary.Keys.Contains(sourcetype))
                PropertyInfoDictionary.Add(sourcetype, sourcetype.GetProperties());

            if (!PropertyInfoDictionary.Keys.Contains(destinationtype))
                PropertyInfoDictionary.Add(destinationtype, destinationtype.GetProperties());

            var sourceProperties = PropertyInfoDictionary[sourcetype];
            var destionationProperties = PropertyInfoDictionary[destinationtype];

            var commonproperties = from sp in sourceProperties
                                   join dp in destionationProperties on sp.Name.ToLower() equals
                                   dp.Name.ToLower()
                                   where sp.PropertyType == dp.PropertyType
                                   select new CommonProperties(sp, dp);
            CommonPropsDict.Add(sourcetype.Name + "::" + destinationtype.Name, commonproperties);

            return CommonPropsDict[key].AsQueryable();

        }
    }

    internal class CommonProperties
    {
        public CommonProperties(PropertyInfo sp, PropertyInfo dp)
        {
            Sp = sp;
            Dp = dp;
        }
        public PropertyInfo Sp { get; set; }
        public PropertyInfo Dp { get; set; }
    }
}