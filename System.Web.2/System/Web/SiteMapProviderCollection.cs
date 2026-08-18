using System;
using System.Configuration.Provider;

namespace System.Web
{
	// Token: 0x020000F3 RID: 243
	public sealed class SiteMapProviderCollection : ProviderCollection
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x00029864 File Offset: 0x00027A64
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is SiteMapProvider))
			{
				throw new ArgumentException(SR.GetString("Provider_must_implement_the_interface", new object[]
				{
					provider.GetType().Name,
					typeof(SiteMapProvider).Name
				}), "provider");
			}
			this.Add((SiteMapProvider)provider);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000298CE File Offset: 0x00027ACE
		public void Add(SiteMapProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			base.Add(provider);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000298E8 File Offset: 0x00027AE8
		public void AddArray(SiteMapProvider[] providerArray)
		{
			if (providerArray == null)
			{
				throw new ArgumentNullException("providerArray");
			}
			foreach (SiteMapProvider siteMapProvider in providerArray)
			{
				if (this[siteMapProvider.Name] != null)
				{
					throw new ArgumentException(SR.GetString("SiteMapProvider_Multiple_Providers_With_Identical_Name", new object[]
					{
						siteMapProvider.Name
					}));
				}
				this.Add(siteMapProvider);
			}
		}

		// Token: 0x17000506 RID: 1286
		public SiteMapProvider this[string name]
		{
			get
			{
				return (SiteMapProvider)base[name];
			}
		}
	}
}
