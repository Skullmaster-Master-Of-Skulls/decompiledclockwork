using System;
using System.Threading;
using System.Web.Compilation;
using System.Web.Hosting;
using Microsoft.Win32;

namespace System.Web.Configuration
{
	// Token: 0x02000247 RID: 583
	internal static class ServerConfig
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00089C10 File Offset: 0x00088C10
		internal static bool UseMetabase
		{
			get
			{
				if (ServerConfig.s_iisMajorVersion == 0)
				{
					int value2;
					try
					{
						object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\InetStp", "MajorVersion", 0);
						value2 = ((value != null) ? ((int)value) : -1);
					}
					catch (ArgumentException)
					{
						value2 = -1;
					}
					Interlocked.CompareExchange(ref ServerConfig.s_iisMajorVersion, value2, 0);
				}
				return ServerConfig.s_iisMajorVersion <= 6;
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00089C78 File Offset: 0x00088C78
		internal static IServerConfig GetInstance()
		{
			if (ServerConfig.UseMetabase)
			{
				return MetabaseServerConfig.GetInstance();
			}
			return ProcessHostServerConfig.GetInstance();
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00089C8C File Offset: 0x00088C8C
		internal static bool UseServerConfig
		{
			get
			{
				if (ServerConfig.s_useServerConfig == -1)
				{
					int value = 0;
					if (!HostingEnvironment.IsHosted)
					{
						value = 1;
					}
					else if (HostingEnvironment.ApplicationHost is ISAPIApplicationHost)
					{
						value = 1;
					}
					else if (HostingEnvironment.IsUnderIISProcess && !BuildManagerHost.InClientBuildManager)
					{
						value = 1;
					}
					Interlocked.CompareExchange(ref ServerConfig.s_useServerConfig, value, -1);
				}
				return ServerConfig.s_useServerConfig == 1;
			}
		}

		// Token: 0x04001A1D RID: 6685
		private static int s_iisMajorVersion = 0;

		// Token: 0x04001A1E RID: 6686
		private static int s_useServerConfig = -1;
	}
}
