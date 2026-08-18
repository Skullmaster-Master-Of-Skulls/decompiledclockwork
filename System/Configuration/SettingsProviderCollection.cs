using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	// Token: 0x0200071B RID: 1819
	public class SettingsProviderCollection : ProviderCollection
	{
		// Token: 0x060037C2 RID: 14274 RVA: 0x000EC2A8 File Offset: 0x000EB2A8
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is SettingsProvider))
			{
				throw new ArgumentException(SR.GetString("Config_provider_must_implement_type", new object[]
				{
					typeof(SettingsProvider).ToString()
				}), "provider");
			}
			base.Add(provider);
		}

		// Token: 0x17000CF3 RID: 3315
		public SettingsProvider this[string name]
		{
			get
			{
				return (SettingsProvider)base[name];
			}
		}
	}
}
