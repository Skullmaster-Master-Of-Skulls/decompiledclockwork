using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace System.Web.ModelBinding
{
	// Token: 0x02000664 RID: 1636
	public class NameValueCollectionValueProvider : IValueProvider, IUnvalidatedValueProvider
	{
		// Token: 0x0600503B RID: 20539 RVA: 0x0011532D File Offset: 0x0011352D
		public NameValueCollectionValueProvider(NameValueCollection collection, CultureInfo culture) : this(collection, null, culture)
		{
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00115338 File Offset: 0x00113538
		public NameValueCollectionValueProvider(NameValueCollection collection, NameValueCollection unvalidatedCollection, CultureInfo culture)
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
					this._values[text] = new NameValueCollectionValueProvider.ValueProviderResultPlaceholder(text, this);
				}
			}
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x001153F0 File Offset: 0x001135F0
		public virtual bool ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			return this._prefixes.ContainsPrefix(prefix);
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x0011540C File Offset: 0x0011360C
		public virtual ValueProviderResult GetValue(string key)
		{
			return this.GetValue(key, false);
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00115418 File Offset: 0x00113618
		public virtual ValueProviderResult GetValue(string key, bool skipValidation)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			NameValueCollectionValueProvider.ValueProviderResultPlaceholder valueProviderResultPlaceholder;
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

		// Token: 0x04002AB9 RID: 10937
		private readonly CultureInfo _culture;

		// Token: 0x04002ABA RID: 10938
		private readonly PrefixContainer _prefixes;

		// Token: 0x04002ABB RID: 10939
		private readonly Dictionary<string, NameValueCollectionValueProvider.ValueProviderResultPlaceholder> _values = new Dictionary<string, NameValueCollectionValueProvider.ValueProviderResultPlaceholder>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002ABC RID: 10940
		private readonly NameValueCollection _validatedCollection;

		// Token: 0x04002ABD RID: 10941
		private readonly NameValueCollection _unvalidatedCollection;

		// Token: 0x02000A2E RID: 2606
		private sealed class ValueProviderResultPlaceholder
		{
			// Token: 0x06006E4F RID: 28239 RVA: 0x00189874 File Offset: 0x00187A74
			public ValueProviderResultPlaceholder(string key, NameValueCollectionValueProvider valueProvider)
			{
				this._validatedResultAccessor = (() => NameValueCollectionValueProvider.ValueProviderResultPlaceholder.GetResultFromCollection(key, valueProvider, true));
				this._unvalidatedResultAccessor = (() => NameValueCollectionValueProvider.ValueProviderResultPlaceholder.GetResultFromCollection(key, valueProvider, false));
			}

			// Token: 0x06006E50 RID: 28240 RVA: 0x001898C0 File Offset: 0x00187AC0
			private static ValueProviderResult GetResultFromCollection(string key, NameValueCollectionValueProvider valueProvider, bool useValidatedCollection)
			{
				NameValueCollection nameValueCollection = useValidatedCollection ? valueProvider._validatedCollection : valueProvider._unvalidatedCollection;
				string[] values = nameValueCollection.GetValues(key);
				string attemptedValue = nameValueCollection[key];
				return new ValueProviderResult(values, attemptedValue, valueProvider._culture);
			}

			// Token: 0x17001E37 RID: 7735
			// (get) Token: 0x06006E51 RID: 28241 RVA: 0x001898FC File Offset: 0x00187AFC
			public ValueProviderResult ValidatedResult
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ValueProviderResult>(ref this._validatedResult, this._validatedResultAccessor);
				}
			}

			// Token: 0x17001E38 RID: 7736
			// (get) Token: 0x06006E52 RID: 28242 RVA: 0x0018990F File Offset: 0x00187B0F
			public ValueProviderResult UnvalidatedResult
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ValueProviderResult>(ref this._unvalidatedResult, this._unvalidatedResultAccessor);
				}
			}

			// Token: 0x04003AD3 RID: 15059
			private readonly Func<ValueProviderResult> _validatedResultAccessor;

			// Token: 0x04003AD4 RID: 15060
			private readonly Func<ValueProviderResult> _unvalidatedResultAccessor;

			// Token: 0x04003AD5 RID: 15061
			private ValueProviderResult _validatedResult;

			// Token: 0x04003AD6 RID: 15062
			private ValueProviderResult _unvalidatedResult;
		}
	}
}
