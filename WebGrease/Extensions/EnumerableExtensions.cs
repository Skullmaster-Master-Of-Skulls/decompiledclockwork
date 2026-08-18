using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Css.Extensions;

namespace WebGrease.Extensions
{
	// Token: 0x020000FB RID: 251
	internal static class EnumerableExtensions
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x00049374 File Offset: 0x00047574
		internal static bool HasAtLeast<T>(this IEnumerable<T> source, int atLeast)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num = 0;
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (++num == atLeast)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000493F4 File Offset: 0x000475F4
		internal static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> hash = new HashSet<TKey>();
			return from p in source
			where hash.Add(keySelector(p))
			select p;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004942B File Offset: 0x0004762B
		internal static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> range)
		{
			range.ForEach(new Action<KeyValuePair<TKey, TValue>>(dictionary.Add));
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00049440 File Offset: 0x00047640
		internal static void AddRange<TValue>(this BlockingCollection<TValue> collection, IEnumerable<TValue> range)
		{
			range.ForEach(new Action<TValue>(collection.Add));
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00049454 File Offset: 0x00047654
		internal static void Add<TKey>(this IDictionary<TKey, double> dictionary1, IEnumerable<KeyValuePair<TKey, double>> dictionary2)
		{
			foreach (KeyValuePair<TKey, double> keyValuePair in dictionary2)
			{
				TKey key = keyValuePair.Key;
				if (!dictionary1.ContainsKey(key))
				{
					dictionary1[key] = 0.0;
				}
				TKey key2;
				dictionary1[key2 = key] = dictionary1[key2] + keyValuePair.Value;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000494D4 File Offset: 0x000476D4
		internal static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			TValue result;
			if (!dictionary.TryGetValue(key, out result))
			{
				return default(TValue);
			}
			return result;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00049508 File Offset: 0x00047708
		internal static void Add<TKey>(this IDictionary<TKey, int> dictionary1, IEnumerable<KeyValuePair<TKey, int>> dictionary2)
		{
			foreach (KeyValuePair<TKey, int> keyValuePair in dictionary2)
			{
				TKey key = keyValuePair.Key;
				if (!dictionary1.ContainsKey(key))
				{
					dictionary1[key] = 0;
				}
				TKey key2;
				dictionary1[key2 = key] = dictionary1[key2] + keyValuePair.Value;
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00049580 File Offset: 0x00047780
		internal static void AddNamedConfig<TConfig>(this IDictionary<string, TConfig> configs, TConfig config) where TConfig : INamedConfig, new()
		{
			configs[config.Name ?? string.Empty] = config;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000495A0 File Offset: 0x000477A0
		internal static T GetNamedConfig<T>(this IDictionary<string, T> configDictionary, string configName = null) where T : class, INamedConfig, new()
		{
			if (configDictionary == null || !configDictionary.Any<KeyValuePair<string, T>>())
			{
				return Activator.CreateInstance<T>();
			}
			configName = (configName.AsNullIfWhiteSpace() ?? string.Empty);
			T result;
			if ((result = configDictionary.TryGetValue(configName)) == null && (result = configDictionary.TryGetValue(string.Empty)) == null && (result = (configName.IsNullOrWhitespace() ? configDictionary.FirstOrDefault<KeyValuePair<string, T>>().Value : default(T))) == null)
			{
				result = Activator.CreateInstance<T>();
			}
			return result;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00049624 File Offset: 0x00047824
		internal static TResult NullSafeAction<TObject, TResult>(this TObject obj, Func<TObject, TResult> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (obj != null)
			{
				return action(obj);
			}
			return default(TResult);
		}
	}
}
