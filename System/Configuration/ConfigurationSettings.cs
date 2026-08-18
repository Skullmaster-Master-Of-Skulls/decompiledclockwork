using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x020006EE RID: 1774
	public sealed class ConfigurationSettings
	{
		// Token: 0x060036E2 RID: 14050 RVA: 0x000E9EC5 File Offset: 0x000E8EC5
		private ConfigurationSettings()
		{
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000E9ECD File Offset: 0x000E8ECD
		[Obsolete("This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.AppSettings")]
		public static NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000E9ED4 File Offset: 0x000E8ED4
		[Obsolete("This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.GetSection")]
		public static object GetConfig(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
