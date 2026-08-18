using System;
using log4net.Util;

namespace log4net
{
	// Token: 0x02000121 RID: 289
	public sealed class GlobalContext
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x00019FBE File Offset: 0x000181BE
		private GlobalContext()
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00019FC6 File Offset: 0x000181C6
		static GlobalContext()
		{
			GlobalContext.Properties["log4net:HostName"] = SystemInfo.HostName;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00019FE6 File Offset: 0x000181E6
		public static GlobalContextProperties Properties
		{
			get
			{
				return GlobalContext.s_properties;
			}
		}

		// Token: 0x04000313 RID: 787
		private static readonly GlobalContextProperties s_properties = new GlobalContextProperties();
	}
}
