using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200088D RID: 2189
	public static class HttpRequestMessageExtensionMethods
	{
		// Token: 0x06005322 RID: 21282 RVA: 0x001324DA File Offset: 0x001306DA
		public static void SetUserPrincipal(this HttpRequestMessage httpRequestMessage, IPrincipal user)
		{
			if (httpRequestMessage == null)
			{
				throw FxTrace.Exception.AsError(new ArgumentNullException("httpRequestMessage"));
			}
			httpRequestMessage.Properties["MS_UserPrincipal"] = user;
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x00132508 File Offset: 0x00130708
		public static IPrincipal GetUserPrincipal(this HttpRequestMessage httpRequestMessage)
		{
			if (httpRequestMessage == null)
			{
				throw FxTrace.Exception.AsError(new ArgumentNullException("httpRequestMessage"));
			}
			object obj;
			if (httpRequestMessage.Properties.TryGetValue("MS_UserPrincipal", out obj))
			{
				return obj as IPrincipal;
			}
			return null;
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x0013254C File Offset: 0x0013074C
		internal static void AddHeader(this HttpRequestMessage httpRequestMessage, string header, string value)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpRequestMessageExtensionMethods.EnsureNotResponseHeader(headerInfo);
			HttpRequestMessageExtensionMethods.AddHeader(httpRequestMessage, headerInfo, value);
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x00132570 File Offset: 0x00130770
		internal static void SetHeader(this HttpRequestMessage httpRequestMessage, string header, string value)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpRequestMessageExtensionMethods.EnsureNotResponseHeader(headerInfo);
			HttpRequestMessageExtensionMethods.RemoveHeader(httpRequestMessage, headerInfo);
			HttpRequestMessageExtensionMethods.AddHeader(httpRequestMessage, headerInfo, value);
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x0013259C File Offset: 0x0013079C
		internal static IEnumerable<string> GetHeader(this HttpRequestMessage httpRequestMessage, string header)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpRequestMessageExtensionMethods.EnsureNotResponseHeader(headerInfo);
			return HttpRequestMessageExtensionMethods.GetHeader(httpRequestMessage, headerInfo);
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x001325C0 File Offset: 0x001307C0
		internal static void RemoveHeader(this HttpRequestMessage httpRequestMessage, string header)
		{
			HttpHeaderInfo headerInfo = HttpHeaderInfo.Create(header);
			HttpRequestMessageExtensionMethods.EnsureNotResponseHeader(headerInfo);
			HttpRequestMessageExtensionMethods.RemoveHeader(httpRequestMessage, headerInfo);
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x001325E4 File Offset: 0x001307E4
		internal static HttpRequestMessage CreateBufferedCopy(this HttpRequestMessage httpRequestMessage)
		{
			HttpRequestMessage httpRequestMessage2 = new HttpRequestMessage();
			httpRequestMessage2.RequestUri = ((httpRequestMessage.RequestUri != null) ? new Uri(httpRequestMessage.RequestUri, string.Empty) : null);
			httpRequestMessage2.Method = ((httpRequestMessage.Method != null) ? new HttpMethod(httpRequestMessage.Method.Method) : null);
			httpRequestMessage2.Version = (Version)((httpRequestMessage.Version != null) ? httpRequestMessage.Version.Clone() : null);
			foreach (KeyValuePair<string, IEnumerable<string>> header in httpRequestMessage.Headers)
			{
				httpRequestMessage2.Headers.AddHeaderWithoutValidation(header);
			}
			foreach (KeyValuePair<string, object> keyValuePair in httpRequestMessage.Properties)
			{
				IMessageProperty messageProperty = keyValuePair.Value as IMessageProperty;
				object value = (messageProperty != null) ? messageProperty.CreateCopy() : keyValuePair.Value;
				httpRequestMessage2.Properties.Add(keyValuePair.Key, value);
			}
			httpRequestMessage2.Content = HttpRequestMessageExtensionMethods.CreateBufferedCopyOfContent(httpRequestMessage.Content);
			return httpRequestMessage2;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x00132734 File Offset: 0x00130934
		internal static HttpContent CreateBufferedCopyOfContent(HttpContent content)
		{
			if (content != null)
			{
				HttpRequestMessageExtensionMethods.SharedByteArrayContent sharedByteArrayContent = content as HttpRequestMessageExtensionMethods.SharedByteArrayContent;
				byte[] content2 = (sharedByteArrayContent == null) ? content.ReadAsByteArrayAsync().Result : sharedByteArrayContent.ContentBytes;
				HttpContent httpContent = new HttpRequestMessageExtensionMethods.SharedByteArrayContent(content2);
				foreach (KeyValuePair<string, IEnumerable<string>> header in content.Headers)
				{
					httpContent.Headers.AddHeaderWithoutValidation(header);
				}
				return httpContent;
			}
			return null;
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x001327B4 File Offset: 0x001309B4
		internal static void CopyPropertiesFromMessage(this HttpRequestMessage httpRequestMessage, Message message)
		{
			IDictionary<string, object> properties = httpRequestMessage.Properties;
			HttpRequestMessageExtensionMethods.CopyProperties(message.Properties, properties);
			properties["System.ServiceModel.Channels.MessageHeaders"] = message.Headers;
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x001327E8 File Offset: 0x001309E8
		internal static void AddHeaderWithoutValidation(this HttpHeaders httpHeaders, KeyValuePair<string, IEnumerable<string>> header)
		{
			if (!httpHeaders.TryAddWithoutValidation(header.Key, header.Value))
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("CopyHttpHeaderFailed", new object[]
				{
					header.Key,
					header.Value,
					httpHeaders.GetType().Name
				})));
			}
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x00132850 File Offset: 0x00130A50
		private static void CopyProperties(MessageProperties messageProperties, IDictionary<string, object> properties)
		{
			foreach (KeyValuePair<string, object> keyValuePair in ((IEnumerable<KeyValuePair<string, object>>)messageProperties))
			{
				object value = keyValuePair.Value;
				string key = keyValuePair.Key;
				if ((!(value is HttpRequestMessageProperty) || !string.Equals(key, HttpRequestMessageProperty.Name, StringComparison.OrdinalIgnoreCase)) && (!(value is HttpResponseMessageProperty) || !string.Equals(key, HttpResponseMessageProperty.Name, StringComparison.OrdinalIgnoreCase)))
				{
					properties[key] = value;
				}
			}
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x001328D8 File Offset: 0x00130AD8
		private static void EnsureNotResponseHeader(HttpHeaderInfo headerInfo)
		{
			if (!headerInfo.IsRequestHeader && !headerInfo.IsContentHeader && headerInfo.IsResponseHeader)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("ResponseHeaderWithRequestHeadersCollection", new object[]
				{
					headerInfo.Name
				})));
			}
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00132928 File Offset: 0x00130B28
		private static IEnumerable<string> GetHeader(HttpRequestMessage httpRequestMessage, HttpHeaderInfo headerInfo)
		{
			IEnumerable<string> enumerable = null;
			if (headerInfo.IsRequestHeader)
			{
				enumerable = headerInfo.TryGetHeader(httpRequestMessage.Headers);
			}
			if (enumerable == null && headerInfo.IsContentHeader && httpRequestMessage.Content != null)
			{
				enumerable = headerInfo.TryGetHeader(httpRequestMessage.Content.Headers);
			}
			return enumerable;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00132972 File Offset: 0x00130B72
		private static void RemoveHeader(HttpRequestMessage httpRequestMessage, HttpHeaderInfo headerInfo)
		{
			if (headerInfo.IsRequestHeader)
			{
				headerInfo.TryRemoveHeader(httpRequestMessage.Headers);
			}
			if (headerInfo.IsContentHeader && httpRequestMessage.Content != null)
			{
				headerInfo.TryRemoveHeader(httpRequestMessage.Content.Headers);
			}
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x001329AB File Offset: 0x00130BAB
		private static void AddHeader(HttpRequestMessage httpRequestMessage, HttpHeaderInfo headerInfo, string value)
		{
			if (headerInfo.IsRequestHeader && headerInfo.TryAddHeader(httpRequestMessage.Headers, value))
			{
				return;
			}
			if (headerInfo.IsContentHeader)
			{
				HttpRequestMessageExtensionMethods.CreateContentIfNull(httpRequestMessage);
				headerInfo.TryAddHeader(httpRequestMessage.Content.Headers, value);
			}
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x001329E7 File Offset: 0x00130BE7
		private static bool CreateContentIfNull(HttpRequestMessage httpRequestMessage)
		{
			if (httpRequestMessage.Content == null)
			{
				httpRequestMessage.Content = new ByteArrayContent(EmptyArray<byte>.Instance);
				return true;
			}
			return false;
		}

		// Token: 0x040032B3 RID: 12979
		private const string MessageHeadersPropertyKey = "System.ServiceModel.Channels.MessageHeaders";

		// Token: 0x040032B4 RID: 12980
		private const string PrincipalKey = "MS_UserPrincipal";

		// Token: 0x02000D6B RID: 3435
		private class SharedByteArrayContent : ByteArrayContent
		{
			// Token: 0x06007DD3 RID: 32211 RVA: 0x001D65DD File Offset: 0x001D47DD
			public SharedByteArrayContent(byte[] content) : base(content)
			{
				this.ContentBytes = content;
			}

			// Token: 0x17001C16 RID: 7190
			// (get) Token: 0x06007DD4 RID: 32212 RVA: 0x001D65ED File Offset: 0x001D47ED
			// (set) Token: 0x06007DD5 RID: 32213 RVA: 0x001D65F5 File Offset: 0x001D47F5
			public byte[] ContentBytes { get; private set; }
		}
	}
}
