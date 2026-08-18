using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x0200019C RID: 412
	public class CompositeValueProvider : Collection<IValueProvider>, IEnumerableValueProvider, IValueProvider
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x0002312D File Offset: 0x0002132D
		public CompositeValueProvider()
		{
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00023135 File Offset: 0x00021335
		public CompositeValueProvider(IList<IValueProvider> list) : base(list)
		{
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00023140 File Offset: 0x00021340
		public virtual bool ContainsPrefix(string prefix)
		{
			foreach (IValueProvider valueProvider in this)
			{
				if (valueProvider.ContainsPrefix(prefix))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00023194 File Offset: 0x00021394
		public virtual ValueProviderResult GetValue(string key)
		{
			int count = base.Items.Count;
			for (int i = 0; i < count; i++)
			{
				IValueProvider valueProvider = base.Items[i];
				ValueProviderResult value = valueProvider.GetValue(key);
				if (value != null)
				{
					return value;
				}
			}
			return null;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000231D4 File Offset: 0x000213D4
		public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			foreach (IValueProvider provider in this)
			{
				IDictionary<string, string> keysFromPrefixFromProvider = CompositeValueProvider.GetKeysFromPrefixFromProvider(provider, prefix);
				if (keysFromPrefixFromProvider != null && keysFromPrefixFromProvider.Count > 0)
				{
					return keysFromPrefixFromProvider;
				}
			}
			return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002323C File Offset: 0x0002143C
		internal static IDictionary<string, string> GetKeysFromPrefixFromProvider(IValueProvider provider, string prefix)
		{
			IEnumerableValueProvider enumerableValueProvider = provider as IEnumerableValueProvider;
			if (enumerableValueProvider == null)
			{
				return null;
			}
			return enumerableValueProvider.GetKeysFromPrefix(prefix);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002325C File Offset: 0x0002145C
		protected override void InsertItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00023274 File Offset: 0x00021474
		protected override void SetItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
