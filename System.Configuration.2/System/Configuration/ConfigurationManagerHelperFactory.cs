using System;
using System.Configuration.Internal;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200002F RID: 47
	internal static class ConfigurationManagerHelperFactory
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0001082C File Offset: 0x0000EA2C
		internal static IConfigurationManagerHelper Instance
		{
			get
			{
				if (ConfigurationManagerHelperFactory.s_instance == null)
				{
					ConfigurationManagerHelperFactory.s_instance = ConfigurationManagerHelperFactory.CreateConfigurationManagerHelper();
				}
				return ConfigurationManagerHelperFactory.s_instance;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0001084A File Offset: 0x0000EA4A
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		private static IConfigurationManagerHelper CreateConfigurationManagerHelper()
		{
			return TypeUtil.CreateInstance<IConfigurationManagerHelper>("System.Configuration.Internal.ConfigurationManagerHelper, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
		}

		// Token: 0x040001E1 RID: 481
		private const string ConfigurationManagerHelperTypeString = "System.Configuration.Internal.ConfigurationManagerHelper, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040001E2 RID: 482
		private static volatile IConfigurationManagerHelper s_instance;
	}
}
