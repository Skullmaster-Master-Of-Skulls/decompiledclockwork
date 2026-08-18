using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x0200019F RID: 415
	public class NameValuePairsValueProvider : IEnumerableValueProvider, IValueProvider
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x00023325 File Offset: 0x00021525
		public NameValuePairsValueProvider(IEnumerable<KeyValuePair<string, string>> values, CultureInfo culture)
		{
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this._values = NameValuePairsValueProvider.InitializeValues<string>(values);
			this._culture = culture;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00023368 File Offset: 0x00021568
		public NameValuePairsValueProvider(Func<IEnumerable<KeyValuePair<string, string>>> valuesFactory, CultureInfo culture)
		{
			if (valuesFactory == null)
			{
				throw Error.ArgumentNull("valuesFactory");
			}
			this._lazyValues = new Lazy<Dictionary<string, object>>(() => NameValuePairsValueProvider.InitializeValues<string>(valuesFactory()), true);
			this._culture = culture;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000233C1 File Offset: 0x000215C1
		public NameValuePairsValueProvider(IDictionary<string, object> values, CultureInfo culture)
		{
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this._values = NameValuePairsValueProvider.InitializeValues<object>(values);
			this._culture = culture;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000233EA File Offset: 0x000215EA
		internal CultureInfo Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000233F2 File Offset: 0x000215F2
		private PrefixContainer PrefixContainer
		{
			get
			{
				if (this._prefixContainer == null)
				{
					this._prefixContainer = new PrefixContainer(this.Values.Keys);
				}
				return this._prefixContainer;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00023418 File Offset: 0x00021618
		private Dictionary<string, object> Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = this._lazyValues.Value;
				}
				return this._values;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002343C File Offset: 0x0002163C
		private static Dictionary<string, object> InitializeValues<T>(IEnumerable<KeyValuePair<string, T>> nameValuePairs) where T : class
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			KeyValuePair<string, string>[] array = nameValuePairs as KeyValuePair<string, string>[];
			if (array != null && array.Length == 0)
			{
				return dictionary;
			}
			Dictionary<string, object> dictionary2 = nameValuePairs as Dictionary<string, object>;
			if (dictionary2 != null && dictionary2.Count == 0)
			{
				return dictionary;
			}
			foreach (KeyValuePair<string, T> keyValuePair in nameValuePairs)
			{
				string key = keyValuePair.Key;
				object obj;
				if (dictionary.TryGetValue(key, out obj))
				{
					List<T> list = obj as List<T>;
					if (list == null)
					{
						dictionary[key] = new List<T>
						{
							obj as T,
							keyValuePair.Value
						};
					}
					else
					{
						list.Add(keyValuePair.Value);
					}
				}
				else
				{
					dictionary[key] = keyValuePair.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00023534 File Offset: 0x00021734
		public virtual bool ContainsPrefix(string prefix)
		{
			return this.PrefixContainer.ContainsPrefix(prefix);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00023542 File Offset: 0x00021742
		public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw Error.ArgumentNull("prefix");
			}
			return this.PrefixContainer.GetKeysFromPrefix(prefix);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00023560 File Offset: 0x00021760
		public virtual ValueProviderResult GetValue(string key)
		{
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			object obj;
			if (this.Values.TryGetValue(key, out obj))
			{
				return new ValueProviderResult(obj, NameValuePairsValueProvider.GetAttemptedValue(obj), this._culture);
			}
			return null;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000235A0 File Offset: 0x000217A0
		private static string GetAttemptedValue(object value)
		{
			List<string> list = value as List<string>;
			if (list == null)
			{
				return value as string;
			}
			return string.Join(",", list);
		}

		// Token: 0x04000311 RID: 785
		private readonly CultureInfo _culture;

		// Token: 0x04000312 RID: 786
		private PrefixContainer _prefixContainer;

		// Token: 0x04000313 RID: 787
		private Dictionary<string, object> _values;

		// Token: 0x04000314 RID: 788
		private readonly Lazy<Dictionary<string, object>> _lazyValues;
	}
}
