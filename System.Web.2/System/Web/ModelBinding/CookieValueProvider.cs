using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace System.Web.ModelBinding
{
	// Token: 0x02000624 RID: 1572
	public sealed class CookieValueProvider : IValueProvider, IUnvalidatedValueProvider
	{
		// Token: 0x06004EC2 RID: 20162 RVA: 0x001121D7 File Offset: 0x001103D7
		public CookieValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : this(modelBindingExecutionContext, modelBindingExecutionContext.HttpContext.Request.Unvalidated)
		{
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x001121F0 File Offset: 0x001103F0
		internal CookieValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, UnvalidatedRequestValuesBase unvalidatedValues) : this(modelBindingExecutionContext.HttpContext.Request.Cookies, unvalidatedValues.Cookies, CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x00112214 File Offset: 0x00110414
		internal CookieValueProvider(HttpCookieCollection collection, HttpCookieCollection unvalidatedCollection, CultureInfo culture)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._culture = culture;
			this._prefixes = new PrefixContainer(collection.Keys.Cast<string>());
			this._validatedCollection = collection;
			this._unvalidatedCollection = (unvalidatedCollection ?? collection);
			foreach (object obj in collection)
			{
				string text = (string)obj;
				if (text != null)
				{
					this._values[text] = new CookieValueProvider.ValueProviderResultPlaceholder(text, this);
				}
			}
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x001122CC File Offset: 0x001104CC
		public bool ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			return this._prefixes.ContainsPrefix(prefix);
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x001122E8 File Offset: 0x001104E8
		public ValueProviderResult GetValue(string key)
		{
			return this.GetValue(key, false);
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x001122F4 File Offset: 0x001104F4
		public ValueProviderResult GetValue(string key, bool skipValidation)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			CookieValueProvider.ValueProviderResultPlaceholder valueProviderResultPlaceholder;
			this._values.TryGetValue(key, out valueProviderResultPlaceholder);
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

		// Token: 0x04002A4C RID: 10828
		private readonly CultureInfo _culture;

		// Token: 0x04002A4D RID: 10829
		private readonly PrefixContainer _prefixes;

		// Token: 0x04002A4E RID: 10830
		private readonly Dictionary<string, CookieValueProvider.ValueProviderResultPlaceholder> _values = new Dictionary<string, CookieValueProvider.ValueProviderResultPlaceholder>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002A4F RID: 10831
		private readonly HttpCookieCollection _validatedCollection;

		// Token: 0x04002A50 RID: 10832
		private readonly HttpCookieCollection _unvalidatedCollection;

		// Token: 0x02000A14 RID: 2580
		private sealed class ValueProviderResultPlaceholder
		{
			// Token: 0x06006DE7 RID: 28135 RVA: 0x00188E58 File Offset: 0x00187058
			public ValueProviderResultPlaceholder(string key, CookieValueProvider valueProvider)
			{
				this._validatedResultAccessor = (() => CookieValueProvider.ValueProviderResultPlaceholder.GetResultFromCollection(key, valueProvider, true));
				this._unvalidatedResultAccessor = (() => CookieValueProvider.ValueProviderResultPlaceholder.GetResultFromCollection(key, valueProvider, false));
			}

			// Token: 0x06006DE8 RID: 28136 RVA: 0x00188EA4 File Offset: 0x001870A4
			private static ValueProviderResult GetResultFromCollection(string key, CookieValueProvider valueProvider, bool useValidatedCollection)
			{
				HttpCookieCollection httpCookieCollection = useValidatedCollection ? valueProvider._validatedCollection : valueProvider._unvalidatedCollection;
				string value = httpCookieCollection[key].Value;
				return new ValueProviderResult(value, value, valueProvider._culture);
			}

			// Token: 0x17001E2D RID: 7725
			// (get) Token: 0x06006DE9 RID: 28137 RVA: 0x00188EDD File Offset: 0x001870DD
			public ValueProviderResult ValidatedResult
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ValueProviderResult>(ref this._validatedResult, this._validatedResultAccessor);
				}
			}

			// Token: 0x17001E2E RID: 7726
			// (get) Token: 0x06006DEA RID: 28138 RVA: 0x00188EF0 File Offset: 0x001870F0
			public ValueProviderResult UnvalidatedResult
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ValueProviderResult>(ref this._unvalidatedResult, this._unvalidatedResultAccessor);
				}
			}

			// Token: 0x04003A91 RID: 14993
			private readonly Func<ValueProviderResult> _validatedResultAccessor;

			// Token: 0x04003A92 RID: 14994
			private readonly Func<ValueProviderResult> _unvalidatedResultAccessor;

			// Token: 0x04003A93 RID: 14995
			private ValueProviderResult _validatedResult;

			// Token: 0x04003A94 RID: 14996
			private ValueProviderResult _unvalidatedResult;
		}
	}
}
