using System;
using System.IO;
using System.Web.Hosting;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkWeb.Infrastructure
{
	// Token: 0x02000112 RID: 274
	public static class CacheHelper
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x0003A95C File Offset: 0x00038B5C
		public static ApplicationContext GetApplicationContext()
		{
			ApplicationContext applicationContext = ClientCache.CurrentInstance.ApplicationContext;
			bool flag = applicationContext == null;
			if (flag)
			{
				applicationContext = new ApplicationContext
				{
					ExecutingPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin")
				};
			}
			return applicationContext;
		}
	}
}
