using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006C6 RID: 1734
	internal class ConfigsHelper
	{
		// Token: 0x060053C2 RID: 21442 RVA: 0x001265E8 File Offset: 0x001247E8
		internal static void GetRegistryStringAttribute(ref string val, ConfigurationElement config, string propName)
		{
			if (!HandlerBase.CheckAndReadRegistryValue(ref val, false))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_registry_config"), config.ElementInformation.Properties[propName].Source, config.ElementInformation.Properties[propName].LineNumber);
			}
		}
	}
}
