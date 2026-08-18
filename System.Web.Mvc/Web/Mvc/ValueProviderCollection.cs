using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000136 RID: 310
	public class ValueProviderCollection : Collection<IValueProvider>, IUnvalidatedValueProvider, IEnumerableValueProvider, IValueProvider
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x00015F23 File Offset: 0x00014123
		public ValueProviderCollection()
		{
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00015F2B File Offset: 0x0001412B
		public ValueProviderCollection(IList<IValueProvider> list) : base(list)
		{
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00015F34 File Offset: 0x00014134
		public virtual bool ContainsPrefix(string prefix)
		{
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				if (base[i].ContainsPrefix(prefix))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00015F66 File Offset: 0x00014166
		public virtual ValueProviderResult GetValue(string key)
		{
			return this.GetValue(key, false);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00015F70 File Offset: 0x00014170
		public virtual ValueProviderResult GetValue(string key, bool skipValidation)
		{
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				ValueProviderResult valueFromProvider = ValueProviderCollection.GetValueFromProvider(base[i], key, skipValidation);
				if (valueFromProvider != null)
				{
					return valueFromProvider;
				}
			}
			return null;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000160FC File Offset: 0x000142FC
		public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			return (from provider in this
			let result = ValueProviderCollection.GetKeysFromPrefixFromProvider(provider, prefix)
			where result != null && result.Any<KeyValuePair<string, string>>()
			select result).FirstOrDefault<IDictionary<string, string>>() ?? new Dictionary<string, string>();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001617C File Offset: 0x0001437C
		internal static ValueProviderResult GetValueFromProvider(IValueProvider provider, string key, bool skipValidation)
		{
			IUnvalidatedValueProvider unvalidatedValueProvider = provider as IUnvalidatedValueProvider;
			if (unvalidatedValueProvider == null)
			{
				return provider.GetValue(key);
			}
			return unvalidatedValueProvider.GetValue(key, skipValidation);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000161A4 File Offset: 0x000143A4
		internal static IDictionary<string, string> GetKeysFromPrefixFromProvider(IValueProvider provider, string prefix)
		{
			IEnumerableValueProvider enumerableValueProvider = provider as IEnumerableValueProvider;
			if (enumerableValueProvider == null)
			{
				return null;
			}
			return enumerableValueProvider.GetKeysFromPrefix(prefix);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000161C4 File Offset: 0x000143C4
		protected override void InsertItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000161DC File Offset: 0x000143DC
		protected override void SetItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}
	}
}
