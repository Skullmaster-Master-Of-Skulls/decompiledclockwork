using System;
using System.Net;
using System.Net.Http;

namespace Google.Apis.Http
{
	// Token: 0x0200002B RID: 43
	public static class HttpExtenstions
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00004218 File Offset: 0x00002418
		internal static bool IsRedirectStatusCode(this HttpResponseMessage message)
		{
			switch (message.StatusCode)
			{
			case HttpStatusCode.MovedPermanently:
			case HttpStatusCode.Found:
			case HttpStatusCode.SeeOther:
			case HttpStatusCode.TemporaryRedirect:
				return true;
			}
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004259 File Offset: 0x00002459
		public static HttpContent SetEmptyContent(this HttpRequestMessage request)
		{
			request.Content = new ByteArrayContent(new byte[0]);
			request.Content.Headers.ContentLength = new long?(0L);
			return request.Content;
		}
	}
}
