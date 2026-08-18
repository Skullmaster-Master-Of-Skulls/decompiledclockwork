using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	// Token: 0x0200007D RID: 125
	public class ProtectedConfigurationProviderCollection : ProviderCollection
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00019538 File Offset: 0x00017738
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is ProtectedConfigurationProvider))
			{
				throw new ArgumentException(SR.GetString("Config_provider_must_implement_type", new object[]
				{
					typeof(ProtectedConfigurationProvider).ToString()
				}), "provider");
			}
			base.Add(provider);
		}

		// Token: 0x17000166 RID: 358
		public ProtectedConfigurationProvider this[string name]
		{
			get
			{
				return (ProtectedConfigurationProvider)base[name];
			}
		}
	}
}
