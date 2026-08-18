using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x0200064D RID: 1613
	public class DictionaryValueProvider<TValue> : IValueProvider
	{
		// Token: 0x06004F93 RID: 20371 RVA: 0x0011472A File Offset: 0x0011292A
		public DictionaryValueProvider(IDictionary<string, TValue> dictionary, CultureInfo culture)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._prefixes = new PrefixContainer(dictionary.Keys);
			this.AddValues(dictionary, culture);
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0011476C File Offset: 0x0011296C
		private void AddValues(IDictionary<string, TValue> dictionary, CultureInfo culture)
		{
			foreach (KeyValuePair<string, TValue> keyValuePair in dictionary)
			{
				object obj = keyValuePair.Value;
				string attemptedValue = Convert.ToString(obj, culture);
				this._values[keyValuePair.Key] = new ValueProviderResult(obj, attemptedValue, culture);
			}
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x001147DC File Offset: 0x001129DC
		public virtual bool ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			return this._prefixes.ContainsPrefix(prefix);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x001147F8 File Offset: 0x001129F8
		public virtual ValueProviderResult GetValue(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ValueProviderResult result;
			this._values.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x04002A8A RID: 10890
		private readonly PrefixContainer _prefixes;

		// Token: 0x04002A8B RID: 10891
		private readonly Dictionary<string, ValueProviderResult> _values = new Dictionary<string, ValueProviderResult>(StringComparer.OrdinalIgnoreCase);
	}
}
