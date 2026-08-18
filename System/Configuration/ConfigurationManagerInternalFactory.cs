using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x020006ED RID: 1773
	internal static class ConfigurationManagerInternalFactory
	{
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x000E9EA3 File Offset: 0x000E8EA3
		internal static IConfigurationManagerInternal Instance
		{
			get
			{
				if (ConfigurationManagerInternalFactory.s_instance == null)
				{
					ConfigurationManagerInternalFactory.s_instance = (IConfigurationManagerInternal)TypeUtil.CreateInstanceWithReflectionPermission("System.Configuration.Internal.ConfigurationManagerInternal, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				}
				return ConfigurationManagerInternalFactory.s_instance;
			}
		}

		// Token: 0x040031AE RID: 12718
		private const string ConfigurationManagerInternalTypeString = "System.Configuration.Internal.ConfigurationManagerInternal, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x040031AF RID: 12719
		private static IConfigurationManagerInternal s_instance;
	}
}
