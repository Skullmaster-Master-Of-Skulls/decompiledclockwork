using System;
using System.Web.Hosting;

namespace System.Web.WebPages.Razor
{
	// Token: 0x0200000A RID: 10
	internal sealed class HostingEnvironmentWrapper : IHostingEnvironment
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002453 File Offset: 0x00000653
		public string MapPath(string virtualPath)
		{
			return HostingEnvironment.MapPath(virtualPath);
		}
	}
}
