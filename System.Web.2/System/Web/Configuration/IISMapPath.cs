using System;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x0200070B RID: 1803
	internal static class IISMapPath
	{
		// Token: 0x06005702 RID: 22274 RVA: 0x00130224 File Offset: 0x0012E424
		internal static IConfigMapPath GetInstance()
		{
			if (ServerConfig.UseMetabase)
			{
				return (IConfigMapPath)MetabaseServerConfig.GetInstance();
			}
			if (ServerConfig.IISExpressVersion != null)
			{
				return (IConfigMapPath)ServerConfig.GetInstance();
			}
			ProcessHost defaultHost = ProcessHost.DefaultHost;
			IProcessHostSupportFunctions processHostSupportFunctions = null;
			if (defaultHost != null)
			{
				processHostSupportFunctions = defaultHost.SupportFunctions;
			}
			if (processHostSupportFunctions == null)
			{
				processHostSupportFunctions = HostingEnvironment.SupportFunctions;
			}
			return new ProcessHostMapPath(processHostSupportFunctions);
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x00130278 File Offset: 0x0012E478
		internal static bool IsSiteId(string siteName)
		{
			if (string.IsNullOrEmpty(siteName))
			{
				return false;
			}
			for (int i = 0; i < siteName.Length; i++)
			{
				if (!char.IsDigit(siteName[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
