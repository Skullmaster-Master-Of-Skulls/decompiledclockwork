using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Web.Compilation;
using System.Web.Hosting;
using Microsoft.Win32;

namespace System.Web.Configuration
{
	// Token: 0x02000751 RID: 1873
	internal static class ServerConfig
	{
		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x06005A3C RID: 23100 RVA: 0x0013A817 File Offset: 0x00138A17
		// (set) Token: 0x06005A3D RID: 23101 RVA: 0x0013A81E File Offset: 0x00138A1E
		internal static string IISExpressVersion
		{
			get
			{
				return ServerConfig.s_iisExpressVersion;
			}
			set
			{
				if (Thread.GetDomain().IsDefaultAppDomain() || (ServerConfig.s_iisExpressVersion != null && ServerConfig.s_iisExpressVersion != value))
				{
					throw new InvalidOperationException();
				}
				ServerConfig.s_iisExpressVersion = value;
			}
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x06005A3E RID: 23102 RVA: 0x0013A84C File Offset: 0x00138A4C
		internal static bool UseMetabase
		{
			get
			{
				if (ServerConfig.IISExpressVersion != null || HostingEnvironment.IsUnderIISExpressProcess)
				{
					return false;
				}
				if (ServerConfig.s_iisMajorVersion == 0)
				{
					int value2;
					try
					{
						new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\InetStp").Assert();
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

		// Token: 0x06005A3F RID: 23103 RVA: 0x0013A8D4 File Offset: 0x00138AD4
		internal static IServerConfig GetInstance()
		{
			if (ServerConfig.UseMetabase)
			{
				return MetabaseServerConfig.GetInstance();
			}
			if (ServerConfig.IISExpressVersion == null)
			{
				return ProcessHostServerConfig.GetInstance();
			}
			return ExpressServerConfig.GetInstance(ServerConfig.IISExpressVersion);
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x0013A8FC File Offset: 0x00138AFC
		internal static IServerConfig GetDefaultDomainInstance(string version)
		{
			if (version == null)
			{
				return ServerConfig.GetInstance();
			}
			ExpressServerConfig expressServerConfig = null;
			object obj = ServerConfig.s_expressConfigsLock;
			lock (obj)
			{
				if (ServerConfig.s_expressConfigs == null)
				{
					if (!Thread.GetDomain().IsDefaultAppDomain())
					{
						throw new InvalidOperationException();
					}
					ServerConfig.s_expressConfigs = new Dictionary<string, ExpressServerConfig>(3);
				}
				if (!ServerConfig.s_expressConfigs.TryGetValue(version, out expressServerConfig))
				{
					expressServerConfig = new ExpressServerConfig(version);
					ServerConfig.s_expressConfigs[version] = expressServerConfig;
				}
			}
			return expressServerConfig;
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x06005A41 RID: 23105 RVA: 0x0013A988 File Offset: 0x00138B88
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
					else if (HostingEnvironment.ApplicationHostInternal is ISAPIApplicationHost)
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

		// Token: 0x04002FCE RID: 12238
		private static int s_iisMajorVersion = 0;

		// Token: 0x04002FCF RID: 12239
		private static object s_expressConfigsLock = new object();

		// Token: 0x04002FD0 RID: 12240
		private static Dictionary<string, ExpressServerConfig> s_expressConfigs;

		// Token: 0x04002FD1 RID: 12241
		private static string s_iisExpressVersion;

		// Token: 0x04002FD2 RID: 12242
		private static int s_useServerConfig = -1;
	}
}
