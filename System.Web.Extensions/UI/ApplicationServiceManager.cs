using System;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000041 RID: 65
	internal static class ApplicationServiceManager
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00010EC3 File Offset: 0x0000F0C3
		public static string MergeServiceUrls(string serviceUrl, string existingUrl, Control urlBase)
		{
			serviceUrl = serviceUrl.Trim();
			if (serviceUrl.Length > 0)
			{
				serviceUrl = urlBase.ResolveClientUrl(serviceUrl);
				if (string.IsNullOrEmpty(existingUrl))
				{
					existingUrl = serviceUrl;
				}
				else if (!string.Equals(serviceUrl, existingUrl, StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(AtlasWeb.AppService_MultiplePaths);
				}
			}
			return existingUrl;
		}

		// Token: 0x040000FA RID: 250
		public const int StringBuilderCapacity = 128;
	}
}
