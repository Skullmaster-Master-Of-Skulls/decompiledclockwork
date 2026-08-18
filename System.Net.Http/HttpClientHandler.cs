using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000009 RID: 9
	[__DynamicallyInvokable]
	public class HttpClientHandler : HttpMessageHandler
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002E99 File Offset: 0x00001099
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002EA1 File Offset: 0x000010A1
		public bool CheckCertificateRevocationList
		{
			get
			{
				return this._checkCertificateRevocationList;
			}
			set
			{
				this.CheckDisposedOrStarted();
				this._checkCertificateRevocationList = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002EB0 File Offset: 0x000010B0
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this._clientCertOptions != ClientCertificateOption.Manual)
				{
					throw new InvalidOperationException(string.Format(SR.net_http_invalid_enable_first, "ClientCertificateOptions", "Manual"));
				}
				if (this._clientCertificates == null)
				{
					this._clientCertificates = new X509Certificate2Collection();
				}
				return this._clientCertificates;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002EED File Offset: 0x000010ED
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002EF5 File Offset: 0x000010F5
		public ICredentials DefaultProxyCredentials
		{
			get
			{
				return this._defaultProxyCredentials;
			}
			set
			{
				this.CheckDisposedOrStarted();
				this._defaultProxyCredentials = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002F04 File Offset: 0x00001104
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002F0C File Offset: 0x0000110C
		public int MaxConnectionsPerServer
		{
			get
			{
				return this._maxConnectionsPerServer;
			}
			set
			{
				this.CheckDisposedOrStarted();
				this._maxConnectionsPerServerChanged = true;
				this._maxConnectionsPerServer = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002F22 File Offset: 0x00001122
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002F2A File Offset: 0x0000112A
		public int MaxResponseHeadersLength
		{
			get
			{
				return this._maxResponseHeadersLength;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckDisposedOrStarted();
				this._maxResponseHeadersLength = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002F48 File Offset: 0x00001148
		public IDictionary<string, object> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new Dictionary<string, object>();
				}
				return this._properties;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002F63 File Offset: 0x00001163
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002F6B File Offset: 0x0000116B
		public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> ServerCertificateCustomValidationCallback
		{
			get
			{
				return this._serverCertificateCustomValidationCallback;
			}
			set
			{
				this.CheckDisposedOrStarted();
				this._serverCertificateCustomValidationCallback = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002F7A File Offset: 0x0000117A
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002F82 File Offset: 0x00001182
		public SslProtocols SslProtocols
		{
			get
			{
				return this._sslProtocols;
			}
			set
			{
				HttpClientHandler.SecurityProtocol.ThrowOnNotAllowed(value, true);
				this.CheckDisposedOrStarted();
				this._sslProtocols = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002F98 File Offset: 0x00001198
		[__DynamicallyInvokable]
		public virtual bool SupportsAutomaticDecompression
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F9B File Offset: 0x0000119B
		[__DynamicallyInvokable]
		public virtual bool SupportsProxy
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002F9E File Offset: 0x0000119E
		[__DynamicallyInvokable]
		public virtual bool SupportsRedirectConfiguration
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002FA1 File Offset: 0x000011A1
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002FA9 File Offset: 0x000011A9
		[__DynamicallyInvokable]
		public bool UseCookies
		{
			[__DynamicallyInvokable]
			get
			{
				return this._useCookies;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposedOrStarted();
				this._useCookies = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FB8 File Offset: 0x000011B8
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002FC0 File Offset: 0x000011C0
		[__DynamicallyInvokable]
		public CookieContainer CookieContainer
		{
			[__DynamicallyInvokable]
			get
			{
				return this._cookieContainer;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.UseCookies)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, SR.net_http_invalid_enable_first, new object[]
					{
						"UseCookies",
						"true"
					}));
				}
				this.CheckDisposedOrStarted();
				this._cookieContainer = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000301B File Offset: 0x0000121B
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003023 File Offset: 0x00001223
		[__DynamicallyInvokable]
		public ClientCertificateOption ClientCertificateOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this._clientCertOptions;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != ClientCertificateOption.Manual && value != ClientCertificateOption.Automatic)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckDisposedOrStarted();
				this._clientCertOptions = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003044 File Offset: 0x00001244
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000304C File Offset: 0x0000124C
		[__DynamicallyInvokable]
		public DecompressionMethods AutomaticDecompression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._automaticDecompression;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposedOrStarted();
				this._automaticDecompression = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000305B File Offset: 0x0000125B
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003063 File Offset: 0x00001263
		[__DynamicallyInvokable]
		public bool UseProxy
		{
			[__DynamicallyInvokable]
			get
			{
				return this._useProxy;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposedOrStarted();
				this._useProxy = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003072 File Offset: 0x00001272
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000307C File Offset: 0x0000127C
		[__DynamicallyInvokable]
		public IWebProxy Proxy
		{
			[__DynamicallyInvokable]
			get
			{
				return this._proxy;
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			set
			{
				if (!this.UseProxy && value != null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, SR.net_http_invalid_enable_first, new object[]
					{
						"UseProxy",
						"true"
					}));
				}
				this.CheckDisposedOrStarted();
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				this._proxy = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000030DE File Offset: 0x000012DE
		[__DynamicallyInvokable]
		public bool PreAuthenticate
		{
			[__DynamicallyInvokable]
			get
			{
				return this._preAuthenticate;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposedOrStarted();
				this._preAuthenticate = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000030ED File Offset: 0x000012ED
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000030F5 File Offset: 0x000012F5
		[__DynamicallyInvokable]
		public bool UseDefaultCredentials
		{
			[__DynamicallyInvokable]
			get
			{
				return this._useDefaultCredentials;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposedOrStarted();
				this._useDefaultCredentials = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003104 File Offset: 0x00001304
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000310C File Offset: 0x0000130C
		[__DynamicallyInvokable]
		public ICredentials Credentials
		{
			[__DynamicallyInvokable]
			get
			{
				return this._credentials;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposedOrStarted();
				this._credentials = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000311B File Offset: 0x0000131B
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003123 File Offset: 0x00001323
		[__DynamicallyInvokable]
		public bool AllowAutoRedirect
		{
			[__DynamicallyInvokable]
			get
			{
				return this._allowAutoRedirect;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposedOrStarted();
				this._allowAutoRedirect = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000313A File Offset: 0x0000133A
		[__DynamicallyInvokable]
		public int MaxAutomaticRedirections
		{
			[__DynamicallyInvokable]
			get
			{
				return this._maxAutomaticRedirections;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckDisposedOrStarted();
				this._maxAutomaticRedirections = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003158 File Offset: 0x00001358
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003160 File Offset: 0x00001360
		[__DynamicallyInvokable]
		public long MaxRequestContentBufferSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this._maxRequestContentBufferSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", value, string.Format(CultureInfo.InvariantCulture, SR.net_http_content_buffersize_limit, new object[]
					{
						2147483647L
					}));
				}
				this.CheckDisposedOrStarted();
				this._maxRequestContentBufferSize = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000031C7 File Offset: 0x000013C7
		public static Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> DangerousAcceptAnyServerCertificateValidator { get; } = (HttpRequestMessage <p0>, X509Certificate2 <p1>, X509Chain <p2>, SslPolicyErrors <p3>) => true;

		// Token: 0x06000080 RID: 128 RVA: 0x000031D0 File Offset: 0x000013D0
		[__DynamicallyInvokable]
		public HttpClientHandler()
		{
			this._startRequest = new Action<object>(this.StartRequest);
			this._getRequestStreamCallback = new AsyncCallback(this.GetRequestStreamCallback);
			this._getResponseCallback = new AsyncCallback(this.GetResponseCallback);
			this._connectionGroupName = RuntimeHelpers.GetHashCode(this).ToString(NumberFormatInfo.InvariantInfo);
			this._allowAutoRedirect = true;
			this._maxRequestContentBufferSize = 2147483647L;
			this._automaticDecompression = DecompressionMethods.None;
			this._cookieContainer = new CookieContainer();
			this._credentials = null;
			this._maxAutomaticRedirections = 50;
			this._preAuthenticate = false;
			this._proxy = null;
			this._useProxy = true;
			this._useCookies = true;
			this._useDefaultCredentials = false;
			this._clientCertOptions = ClientCertificateOption.Manual;
			this._maxResponseHeadersLength = HttpWebRequest.DefaultMaximumResponseHeadersLength;
			this._defaultProxyCredentials = null;
			this._clientCertificates = null;
			this._properties = null;
			this._maxConnectionsPerServer = ServicePointManager.DefaultConnectionLimit;
			this._maxConnectionsPerServerChanged = false;
			this._serverCertificateCustomValidationCallback = null;
			this._sslProtocols = SslProtocols.None;
			this._checkCertificateRevocationList = false;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000032D7 File Offset: 0x000014D7
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				this._disposed = true;
				ServicePointManager.CloseConnectionGroups(this._connectionGroupName);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003304 File Offset: 0x00001504
		private HttpWebRequest CreateAndPrepareWebRequest(HttpRequestMessage request)
		{
			HttpWebRequest httpWebRequest;
			if (request.Content != null)
			{
				httpWebRequest = new HttpWebRequest(request.RequestUri, true, this._connectionGroupName, new Action<Stream>(request.Content.CopyTo));
			}
			else
			{
				httpWebRequest = new HttpWebRequest(request.RequestUri, true, this._connectionGroupName, null);
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Http, request, httpWebRequest);
			}
			httpWebRequest.Method = request.Method.Method;
			httpWebRequest.ProtocolVersion = request.Version;
			this.SetDefaultOptions(httpWebRequest);
			HttpClientHandler.SetConnectionOptions(httpWebRequest, request);
			this.SetServicePointOptions(httpWebRequest, request);
			HttpClientHandler.SetRequestHeaders(httpWebRequest, request);
			HttpClientHandler.SetContentHeaders(httpWebRequest, request);
			request.SetRtcOptions(httpWebRequest);
			if (this._maxConnectionsPerServerChanged)
			{
				httpWebRequest.ServicePoint.ConnectionLimit = this._maxConnectionsPerServer;
			}
			httpWebRequest.MaximumResponseHeadersLength = this._maxResponseHeadersLength;
			if (this.ClientCertificateOptions == ClientCertificateOption.Manual && this._clientCertificates != null && this._clientCertificates.Count > 0)
			{
				httpWebRequest.ClientCertificates = this._clientCertificates;
			}
			if (this._serverCertificateCustomValidationCallback != null)
			{
				httpWebRequest.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.ServerCertificateValidationCallback);
				httpWebRequest.ServerCertificateValidationCallbackContext = request;
			}
			if (this._defaultProxyCredentials != null && this._useProxy && this._proxy == null && httpWebRequest.Proxy != null)
			{
				httpWebRequest.Proxy.Credentials = this._defaultProxyCredentials;
			}
			if (this._checkCertificateRevocationList)
			{
				httpWebRequest.CheckCertificateRevocationList = this._checkCertificateRevocationList;
			}
			if (this._sslProtocols != SslProtocols.None)
			{
				httpWebRequest.SslProtocols = this._sslProtocols;
			}
			this.InitializeWebRequest(request, httpWebRequest);
			return httpWebRequest;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003484 File Offset: 0x00001684
		private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)sender;
			HttpRequestMessage arg = (HttpRequestMessage)httpWebRequest.ServerCertificateValidationCallbackContext;
			return this._serverCertificateCustomValidationCallback(arg, (X509Certificate2)certificate, chain, sslPolicyErrors);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000034B9 File Offset: 0x000016B9
		internal virtual void InitializeWebRequest(HttpRequestMessage request, HttpWebRequest webRequest)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000034BC File Offset: 0x000016BC
		private void SetDefaultOptions(HttpWebRequest webRequest)
		{
			webRequest.Timeout = -1;
			webRequest.AllowAutoRedirect = this._allowAutoRedirect;
			webRequest.AutomaticDecompression = this._automaticDecompression;
			webRequest.PreAuthenticate = this._preAuthenticate;
			if (this._useDefaultCredentials)
			{
				webRequest.UseDefaultCredentials = true;
			}
			else
			{
				webRequest.Credentials = this._credentials;
			}
			if (this._allowAutoRedirect)
			{
				webRequest.MaximumAutomaticRedirections = this._maxAutomaticRedirections;
			}
			if (this._useProxy)
			{
				if (this._proxy != null)
				{
					webRequest.Proxy = this._proxy;
				}
			}
			else
			{
				webRequest.Proxy = null;
			}
			if (this._useCookies)
			{
				webRequest.CookieContainer = this._cookieContainer;
			}
			if (this._clientCertOptions == ClientCertificateOption.Automatic && ComNetOS.IsWin7orLater)
			{
				X509CertificateCollection x509CertificateCollection = UnsafeNclNativeMethods.NativePKI.FindClientCertificates();
				if (x509CertificateCollection.Count > 0)
				{
					webRequest.ClientCertificates = x509CertificateCollection;
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003584 File Offset: 0x00001784
		private static void SetConnectionOptions(HttpWebRequest webRequest, HttpRequestMessage request)
		{
			if (request.Version <= HttpVersion.Version10)
			{
				bool keepAlive = false;
				foreach (string strA in request.Headers.Connection)
				{
					if (string.Compare(strA, "Keep-Alive", StringComparison.OrdinalIgnoreCase) == 0)
					{
						keepAlive = true;
						break;
					}
				}
				webRequest.KeepAlive = keepAlive;
				return;
			}
			bool? connectionClose = request.Headers.ConnectionClose;
			bool flag = true;
			if (connectionClose.GetValueOrDefault() == flag & connectionClose != null)
			{
				webRequest.KeepAlive = false;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003628 File Offset: 0x00001828
		private void SetServicePointOptions(HttpWebRequest webRequest, HttpRequestMessage request)
		{
			HttpRequestHeaders headers = request.Headers;
			bool? expectContinue = headers.ExpectContinue;
			if (expectContinue != null)
			{
				ServicePoint servicePoint = webRequest.ServicePoint;
				servicePoint.Expect100Continue = expectContinue.Value;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003664 File Offset: 0x00001864
		private static void SetRequestHeaders(HttpWebRequest webRequest, HttpRequestMessage request)
		{
			WebHeaderCollection headers = webRequest.Headers;
			HttpRequestHeaders headers2 = request.Headers;
			bool flag = headers2.Contains("Host");
			bool flag2 = headers2.Contains("Expect");
			bool flag3 = headers2.Contains("Transfer-Encoding");
			bool flag4 = headers2.Contains("Connection");
			if (flag)
			{
				string host = headers2.Host;
				if (host != null)
				{
					webRequest.Host = host;
				}
			}
			if (flag2)
			{
				string headerStringWithoutSpecial = headers2.Expect.GetHeaderStringWithoutSpecial();
				if (!string.IsNullOrEmpty(headerStringWithoutSpecial) || !headers2.Expect.IsSpecialValueSet)
				{
					headers.AddInternal("Expect", headerStringWithoutSpecial);
				}
			}
			if (flag3)
			{
				string headerStringWithoutSpecial2 = headers2.TransferEncoding.GetHeaderStringWithoutSpecial();
				if (!string.IsNullOrEmpty(headerStringWithoutSpecial2) || !headers2.TransferEncoding.IsSpecialValueSet)
				{
					headers.AddInternal("Transfer-Encoding", headerStringWithoutSpecial2);
				}
			}
			if (flag4)
			{
				string headerStringWithoutSpecial3 = headers2.Connection.GetHeaderStringWithoutSpecial();
				if (!string.IsNullOrEmpty(headerStringWithoutSpecial3) || !headers2.Connection.IsSpecialValueSet)
				{
					headers.AddInternal("Connection", headerStringWithoutSpecial3);
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair in request.Headers.GetHeaderStrings())
			{
				string key = keyValuePair.Key;
				if ((!flag || !HttpClientHandler.AreEqual("Host", key)) && (!flag2 || !HttpClientHandler.AreEqual("Expect", key)) && (!flag3 || !HttpClientHandler.AreEqual("Transfer-Encoding", key)) && (!flag4 || !HttpClientHandler.AreEqual("Connection", key)))
				{
					headers.AddInternal(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000380C File Offset: 0x00001A0C
		private static void SetContentHeaders(HttpWebRequest webRequest, HttpRequestMessage request)
		{
			if (request.Content != null)
			{
				HttpContentHeaders headers = request.Content.Headers;
				if (headers.Contains("Content-Length"))
				{
					using (IEnumerator<KeyValuePair<string, IEnumerable<string>>> enumerator = request.Content.Headers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, IEnumerable<string>> keyValuePair = enumerator.Current;
							if (string.Compare("Content-Length", keyValuePair.Key, StringComparison.OrdinalIgnoreCase) != 0)
							{
								webRequest.Headers.AddInternal(keyValuePair.Key, string.Join(", ", keyValuePair.Value));
							}
						}
						return;
					}
				}
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair2 in request.Content.Headers)
				{
					webRequest.Headers.AddInternal(keyValuePair2.Key, string.Join(", ", keyValuePair2.Value));
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003910 File Offset: 0x00001B10
		[__DynamicallyInvokable]
		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request", SR.net_http_handler_norequest);
			}
			this.CheckDisposed();
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, "SendAsync", request);
			}
			this.SetOperationStarted();
			TaskCompletionSource<HttpResponseMessage> taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
			HttpClientHandler.RequestState state = new HttpClientHandler.RequestState();
			state.tcs = taskCompletionSource;
			state.cancellationToken = cancellationToken;
			state.requestMessage = request;
			try
			{
				HttpWebRequest httpWebRequest = this.CreateAndPrepareWebRequest(request);
				state.webRequest = httpWebRequest;
				cancellationToken.Register(HttpClientHandler.s_onCancel, httpWebRequest);
				if (ExecutionContext.IsFlowSuppressed())
				{
					IWebProxy webProxy = null;
					if (this._useProxy)
					{
						webProxy = (this._proxy ?? WebRequest.DefaultWebProxy);
					}
					if (this.UseDefaultCredentials || this.Credentials != null || (webProxy != null && webProxy.Credentials != null))
					{
						this.SafeCaptureIdenity(state);
					}
				}
				Task.Run(delegate()
				{
					this._startRequest(state);
				});
			}
			catch (Exception e)
			{
				this.HandleAsyncException(state, e);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, "SendAsync", taskCompletionSource.Task);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003A5C File Offset: 0x00001C5C
		private void StartRequest(object obj)
		{
			HttpClientHandler.RequestState requestState = obj as HttpClientHandler.RequestState;
			try
			{
				if (requestState.requestMessage.Content != null)
				{
					this.PrepareAndStartContentUpload(requestState);
				}
				else
				{
					requestState.webRequest.ContentLength = 0L;
					this.StartGettingResponse(requestState);
				}
			}
			catch (Exception e)
			{
				this.HandleAsyncException(requestState, e);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003AB8 File Offset: 0x00001CB8
		private void PrepareAndStartContentUpload(HttpClientHandler.RequestState state)
		{
			HttpContent requestContent = state.requestMessage.Content;
			try
			{
				bool? transferEncodingChunked = state.requestMessage.Headers.TransferEncodingChunked;
				bool flag = true;
				if (transferEncodingChunked.GetValueOrDefault() == flag & transferEncodingChunked != null)
				{
					state.webRequest.SendChunked = true;
					this.StartGettingRequestStream(state);
				}
				else
				{
					long? contentLength = requestContent.Headers.ContentLength;
					if (contentLength != null)
					{
						state.webRequest.ContentLength = contentLength.Value;
						this.StartGettingRequestStream(state);
					}
					else
					{
						if (this._maxRequestContentBufferSize == 0L)
						{
							throw new HttpRequestException(SR.net_http_handler_nocontentlength);
						}
						requestContent.LoadIntoBufferAsync(this._maxRequestContentBufferSize).ContinueWithStandard(delegate(Task task)
						{
							if (task.IsFaulted)
							{
								this.HandleAsyncException(state, task.Exception.GetBaseException());
								return;
							}
							try
							{
								contentLength = requestContent.Headers.ContentLength;
								state.webRequest.ContentLength = contentLength.Value;
								this.StartGettingRequestStream(state);
							}
							catch (Exception e2)
							{
								this.HandleAsyncException(state, e2);
							}
						});
					}
				}
			}
			catch (Exception e)
			{
				this.HandleAsyncException(state, e);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003BE4 File Offset: 0x00001DE4
		private void StartGettingRequestStream(HttpClientHandler.RequestState state)
		{
			if (state.identity != null)
			{
				using (state.identity.Impersonate())
				{
					state.webRequest.BeginGetRequestStream(this._getRequestStreamCallback, state);
					return;
				}
			}
			state.webRequest.BeginGetRequestStream(this._getRequestStreamCallback, state);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003C48 File Offset: 0x00001E48
		private void GetRequestStreamCallback(IAsyncResult ar)
		{
			HttpClientHandler.RequestState state = ar.AsyncState as HttpClientHandler.RequestState;
			try
			{
				TransportContext context = null;
				Stream stream = state.webRequest.EndGetRequestStream(ar, out context);
				state.requestStream = stream;
				state.requestMessage.Content.CopyToAsync(stream, context).ContinueWithStandard(delegate(Task task)
				{
					try
					{
						if (task.IsFaulted)
						{
							this.HandleAsyncException(state, task.Exception.GetBaseException());
						}
						else if (task.IsCanceled)
						{
							state.tcs.TrySetCanceled(state.cancellationToken);
						}
						else
						{
							state.requestStream.Close();
							this.StartGettingResponse(state);
						}
					}
					catch (Exception e2)
					{
						this.HandleAsyncException(state, e2);
					}
				});
			}
			catch (Exception e)
			{
				this.HandleAsyncException(state, e);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003CE0 File Offset: 0x00001EE0
		private void StartGettingResponse(HttpClientHandler.RequestState state)
		{
			if (state.identity != null)
			{
				using (state.identity.Impersonate())
				{
					state.webRequest.BeginGetResponse(this._getResponseCallback, state);
					goto IL_46;
				}
			}
			state.webRequest.BeginGetResponse(this._getResponseCallback, state);
			IL_46:
			state.requestMessage.MarkRtcFlushComplete();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003D50 File Offset: 0x00001F50
		private void GetResponseCallback(IAsyncResult ar)
		{
			HttpClientHandler.RequestState requestState = ar.AsyncState as HttpClientHandler.RequestState;
			try
			{
				HttpWebResponse webResponse = requestState.webRequest.EndGetResponse(ar) as HttpWebResponse;
				requestState.tcs.TrySetResult(this.CreateResponseMessage(webResponse, requestState.requestMessage));
			}
			catch (Exception e)
			{
				this.HandleAsyncException(requestState, e);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003DB4 File Offset: 0x00001FB4
		private HttpResponseMessage CreateResponseMessage(HttpWebResponse webResponse, HttpRequestMessage request)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(webResponse.StatusCode);
			httpResponseMessage.ReasonPhrase = webResponse.StatusDescription;
			httpResponseMessage.Version = webResponse.ProtocolVersion;
			httpResponseMessage.RequestMessage = request;
			httpResponseMessage.Content = new StreamContent(new HttpClientHandler.WebExceptionWrapperStream(webResponse.GetResponseStream()));
			request.RequestUri = webResponse.ResponseUri;
			WebHeaderCollection headers = webResponse.Headers;
			HttpContentHeaders headers2 = httpResponseMessage.Content.Headers;
			HttpResponseHeaders headers3 = httpResponseMessage.Headers;
			if (webResponse.ContentLength >= 0L)
			{
				headers2.ContentLength = new long?(webResponse.ContentLength);
			}
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				if (string.Compare(key, "Content-Length", StringComparison.OrdinalIgnoreCase) != 0)
				{
					string[] values = headers.GetValues(i);
					if (!headers3.TryAddWithoutValidation(key, values))
					{
						bool flag = headers2.TryAddWithoutValidation(key, values);
					}
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003E94 File Offset: 0x00002094
		private void HandleAsyncException(HttpClientHandler.RequestState state, Exception e)
		{
			if (Logging.On)
			{
				Logging.Exception(Logging.Http, this, "SendAsync", e);
			}
			if (state.cancellationToken.IsCancellationRequested)
			{
				state.tcs.TrySetCanceled(state.cancellationToken);
			}
			else if (e is WebException || e is IOException)
			{
				state.tcs.TrySetException(new HttpRequestException(SR.net_http_client_execution_error, e));
			}
			else
			{
				state.tcs.TrySetException(e);
			}
			state.requestMessage.AbortRtcRequest();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003F1C File Offset: 0x0000211C
		private static void OnCancel(object state)
		{
			HttpWebRequest httpWebRequest = state as HttpWebRequest;
			httpWebRequest.Abort();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003F36 File Offset: 0x00002136
		private void SetOperationStarted()
		{
			if (!this._operationStarted)
			{
				this._operationStarted = true;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003F4B File Offset: 0x0000214B
		private void CheckDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003F68 File Offset: 0x00002168
		internal void CheckDisposedOrStarted()
		{
			this.CheckDisposed();
			if (this._operationStarted)
			{
				throw new InvalidOperationException(SR.net_http_operation_started);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003F85 File Offset: 0x00002185
		private static bool AreEqual(string x, string y)
		{
			return string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003F92 File Offset: 0x00002192
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private void SafeCaptureIdenity(HttpClientHandler.RequestState state)
		{
			state.identity = WindowsIdentity.GetCurrent();
		}

		// Token: 0x04000059 RID: 89
		private static readonly Action<object> s_onCancel = new Action<object>(HttpClientHandler.OnCancel);

		// Token: 0x0400005A RID: 90
		private readonly Action<object> _startRequest;

		// Token: 0x0400005B RID: 91
		private readonly AsyncCallback _getRequestStreamCallback;

		// Token: 0x0400005C RID: 92
		private readonly AsyncCallback _getResponseCallback;

		// Token: 0x0400005D RID: 93
		private volatile bool _operationStarted;

		// Token: 0x0400005E RID: 94
		private volatile bool _disposed;

		// Token: 0x0400005F RID: 95
		private long _maxRequestContentBufferSize;

		// Token: 0x04000060 RID: 96
		private int _maxResponseHeadersLength;

		// Token: 0x04000061 RID: 97
		private CookieContainer _cookieContainer;

		// Token: 0x04000062 RID: 98
		private bool _useCookies;

		// Token: 0x04000063 RID: 99
		private DecompressionMethods _automaticDecompression;

		// Token: 0x04000064 RID: 100
		private IWebProxy _proxy;

		// Token: 0x04000065 RID: 101
		private bool _useProxy;

		// Token: 0x04000066 RID: 102
		private ICredentials _defaultProxyCredentials;

		// Token: 0x04000067 RID: 103
		private bool _preAuthenticate;

		// Token: 0x04000068 RID: 104
		private bool _useDefaultCredentials;

		// Token: 0x04000069 RID: 105
		private ICredentials _credentials;

		// Token: 0x0400006A RID: 106
		private bool _allowAutoRedirect;

		// Token: 0x0400006B RID: 107
		private int _maxAutomaticRedirections;

		// Token: 0x0400006C RID: 108
		private string _connectionGroupName;

		// Token: 0x0400006D RID: 109
		private ClientCertificateOption _clientCertOptions;

		// Token: 0x0400006E RID: 110
		private X509Certificate2Collection _clientCertificates;

		// Token: 0x0400006F RID: 111
		private IDictionary<string, object> _properties;

		// Token: 0x04000070 RID: 112
		private int _maxConnectionsPerServer;

		// Token: 0x04000071 RID: 113
		private bool _maxConnectionsPerServerChanged;

		// Token: 0x04000072 RID: 114
		private Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> _serverCertificateCustomValidationCallback;

		// Token: 0x04000073 RID: 115
		private SslProtocols _sslProtocols;

		// Token: 0x04000074 RID: 116
		private bool _checkCertificateRevocationList;

		// Token: 0x0200004D RID: 77
		private static class SecurityProtocol
		{
			// Token: 0x06000417 RID: 1047 RVA: 0x0000F504 File Offset: 0x0000D704
			internal static void ThrowOnNotAllowed(SslProtocols protocols, bool allowNone = true)
			{
				if ((!allowNone && protocols == SslProtocols.None) || (protocols & ~(SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13)) != SslProtocols.None)
				{
					throw new NotSupportedException(SR.net_http_securityprotocolnotsupported);
				}
			}

			// Token: 0x0400018C RID: 396
			internal const SslProtocols AllowedSecurityProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;

			// Token: 0x0400018D RID: 397
			internal const SslProtocols DefaultSecurityProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;

			// Token: 0x0400018E RID: 398
			internal const SslProtocols SystemDefaultSecurityProtocols = SslProtocols.None;
		}

		// Token: 0x0200004E RID: 78
		private class RequestState
		{
			// Token: 0x0400018F RID: 399
			internal HttpWebRequest webRequest;

			// Token: 0x04000190 RID: 400
			internal TaskCompletionSource<HttpResponseMessage> tcs;

			// Token: 0x04000191 RID: 401
			internal CancellationToken cancellationToken;

			// Token: 0x04000192 RID: 402
			internal HttpRequestMessage requestMessage;

			// Token: 0x04000193 RID: 403
			internal Stream requestStream;

			// Token: 0x04000194 RID: 404
			internal WindowsIdentity identity;
		}

		// Token: 0x0200004F RID: 79
		private class WebExceptionWrapperStream : DelegatingStream
		{
			// Token: 0x06000419 RID: 1049 RVA: 0x0000F528 File Offset: 0x0000D728
			internal WebExceptionWrapperStream(Stream innerStream) : base(innerStream)
			{
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x0000F534 File Offset: 0x0000D734
			public override int Read(byte[] buffer, int offset, int count)
			{
				int result;
				try
				{
					result = base.Read(buffer, offset, count);
				}
				catch (WebException innerException)
				{
					throw new IOException(SR.net_http_read_error, innerException);
				}
				return result;
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x0000F56C File Offset: 0x0000D76C
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				try
				{
					result = base.BeginRead(buffer, offset, count, callback, state);
				}
				catch (WebException innerException)
				{
					throw new IOException(SR.net_http_read_error, innerException);
				}
				return result;
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
			public override int EndRead(IAsyncResult asyncResult)
			{
				int result;
				try
				{
					result = base.EndRead(asyncResult);
				}
				catch (WebException innerException)
				{
					throw new IOException(SR.net_http_read_error, innerException);
				}
				return result;
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				HttpClientHandler.WebExceptionWrapperStream.<ReadAsync>d__4 <ReadAsync>d__;
				<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
				<ReadAsync>d__.<>4__this = this;
				<ReadAsync>d__.buffer = buffer;
				<ReadAsync>d__.offset = offset;
				<ReadAsync>d__.count = count;
				<ReadAsync>d__.cancellationToken = cancellationToken;
				<ReadAsync>d__.<>1__state = -1;
				<ReadAsync>d__.<>t__builder.Start<HttpClientHandler.WebExceptionWrapperStream.<ReadAsync>d__4>(ref <ReadAsync>d__);
				return <ReadAsync>d__.<>t__builder.Task;
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x0000F644 File Offset: 0x0000D844
			public override int ReadByte()
			{
				int result;
				try
				{
					result = base.ReadByte();
				}
				catch (WebException innerException)
				{
					throw new IOException(SR.net_http_read_error, innerException);
				}
				return result;
			}
		}
	}
}
