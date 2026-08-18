using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;

namespace System.Runtime.Remoting.Channels.Http
{
	// Token: 0x0200002A RID: 42
	internal class HttpClientTransportSink : BaseChannelSinkWithProperties, IClientChannelSink, IChannelSinkBase
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000067D0 File Offset: 0x000057D0
		internal HttpClientTransportSink(HttpClientChannel channel, string channelURI)
		{
			this._channel = channel;
			this._channelURI = channelURI;
			if (this._channelURI.EndsWith("/", StringComparison.Ordinal))
			{
				this._channelURI = this._channelURI.Substring(0, this._channelURI.Length - 1);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006838 File Offset: 0x00005838
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			HttpWebRequest httpWebRequest = this.ProcessAndSend(msg, requestHeaders, requestStream);
			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)httpWebRequest.GetResponse();
			}
			catch (WebException webException)
			{
				HttpClientTransportSink.ProcessResponseException(webException, out response);
			}
			this.ReceiveAndProcess(response, out responseHeaders, out responseStream);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006888 File Offset: 0x00005888
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			HttpClientTransportSink.AsyncHttpClientRequestState asyncHttpClientRequestState = new HttpClientTransportSink.AsyncHttpClientRequestState(this, sinkStack, msg, headers, stream, 1);
			asyncHttpClientRequestState.StartRequest();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000068A8 File Offset: 0x000058A8
		private static void ProcessResponseException(WebException webException, out HttpWebResponse response)
		{
			if (webException.Status == WebExceptionStatus.Timeout)
			{
				throw new RemotingTimeoutException(CoreChannel.GetResourceString("Remoting_Channels_RequestTimedOut"), webException);
			}
			response = (webException.Response as HttpWebResponse);
			if (response == null)
			{
				throw webException;
			}
			int statusCode = (int)response.StatusCode;
			if (statusCode < 500 || statusCode > 599)
			{
				throw webException;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000068FD File Offset: 0x000058FD
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000068FF File Offset: 0x000058FF
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006902 File Offset: 0x00005902
		public IClientChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006908 File Offset: 0x00005908
		private HttpWebRequest SetupWebRequest(IMessage msg, ITransportHeaders headers)
		{
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			string text = (string)headers["__RequestUri"];
			if (text == null)
			{
				if (methodCallMessage != null)
				{
					text = methodCallMessage.Uri;
				}
				else
				{
					text = (string)msg.Properties["__Uri"];
				}
			}
			string requestUriString;
			if (HttpChannelHelper.StartsWithHttp(text) != -1)
			{
				requestUriString = text;
			}
			else
			{
				if (!text.StartsWith("/", StringComparison.Ordinal))
				{
					text = "/" + text;
				}
				requestUriString = this._channelURI + text;
			}
			string text2 = (string)headers["__RequestVerb"];
			if (text2 == null)
			{
				text2 = "POST";
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
			httpWebRequest.AllowAutoRedirect = this._bAllowAutoRedirect;
			httpWebRequest.Method = text2;
			httpWebRequest.SendChunked = this._useChunked;
			httpWebRequest.KeepAlive = this._useKeepAlive;
			httpWebRequest.Pipelined = false;
			httpWebRequest.UserAgent = HttpClientTransportSink.s_userAgent;
			httpWebRequest.Timeout = this._timeout;
			httpWebRequest.CachePolicy = HttpClientTransportSink.s_requestCachePolicy;
			IWebProxy proxyObject = this._proxyObject;
			if (proxyObject == null)
			{
				proxyObject = this._channel.ProxyObject;
			}
			if (proxyObject != null)
			{
				httpWebRequest.Proxy = proxyObject;
			}
			if (this._credentials != null)
			{
				httpWebRequest.Credentials = this._credentials;
				httpWebRequest.PreAuthenticate = this._bSecurityPreAuthenticate;
				httpWebRequest.UnsafeAuthenticatedConnectionSharing = this._bUnsafeAuthenticatedConnectionSharing;
				if (this._connectionGroupName != null)
				{
					httpWebRequest.ConnectionGroupName = this._connectionGroupName;
				}
			}
			else if (this._securityUserName != null)
			{
				if (this._securityDomain == null)
				{
					httpWebRequest.Credentials = new NetworkCredential(this._securityUserName, this._securityPassword);
				}
				else
				{
					httpWebRequest.Credentials = new NetworkCredential(this._securityUserName, this._securityPassword, this._securityDomain);
				}
				httpWebRequest.PreAuthenticate = this._bSecurityPreAuthenticate;
				httpWebRequest.UnsafeAuthenticatedConnectionSharing = this._bUnsafeAuthenticatedConnectionSharing;
				if (this._connectionGroupName != null)
				{
					httpWebRequest.ConnectionGroupName = this._connectionGroupName;
				}
			}
			else if (this._channel.UseDefaultCredentials)
			{
				if (this._channel.UseAuthenticatedConnectionSharing)
				{
					httpWebRequest.ConnectionGroupName = CoreChannel.GetCurrentSidString();
					httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
				}
				httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
				httpWebRequest.PreAuthenticate = this._bSecurityPreAuthenticate;
			}
			if (this._certificates != null)
			{
				foreach (X509Certificate value in this._certificates)
				{
					httpWebRequest.ClientCertificates.Add(value);
				}
				httpWebRequest.PreAuthenticate = this._bSecurityPreAuthenticate;
			}
			foreach (object obj in headers)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text3 = dictionaryEntry.Key as string;
				if (text3 != null && !text3.StartsWith("__", StringComparison.Ordinal))
				{
					if (text3.Equals("Content-Type"))
					{
						httpWebRequest.ContentType = dictionaryEntry.Value.ToString();
					}
					else
					{
						httpWebRequest.Headers[text3] = dictionaryEntry.Value.ToString();
					}
				}
			}
			return httpWebRequest;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006C4C File Offset: 0x00005C4C
		private HttpWebRequest ProcessAndSend(IMessage msg, ITransportHeaders headers, Stream inputStream)
		{
			long position = 0L;
			bool flag = false;
			if (inputStream != null)
			{
				flag = inputStream.CanSeek;
				if (flag)
				{
					position = inputStream.Position;
				}
			}
			HttpWebRequest httpWebRequest = null;
			Stream stream = null;
			try
			{
				httpWebRequest = this.SetupWebRequest(msg, headers);
				if (inputStream != null)
				{
					if (!this._useChunked)
					{
						httpWebRequest.ContentLength = (long)((int)inputStream.Length);
					}
					stream = httpWebRequest.GetRequestStream();
					StreamHelper.CopyStream(inputStream, stream);
				}
			}
			catch
			{
				if (flag)
				{
					httpWebRequest = this.SetupWebRequest(msg, headers);
					if (inputStream != null)
					{
						inputStream.Position = position;
						if (!this._useChunked)
						{
							httpWebRequest.ContentLength = (long)((int)inputStream.Length);
						}
						stream = httpWebRequest.GetRequestStream();
						StreamHelper.CopyStream(inputStream, stream);
					}
				}
			}
			if (inputStream != null)
			{
				inputStream.Close();
			}
			if (stream != null)
			{
				stream.Close();
			}
			return httpWebRequest;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006D08 File Offset: 0x00005D08
		private void ReceiveAndProcess(HttpWebResponse response, out ITransportHeaders returnHeaders, out Stream returnStream)
		{
			int bufferSize;
			if (response == null)
			{
				bufferSize = 4096;
			}
			else
			{
				int num = (int)response.ContentLength;
				if (num == -1 || num == 0)
				{
					bufferSize = 4096;
				}
				else if (num <= 16000)
				{
					bufferSize = num;
				}
				else
				{
					bufferSize = 16000;
				}
			}
			returnStream = new BufferedStream(response.GetResponseStream(), bufferSize);
			returnHeaders = HttpClientTransportSink.CollectResponseHeaders(response);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006D60 File Offset: 0x00005D60
		private static ITransportHeaders CollectResponseHeaders(HttpWebResponse response)
		{
			TransportHeaders transportHeaders = new TransportHeaders();
			foreach (object obj in response.Headers)
			{
				string text = obj.ToString();
				transportHeaders[text] = response.Headers[text];
			}
			return transportHeaders;
		}

		// Token: 0x17000046 RID: 70
		public override object this[object key]
		{
			get
			{
				string text = key as string;
				if (text == null)
				{
					return null;
				}
				string key2;
				switch (key2 = text.ToLower(CultureInfo.InvariantCulture))
				{
				case "username":
					return this._securityUserName;
				case "password":
					return null;
				case "domain":
					return this._securityDomain;
				case "preauthenticate":
					return this._bSecurityPreAuthenticate;
				case "credentials":
					return this._credentials;
				case "clientcertificates":
					return null;
				case "proxyname":
					return this._proxyName;
				case "proxyport":
					return this._proxyPort;
				case "timeout":
					return this._timeout;
				case "allowautoredirect":
					return this._bAllowAutoRedirect;
				case "unsafeauthenticatedconnectionsharing":
					return this._bUnsafeAuthenticatedConnectionSharing;
				case "connectiongroupname":
					return this._connectionGroupName;
				}
				return null;
			}
			set
			{
				string text = key as string;
				if (text == null)
				{
					return;
				}
				string key2;
				switch (key2 = text.ToLower(CultureInfo.InvariantCulture))
				{
				case "username":
					this._securityUserName = (string)value;
					return;
				case "password":
					this._securityPassword = (string)value;
					return;
				case "domain":
					this._securityDomain = (string)value;
					return;
				case "preauthenticate":
					this._bSecurityPreAuthenticate = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					return;
				case "credentials":
					this._credentials = (ICredentials)value;
					return;
				case "clientcertificates":
					this._certificates = (X509CertificateCollection)value;
					return;
				case "proxyname":
					this._proxyName = (string)value;
					this.UpdateProxy();
					return;
				case "proxyport":
					this._proxyPort = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					this.UpdateProxy();
					return;
				case "timeout":
					if (value is TimeSpan)
					{
						this._timeout = (int)((TimeSpan)value).TotalMilliseconds;
						return;
					}
					this._timeout = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					return;
				case "allowautoredirect":
					this._bAllowAutoRedirect = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					return;
				case "unsafeauthenticatedconnectionsharing":
					this._bUnsafeAuthenticatedConnectionSharing = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					return;
				case "connectiongroupname":
					this._connectionGroupName = (string)value;
					break;

					return;
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000715C File Offset: 0x0000615C
		public override ICollection Keys
		{
			get
			{
				if (HttpClientTransportSink.s_keySet == null)
				{
					HttpClientTransportSink.s_keySet = new ArrayList(6)
					{
						"username",
						"password",
						"domain",
						"preauthenticate",
						"credentials",
						"clientcertificates",
						"proxyname",
						"proxyport",
						"timeout",
						"allowautoredirect",
						"unsafeauthenticatedconnectionsharing",
						"connectiongroupname"
					};
				}
				return HttpClientTransportSink.s_keySet;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007218 File Offset: 0x00006218
		private void UpdateProxy()
		{
			if (this._proxyName != null && this._proxyPort > 0)
			{
				this._proxyObject = new WebProxy(this._proxyName, this._proxyPort)
				{
					BypassProxyOnLocal = true
				};
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007256 File Offset: 0x00006256
		internal static string UserAgent
		{
			get
			{
				return HttpClientTransportSink.s_userAgent;
			}
		}

		// Token: 0x040000E9 RID: 233
		private const string s_defaultVerb = "POST";

		// Token: 0x040000EA RID: 234
		private const string UserNameKey = "username";

		// Token: 0x040000EB RID: 235
		private const string PasswordKey = "password";

		// Token: 0x040000EC RID: 236
		private const string DomainKey = "domain";

		// Token: 0x040000ED RID: 237
		private const string PreAuthenticateKey = "preauthenticate";

		// Token: 0x040000EE RID: 238
		private const string CredentialsKey = "credentials";

		// Token: 0x040000EF RID: 239
		private const string ClientCertificatesKey = "clientcertificates";

		// Token: 0x040000F0 RID: 240
		private const string ProxyNameKey = "proxyname";

		// Token: 0x040000F1 RID: 241
		private const string ProxyPortKey = "proxyport";

		// Token: 0x040000F2 RID: 242
		private const string TimeoutKey = "timeout";

		// Token: 0x040000F3 RID: 243
		private const string AllowAutoRedirectKey = "allowautoredirect";

		// Token: 0x040000F4 RID: 244
		private const string UnsafeAuthenticatedConnectionSharingKey = "unsafeauthenticatedconnectionsharing";

		// Token: 0x040000F5 RID: 245
		private const string ConnectionGroupNameKey = "connectiongroupname";

		// Token: 0x040000F6 RID: 246
		private static string s_userAgent = string.Concat(new object[]
		{
			"Mozilla/4.0+(compatible; MSIE 6.0; Windows ",
			Environment.OSVersion.Version,
			"; MS .NET Remoting; MS .NET CLR ",
			Environment.Version.ToString(),
			" )"
		});

		// Token: 0x040000F7 RID: 247
		private static ICollection s_keySet = null;

		// Token: 0x040000F8 RID: 248
		private string _securityUserName;

		// Token: 0x040000F9 RID: 249
		private string _securityPassword;

		// Token: 0x040000FA RID: 250
		private string _securityDomain;

		// Token: 0x040000FB RID: 251
		private bool _bSecurityPreAuthenticate;

		// Token: 0x040000FC RID: 252
		private bool _bUnsafeAuthenticatedConnectionSharing;

		// Token: 0x040000FD RID: 253
		private string _connectionGroupName;

		// Token: 0x040000FE RID: 254
		private ICredentials _credentials;

		// Token: 0x040000FF RID: 255
		private X509CertificateCollection _certificates;

		// Token: 0x04000100 RID: 256
		private int _timeout = -1;

		// Token: 0x04000101 RID: 257
		private bool _bAllowAutoRedirect;

		// Token: 0x04000102 RID: 258
		private IWebProxy _proxyObject;

		// Token: 0x04000103 RID: 259
		private string _proxyName;

		// Token: 0x04000104 RID: 260
		private int _proxyPort = -1;

		// Token: 0x04000105 RID: 261
		private static RequestCachePolicy s_requestCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

		// Token: 0x04000106 RID: 262
		private HttpClientChannel _channel;

		// Token: 0x04000107 RID: 263
		private string _channelURI;

		// Token: 0x04000108 RID: 264
		private bool _useChunked;

		// Token: 0x04000109 RID: 265
		private bool _useKeepAlive = true;

		// Token: 0x0200002B RID: 43
		private class AsyncHttpClientRequestState
		{
			// Token: 0x0600014F RID: 335 RVA: 0x000072C4 File Offset: 0x000062C4
			internal AsyncHttpClientRequestState(HttpClientTransportSink transportSink, IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream, int retryCount)
			{
				this._transportSink = transportSink;
				this.SinkStack = sinkStack;
				this._msg = msg;
				this._requestHeaders = headers;
				this.RequestStream = stream;
				this._retryCount = retryCount;
				if (this.RequestStream.CanSeek)
				{
					this._initialStreamPosition = this.RequestStream.Position;
				}
			}

			// Token: 0x06000150 RID: 336 RVA: 0x00007324 File Offset: 0x00006324
			internal void StartRequest()
			{
				this.WebRequest = this._transportSink.SetupWebRequest(this._msg, this._requestHeaders);
				if (!this._transportSink._useChunked)
				{
					try
					{
						this.WebRequest.ContentLength = (long)((int)this.RequestStream.Length);
					}
					catch
					{
					}
				}
				this.WebRequest.BeginGetRequestStream(HttpClientTransportSink.AsyncHttpClientRequestState.s_processGetRequestStreamCompletionCallback, this);
			}

			// Token: 0x06000151 RID: 337 RVA: 0x0000739C File Offset: 0x0000639C
			internal void RetryOrDispatchException(Exception e)
			{
				bool flag = false;
				try
				{
					if (this._retryCount > 0)
					{
						this._retryCount--;
						if (this.RequestStream.CanSeek)
						{
							this.RequestStream.Position = this._initialStreamPosition;
							this.StartRequest();
							flag = true;
						}
					}
				}
				catch
				{
				}
				if (!flag)
				{
					this.RequestStream.Close();
					this.SinkStack.DispatchException(e);
				}
			}

			// Token: 0x06000152 RID: 338 RVA: 0x00007418 File Offset: 0x00006418
			private static void ProcessGetRequestStreamCompletion(IAsyncResult iar)
			{
				HttpClientTransportSink.AsyncHttpClientRequestState asyncHttpClientRequestState = (HttpClientTransportSink.AsyncHttpClientRequestState)iar.AsyncState;
				try
				{
					HttpWebRequest webRequest = asyncHttpClientRequestState.WebRequest;
					Stream requestStream = asyncHttpClientRequestState.RequestStream;
					Stream target = webRequest.EndGetRequestStream(iar);
					StreamHelper.BeginAsyncCopyStream(requestStream, target, false, true, false, true, HttpClientTransportSink.AsyncHttpClientRequestState.s_processAsyncCopyRequestStreamCompletionCallback, asyncHttpClientRequestState);
				}
				catch (Exception e)
				{
					asyncHttpClientRequestState.RetryOrDispatchException(e);
				}
				catch
				{
					asyncHttpClientRequestState.RetryOrDispatchException(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")));
				}
			}

			// Token: 0x06000153 RID: 339 RVA: 0x0000749C File Offset: 0x0000649C
			private static void ProcessAsyncCopyRequestStreamCompletion(IAsyncResult iar)
			{
				HttpClientTransportSink.AsyncHttpClientRequestState asyncHttpClientRequestState = (HttpClientTransportSink.AsyncHttpClientRequestState)iar.AsyncState;
				try
				{
					StreamHelper.EndAsyncCopyStream(iar);
					asyncHttpClientRequestState.WebRequest.BeginGetResponse(HttpClientTransportSink.AsyncHttpClientRequestState.s_processGetResponseCompletionCallback, asyncHttpClientRequestState);
				}
				catch (Exception e)
				{
					asyncHttpClientRequestState.RetryOrDispatchException(e);
				}
				catch
				{
					asyncHttpClientRequestState.RetryOrDispatchException(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")));
				}
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00007510 File Offset: 0x00006510
			private static void ProcessGetResponseCompletion(IAsyncResult iar)
			{
				HttpClientTransportSink.AsyncHttpClientRequestState asyncHttpClientRequestState = (HttpClientTransportSink.AsyncHttpClientRequestState)iar.AsyncState;
				try
				{
					asyncHttpClientRequestState.RequestStream.Close();
					HttpWebResponse httpWebResponse = null;
					HttpWebRequest webRequest = asyncHttpClientRequestState.WebRequest;
					try
					{
						httpWebResponse = (HttpWebResponse)webRequest.EndGetResponse(iar);
					}
					catch (WebException webException)
					{
						HttpClientTransportSink.ProcessResponseException(webException, out httpWebResponse);
					}
					asyncHttpClientRequestState.WebResponse = httpWebResponse;
					ChunkedMemoryStream chunkedMemoryStream = new ChunkedMemoryStream(CoreChannel.BufferPool);
					asyncHttpClientRequestState.ActualResponseStream = chunkedMemoryStream;
					StreamHelper.BeginAsyncCopyStream(httpWebResponse.GetResponseStream(), chunkedMemoryStream, true, false, true, false, HttpClientTransportSink.AsyncHttpClientRequestState.s_processAsyncCopyRequestStreamCompletion, asyncHttpClientRequestState);
				}
				catch (Exception e)
				{
					asyncHttpClientRequestState.SinkStack.DispatchException(e);
				}
				catch
				{
					asyncHttpClientRequestState.SinkStack.DispatchException(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")));
				}
			}

			// Token: 0x06000155 RID: 341 RVA: 0x000075E4 File Offset: 0x000065E4
			private static void ProcessAsyncCopyResponseStreamCompletion(IAsyncResult iar)
			{
				HttpClientTransportSink.AsyncHttpClientRequestState asyncHttpClientRequestState = (HttpClientTransportSink.AsyncHttpClientRequestState)iar.AsyncState;
				try
				{
					StreamHelper.EndAsyncCopyStream(iar);
					HttpWebResponse webResponse = asyncHttpClientRequestState.WebResponse;
					Stream actualResponseStream = asyncHttpClientRequestState.ActualResponseStream;
					ITransportHeaders headers = HttpClientTransportSink.CollectResponseHeaders(webResponse);
					asyncHttpClientRequestState.SinkStack.AsyncProcessResponse(headers, actualResponseStream);
				}
				catch (Exception e)
				{
					asyncHttpClientRequestState.SinkStack.DispatchException(e);
				}
				catch
				{
					asyncHttpClientRequestState.SinkStack.DispatchException(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")));
				}
			}

			// Token: 0x0400010A RID: 266
			private static AsyncCallback s_processGetRequestStreamCompletionCallback = new AsyncCallback(HttpClientTransportSink.AsyncHttpClientRequestState.ProcessGetRequestStreamCompletion);

			// Token: 0x0400010B RID: 267
			private static AsyncCallback s_processAsyncCopyRequestStreamCompletionCallback = new AsyncCallback(HttpClientTransportSink.AsyncHttpClientRequestState.ProcessAsyncCopyRequestStreamCompletion);

			// Token: 0x0400010C RID: 268
			private static AsyncCallback s_processGetResponseCompletionCallback = new AsyncCallback(HttpClientTransportSink.AsyncHttpClientRequestState.ProcessGetResponseCompletion);

			// Token: 0x0400010D RID: 269
			private static AsyncCallback s_processAsyncCopyRequestStreamCompletion = new AsyncCallback(HttpClientTransportSink.AsyncHttpClientRequestState.ProcessAsyncCopyResponseStreamCompletion);

			// Token: 0x0400010E RID: 270
			internal HttpWebRequest WebRequest;

			// Token: 0x0400010F RID: 271
			internal HttpWebResponse WebResponse;

			// Token: 0x04000110 RID: 272
			internal IClientChannelSinkStack SinkStack;

			// Token: 0x04000111 RID: 273
			internal Stream RequestStream;

			// Token: 0x04000112 RID: 274
			internal Stream ActualResponseStream;

			// Token: 0x04000113 RID: 275
			private HttpClientTransportSink _transportSink;

			// Token: 0x04000114 RID: 276
			private int _retryCount;

			// Token: 0x04000115 RID: 277
			private long _initialStreamPosition;

			// Token: 0x04000116 RID: 278
			private IMessage _msg;

			// Token: 0x04000117 RID: 279
			private ITransportHeaders _requestHeaders;
		}
	}
}
