using System;
using System.Collections.Specialized;
using System.Configuration;

namespace NLog.Internal
{
	// Token: 0x02000079 RID: 121
	public class ConfigurationManager : IConfigurationManager
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00009195 File Offset: 0x00007395
		public NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}
	}
}
