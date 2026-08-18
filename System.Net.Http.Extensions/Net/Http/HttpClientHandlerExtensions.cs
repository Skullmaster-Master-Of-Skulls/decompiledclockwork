using System;

namespace System.Net.Http
{
	// Token: 0x02000002 RID: 2
	public static class HttpClientHandlerExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static bool SupportsAllowAutoRedirect(this HttpClientHandler handler)
		{
			return true;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D3 File Offset: 0x000002D3
		public static bool SupportsPreAuthenticate(this HttpClientHandler handler)
		{
			return true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D6 File Offset: 0x000002D6
		public static bool SupportsProtocolVersion(this HttpClientHandler handler)
		{
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D9 File Offset: 0x000002D9
		public static bool SupportsTransferEncodingChunked(this HttpClientHandler handler)
		{
			return true;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		public static bool SupportsUseProxy(this HttpClientHandler handler)
		{
			return true;
		}
	}
}
