using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200088E RID: 2190
	internal static class HttpResponseMessageExtensionMethods
	{
		// Token: 0x06005332 RID: 21298 RVA: 0x00132A04 File Offset: 0x00130C04
		internal static void AddHeader(this HttpResponseMessage httpResponseMessage, string header, string value)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpResponseMessageExtensionMethods.EnsureNotRequestHeader(headerInfo);
			HttpResponseMessageExtensionMethods.AddHeader(httpResponseMessage, headerInfo, value);
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x00132A28 File Offset: 0x00130C28
		internal static void SetHeader(this HttpResponseMessage httpResponseMessage, string header, string value)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpResponseMessageExtensionMethods.EnsureNotRequestHeader(headerInfo);
			HttpResponseMessageExtensionMethods.RemoveHeader(httpResponseMessage, headerInfo);
			HttpResponseMessageExtensionMethods.AddHeader(httpResponseMessage, headerInfo, value);
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x00132A54 File Offset: 0x00130C54
		internal static IEnumerable<string> GetHeader(this HttpResponseMessage httpResponseMessage, string header)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpResponseMessageExtensionMethods.EnsureNotRequestHeader(headerInfo);
			return HttpResponseMessageExtensionMethods.GetHeader(httpResponseMessage, headerInfo);
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x00132A78 File Offset: 0x00130C78
		internal static void RemoveHeader(this HttpResponseMessage httpResponseMessage, string header)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpResponseMessageExtensionMethods.EnsureNotRequestHeader(headerInfo);
			HttpResponseMessageExtensionMethods.RemoveHeader(httpResponseMessage, headerInfo);
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x00132A9C File Offset: 0x00130C9C
		internal static HttpResponseMessage CreateBufferedCopy(this HttpResponseMessage httpResponseMessage)
		{
			HttpResponseMessage httpResponseMessage2 = new HttpResponseMessage();
			httpResponseMessage2.ReasonPhrase = httpResponseMessage.ReasonPhrase;
			httpResponseMessage2.StatusCode = httpResponseMessage.StatusCode;
			httpResponseMessage2.Version = (Version)((httpResponseMessage.Version != null) ? httpResponseMessage.Version.Clone() : null);
			if (httpResponseMessage.RequestMessage != null)
			{
				httpResponseMessage2.RequestMessage = httpResponseMessage.RequestMessage.CreateBufferedCopy();
			}
			foreach (KeyValuePair<string, IEnumerable<string>> header in httpResponseMessage.Headers)
			{
				httpResponseMessage2.Headers.AddHeaderWithoutValidation(header);
			}
			httpResponseMessage2.Content = HttpRequestMessageExtensionMethods.CreateBufferedCopyOfContent(httpResponseMessage.Content);
			return httpResponseMessage2;
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x00132B60 File Offset: 0x00130D60
		internal static void CopyPropertiesFromMessage(this HttpResponseMessage httpResponseMessage, Message message)
		{
			HttpRequestMessage requestMessage = httpResponseMessage.RequestMessage;
			if (requestMessage != null)
			{
				requestMessage.CopyPropertiesFromMessage(message);
			}
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x00132B80 File Offset: 0x00130D80
		private static void EnsureNotRequestHeader(HttpHeaderInfo headerInfo)
		{
			if (!headerInfo.IsResponseHeader && !headerInfo.IsContentHeader && headerInfo.IsResponseHeader)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("RequestHeaderWithResponseHeadersCollection", new object[]
				{
					headerInfo.Name
				})));
			}
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x00132BD0 File Offset: 0x00130DD0
		private static IEnumerable<string> GetHeader(HttpResponseMessage httpResponseMessage, HttpHeaderInfo headerInfo)
		{
			IEnumerable<string> enumerable = null;
			if (headerInfo.IsResponseHeader)
			{
				enumerable = headerInfo.TryGetHeader(httpResponseMessage.Headers);
			}
			if (enumerable == null && headerInfo.IsContentHeader && httpResponseMessage.Content != null)
			{
				enumerable = headerInfo.TryGetHeader(httpResponseMessage.Content.Headers);
			}
			return enumerable;
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x00132C1A File Offset: 0x00130E1A
		private static void RemoveHeader(HttpResponseMessage httpResponseMessage, HttpHeaderInfo headerInfo)
		{
			if (headerInfo.IsResponseHeader)
			{
				headerInfo.TryRemoveHeader(httpResponseMessage.Headers);
			}
			if (headerInfo.IsContentHeader && httpResponseMessage.Content != null)
			{
				headerInfo.TryRemoveHeader(httpResponseMessage.Content.Headers);
			}
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x00132C53 File Offset: 0x00130E53
		private static void AddHeader(HttpResponseMessage httpResponseMessage, HttpHeaderInfo headerInfo, string value)
		{
			if (headerInfo.IsResponseHeader && headerInfo.TryAddHeader(httpResponseMessage.Headers, value))
			{
				return;
			}
			if (headerInfo.IsContentHeader)
			{
				HttpResponseMessageExtensionMethods.CreateContentIfNull(httpResponseMessage);
				headerInfo.TryAddHeader(httpResponseMessage.Content.Headers, value);
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00132C8F File Offset: 0x00130E8F
		private static bool CreateContentIfNull(HttpResponseMessage httpResponseMessage)
		{
			if (httpResponseMessage.Content == null)
			{
				httpResponseMessage.Content = new ByteArrayContent(EmptyArray<byte>.Instance);
				return true;
			}
			return false;
		}
	}
}
