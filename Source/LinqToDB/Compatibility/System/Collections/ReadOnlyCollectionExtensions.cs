#if THE_RAOT_CORE
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LinqToDB.Compatibility.System.Collections
{
	internal static class ReadOnlyCollectionExtensions
	{
		/// <summary>
		/// Views a <see cref="IDictionary{K,V}"/> as a read-only dictionary
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <returns></returns>
		public static IReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dict)
		{
			return new ReadOnlyDictionary<TKey, TValue>(dict);
		}
	}
}
#endif
