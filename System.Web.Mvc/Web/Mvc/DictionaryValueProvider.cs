using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x02000083 RID: 131
	public class DictionaryValueProvider<TValue> : IEnumerableValueProvider, IValueProvider
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0000BA4C File Offset: 0x00009C4C
		public DictionaryValueProvider(IDictionary<string, TValue> dictionary, CultureInfo culture)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			foreach (KeyValuePair<string, TValue> keyValuePair in dictionary)
			{
				object obj = keyValuePair.Value;
				string attemptedValue = Convert.ToString(obj, culture);
				this._values[keyValuePair.Key] = new ValueProviderResult(obj, attemptedValue, culture);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		private PrefixContainer PrefixContainer
		{
			get
			{
				if (this._prefixContainer == null)
				{
					this._prefixContainer = new PrefixContainer(this._values.Keys);
				}
				return this._prefixContainer;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000BB06 File Offset: 0x00009D06
		public virtual bool ContainsPrefix(string prefix)
		{
			return this.PrefixContainer.ContainsPrefix(prefix);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000BB14 File Offset: 0x00009D14
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

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000BB3F File Offset: 0x00009D3F
		public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			return this.PrefixContainer.GetKeysFromPrefix(prefix);
		}

		// Token: 0x0400010E RID: 270
		private PrefixContainer _prefixContainer;

		// Token: 0x0400010F RID: 271
		private readonly Dictionary<string, ValueProviderResult> _values = new Dictionary<string, ValueProviderResult>(StringComparer.OrdinalIgnoreCase);
	}
}
