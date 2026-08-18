using System;
using System.Configuration.Provider;

namespace System.Web.Caching
{
	// Token: 0x0200088B RID: 2187
	public sealed class OutputCacheProviderCollection : ProviderCollection
	{
		// Token: 0x17001CCB RID: 7371
		public OutputCacheProvider this[string name]
		{
			get
			{
				return (OutputCacheProvider)base[name];
			}
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x0016A86C File Offset: 0x00168A6C
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is OutputCacheProvider))
			{
				throw new ArgumentException(SR.GetString("Provider_must_implement_type", new object[]
				{
					typeof(OutputCacheProvider).Name
				}), "provider");
			}
			base.Add(provider);
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x0016A8C4 File Offset: 0x00168AC4
		public void CopyTo(OutputCacheProvider[] array, int index)
		{
			base.CopyTo(array, index);
		}
	}
}
