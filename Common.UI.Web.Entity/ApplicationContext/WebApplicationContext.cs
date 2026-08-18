using System;
using System.IO;
using System.Web.Hosting;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.UI.Web.Entity.ApplicationContext
{
	// Token: 0x0200004D RID: 77
	public class WebApplicationContext : ApplicationContext
	{
		// Token: 0x06000203 RID: 515 RVA: 0x000045F6 File Offset: 0x000027F6
		public WebApplicationContext()
		{
			base.ExecutingPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin");
		}
	}
}
