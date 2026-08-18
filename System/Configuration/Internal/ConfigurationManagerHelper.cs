using System;
using System.Net.Configuration;

namespace System.Configuration.Internal
{
	// Token: 0x0200071F RID: 1823
	internal sealed class ConfigurationManagerHelper : IConfigurationManagerHelper
	{
		// Token: 0x060037C8 RID: 14280 RVA: 0x000EC3C2 File Offset: 0x000EB3C2
		private ConfigurationManagerHelper()
		{
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000EC3CA File Offset: 0x000EB3CA
		void IConfigurationManagerHelper.EnsureNetConfigLoaded()
		{
			SettingsSection.EnsureConfigLoaded();
		}
	}
}
