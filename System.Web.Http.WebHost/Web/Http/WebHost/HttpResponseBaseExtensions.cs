using System;
using System.Reflection;
using System.Threading;

namespace System.Web.Http.WebHost
{
	// Token: 0x0200000A RID: 10
	internal static class HttpResponseBaseExtensions
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002C98 File Offset: 0x00000E98
		public static CancellationToken GetClientDisconnectedTokenWhenFixed(this HttpResponseBase response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (!HttpResponseBaseExtensions._isClientDisconnectedTokenAvailable || !HttpResponseBaseExtensions._isSystemWebVersion451OrGreater)
			{
				return CancellationToken.None;
			}
			return response.ClientDisconnectedToken;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CC4 File Offset: 0x00000EC4
		private static bool IsClientDisconnectedTokenAvailable()
		{
			Version v = new Version(7, 5);
			Version iisversion = HttpRuntime.IISVersion;
			return iisversion != null && iisversion >= v && HttpRuntime.UsingIntegratedPipeline;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CF8 File Offset: 0x00000EF8
		private static bool IsSystemWebVersion451OrGreater()
		{
			Assembly assembly = typeof(HttpContextBase).Assembly;
			return assembly.GetType("System.Web.AspNetEventSource") != null;
		}

		// Token: 0x04000008 RID: 8
		private static readonly bool _isSystemWebVersion451OrGreater = HttpResponseBaseExtensions.IsSystemWebVersion451OrGreater();

		// Token: 0x04000009 RID: 9
		private static readonly bool _isClientDisconnectedTokenAvailable = HttpResponseBaseExtensions.IsClientDisconnectedTokenAvailable();
	}
}
