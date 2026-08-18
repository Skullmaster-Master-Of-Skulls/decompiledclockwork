using System;
using System.Net.Http;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x02000016 RID: 22
	internal static class HttpContextBaseExtensions
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003D84 File Offset: 0x00001F84
		public static HttpRequestMessage GetHttpRequestMessage(this HttpContextBase context)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			if (context.Items == null || !context.Items.Contains(HttpContextBaseExtensions.HttpRequestMessageKey))
			{
				return null;
			}
			return context.Items[HttpContextBaseExtensions.HttpRequestMessageKey] as HttpRequestMessage;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public static void SetHttpRequestMessage(this HttpContextBase context, HttpRequestMessage request)
		{
			if (context.Items != null)
			{
				context.Items.Add(HttpContextBaseExtensions.HttpRequestMessageKey, request);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003DEC File Offset: 0x00001FEC
		public static HttpRequestMessage GetOrCreateHttpRequestMessage(this HttpContextBase context)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			HttpRequestMessage httpRequestMessage = context.GetHttpRequestMessage();
			if (httpRequestMessage == null)
			{
				httpRequestMessage = HttpControllerHandler.ConvertRequest(context);
				context.SetHttpRequestMessage(httpRequestMessage);
			}
			return httpRequestMessage;
		}

		// Token: 0x04000024 RID: 36
		internal static readonly string HttpRequestMessageKey = "MS_HttpRequestMessage";
	}
}
