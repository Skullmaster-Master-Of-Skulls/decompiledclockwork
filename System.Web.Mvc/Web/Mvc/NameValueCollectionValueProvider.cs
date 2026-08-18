using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000038 RID: 56
	public class NameValueCollectionValueProvider : IUnvalidatedValueProvider, IEnumerableValueProvider, IValueProvider
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000052DD File Offset: 0x000034DD
		public NameValueCollectionValueProvider(NameValueCollection collection, CultureInfo culture) : this(collection, null, culture)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000052E8 File Offset: 0x000034E8
		public NameValueCollectionValueProvider(NameValueCollection collection, NameValueCollection unvalidatedCollection, CultureInfo culture) : this(collection, unvalidatedCollection, culture, false)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000052F4 File Offset: 0x000034F4
		public NameValueCollectionValueProvider(NameValueCollection collection, NameValueCollection unvalidatedCollection, CultureInfo culture, bool jQueryToMvcRequestNormalizationRequired)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._unvalidatedCollection = (unvalidatedCollection ?? collection);
			this._collection = collection;
			this._culture = culture;
			this._jQueryToMvcRequestNormalizationRequired = jQueryToMvcRequestNormalizationRequired;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000532C File Offset: 0x0000352C
		private Dictionary<string, NameValueCollectionValueProvider.ValueProviderResultPlaceholder> Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = this.InitializeCollectionValues();
				}
				return this._values;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005348 File Offset: 0x00003548
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

		// Token: 0x06000115 RID: 277 RVA: 0x0000536E File Offset: 0x0000356E
		public virtual bool ContainsPrefix(string prefix)
		{
			return this.PrefixContainer.ContainsPrefix(prefix);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000537C File Offset: 0x0000357C
		public virtual ValueProviderResult GetValue(string key)
		{
			return this.GetValue(key, false);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005388 File Offset: 0x00003588
		public virtual ValueProviderResult GetValue(string key, bool skipValidation)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			NameValueCollectionValueProvider.ValueProviderResultPlaceholder valueProviderResultPlaceholder;
			this.Values.TryGetValue(key, out valueProviderResultPlaceholder);
			if (valueProviderResultPlaceholder == null)
			{
				return null;
			}
			if (!skipValidation)
			{
				return valueProviderResultPlaceholder.ValidatedResult;
			}
			return valueProviderResultPlaceholder.UnvalidatedResult;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000053C7 File Offset: 0x000035C7
		public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			return this.PrefixContainer.GetKeysFromPrefix(prefix);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000053D8 File Offset: 0x000035D8
		private Dictionary<string, NameValueCollectionValueProvider.ValueProviderResultPlaceholder> InitializeCollectionValues()
		{
			Dictionary<string, NameValueCollectionValueProvider.ValueProviderResultPlaceholder> dictionary = new Dictionary<string, NameValueCollectionValueProvider.ValueProviderResultPlaceholder>(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in this._unvalidatedCollection)
			{
				string text = (string)obj;
				if (text != null)
				{
					string key = text;
					if (this._jQueryToMvcRequestNormalizationRequired)
					{
						key = NameValueCollectionValueProvider.NormalizeJQueryToMvc(text);
					}
					dictionary[key] = new NameValueCollectionValueProvider.ValueProviderResultPlaceholder(text, this._collection, this._unvalidatedCollection, this._culture);
				}
			}
			return dictionary;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000546C File Offset: 0x0000366C
		private static string NormalizeJQueryToMvc(string key)
		{
			if (key == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = null;
			int num = 0;
			for (;;)
			{
				int num2 = key.IndexOf('[', num);
				if (num2 < 0)
				{
					break;
				}
				stringBuilder = (stringBuilder ?? new StringBuilder());
				stringBuilder.Append(key, num, num2 - num);
				int num3 = key.IndexOf(']', num2);
				if (num3 == -1)
				{
					goto Block_6;
				}
				if (num3 != num2 + 1)
				{
					if (char.IsDigit(key[num2 + 1]))
					{
						stringBuilder.Append(key, num2, num3 - num2 + 1);
					}
					else
					{
						stringBuilder.Append('.');
						stringBuilder.Append(key, num2 + 1, num3 - num2 - 1);
					}
				}
				num = num3 + 1;
				if (num >= key.Length)
				{
					goto IL_CB;
				}
			}
			if (num == 0)
			{
				return key;
			}
			stringBuilder = (stringBuilder ?? new StringBuilder());
			stringBuilder.Append(key, num, key.Length - num);
			goto IL_CB;
			Block_6:
			throw Error.Argument("key", MvcResources.JQuerySyntaxMissingClosingBracket, new object[0]);
			IL_CB:
			return stringBuilder.ToString();
		}

		// Token: 0x04000042 RID: 66
		private PrefixContainer _prefixContainer;

		// Token: 0x04000043 RID: 67
		private NameValueCollection _collection;

		// Token: 0x04000044 RID: 68
		private NameValueCollection _unvalidatedCollection;

		// Token: 0x04000045 RID: 69
		private CultureInfo _culture;

		// Token: 0x04000046 RID: 70
		private bool _jQueryToMvcRequestNormalizationRequired;

		// Token: 0x04000047 RID: 71
		private Dictionary<string, NameValueCollectionValueProvider.ValueProviderResultPlaceholder> _values;

		// Token: 0x02000039 RID: 57
		private sealed class ValueProviderResultPlaceholder
		{
			// Token: 0x0600011B RID: 283 RVA: 0x0000554A File Offset: 0x0000374A
			public ValueProviderResultPlaceholder(string key, NameValueCollection validatedCollection, NameValueCollection unvalidatedCollection, CultureInfo culture)
			{
				this._key = key;
				this._validatedCollection = validatedCollection;
				this._unvalidatedCollection = unvalidatedCollection;
				this._culture = culture;
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600011C RID: 284 RVA: 0x0000556F File Offset: 0x0000376F
			public ValueProviderResult ValidatedResult
			{
				get
				{
					if (this._validatedResult == null)
					{
						this._validatedResult = NameValueCollectionValueProvider.ValueProviderResultPlaceholder.GetResultFromCollection(this._key, this._validatedCollection, this._culture);
					}
					return this._validatedResult;
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x0600011D RID: 285 RVA: 0x0000559C File Offset: 0x0000379C
			public ValueProviderResult UnvalidatedResult
			{
				get
				{
					if (this._unvalidatedResult == null)
					{
						this._unvalidatedResult = NameValueCollectionValueProvider.ValueProviderResultPlaceholder.GetResultFromCollection(this._key, this._unvalidatedCollection, this._culture);
					}
					return this._unvalidatedResult;
				}
			}

			// Token: 0x0600011E RID: 286 RVA: 0x000055CC File Offset: 0x000037CC
			private static ValueProviderResult GetResultFromCollection(string key, NameValueCollection collection, CultureInfo culture)
			{
				string[] values = collection.GetValues(key);
				string attemptedValue = collection[key];
				return new ValueProviderResult(values, attemptedValue, culture);
			}

			// Token: 0x04000048 RID: 72
			private ValueProviderResult _validatedResult;

			// Token: 0x04000049 RID: 73
			private ValueProviderResult _unvalidatedResult;

			// Token: 0x0400004A RID: 74
			private string _key;

			// Token: 0x0400004B RID: 75
			private NameValueCollection _validatedCollection;

			// Token: 0x0400004C RID: 76
			private NameValueCollection _unvalidatedCollection;

			// Token: 0x0400004D RID: 77
			private CultureInfo _culture;
		}
	}
}
