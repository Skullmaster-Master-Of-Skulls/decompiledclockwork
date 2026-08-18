using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace System.Net.Http
{
	// Token: 0x02000016 RID: 22
	[__DynamicallyInvokable]
	public class HttpRequestMessage : IDisposable
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005B22 File Offset: 0x00003D22
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00005B2A File Offset: 0x00003D2A
		[__DynamicallyInvokable]
		public Version Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.version;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.CheckDisposed();
				this.version = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005B4D File Offset: 0x00003D4D
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00005B55 File Offset: 0x00003D55
		[__DynamicallyInvokable]
		public HttpContent Content
		{
			[__DynamicallyInvokable]
			get
			{
				return this.content;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposed();
				if (Logging.On)
				{
					if (value == null)
					{
						Logging.PrintInfo(Logging.Http, this, SR.net_http_log_content_null);
					}
					else
					{
						Logging.Associate(Logging.Http, this, value);
					}
				}
				this.content = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005B8C File Offset: 0x00003D8C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005B94 File Offset: 0x00003D94
		[__DynamicallyInvokable]
		public HttpMethod Method
		{
			[__DynamicallyInvokable]
			get
			{
				return this.method;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.CheckDisposed();
				this.method = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005BB7 File Offset: 0x00003DB7
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005BBF File Offset: 0x00003DBF
		[__DynamicallyInvokable]
		public Uri RequestUri
		{
			[__DynamicallyInvokable]
			get
			{
				return this.requestUri;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null && value.IsAbsoluteUri && !HttpUtilities.IsHttpUri(value))
				{
					throw new ArgumentException(SR.net_http_client_http_baseaddress_required, "value");
				}
				this.CheckDisposed();
				this.requestUri = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005BF7 File Offset: 0x00003DF7
		[__DynamicallyInvokable]
		public HttpRequestHeaders Headers
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.headers == null)
				{
					this.headers = new HttpRequestHeaders();
				}
				return this.headers;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005C12 File Offset: 0x00003E12
		[__DynamicallyInvokable]
		public IDictionary<string, object> Properties
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.properties == null)
				{
					this.properties = new Dictionary<string, object>();
				}
				return this.properties;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005C2D File Offset: 0x00003E2D
		[__DynamicallyInvokable]
		public HttpRequestMessage() : this(HttpMethod.Get, null)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005C3C File Offset: 0x00003E3C
		[__DynamicallyInvokable]
		public HttpRequestMessage(HttpMethod method, Uri requestUri)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, ".ctor", string.Concat(new string[]
				{
					"Method: ",
					(method != null) ? method.ToString() : null,
					", Uri: '",
					(requestUri != null) ? requestUri.ToString() : null,
					"'"
				}));
			}
			this.InitializeValues(method, requestUri);
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, ".ctor", null);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005CCC File Offset: 0x00003ECC
		[__DynamicallyInvokable]
		public HttpRequestMessage(HttpMethod method, string requestUri)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, ".ctor", string.Concat(new string[]
				{
					"Method: ",
					(method != null) ? method.ToString() : null,
					", Uri: '",
					requestUri,
					"'"
				}));
			}
			if (string.IsNullOrEmpty(requestUri))
			{
				this.InitializeValues(method, null);
			}
			else
			{
				this.InitializeValues(method, new Uri(requestUri, UriKind.RelativeOrAbsolute));
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, ".ctor", null);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005D68 File Offset: 0x00003F68
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Method: ");
			stringBuilder.Append(this.method);
			stringBuilder.Append(", RequestUri: '");
			stringBuilder.Append((this.requestUri == null) ? "<null>" : this.requestUri.ToString());
			stringBuilder.Append("', Version: ");
			stringBuilder.Append(this.version);
			stringBuilder.Append(", Content: ");
			stringBuilder.Append((this.content == null) ? "<null>" : this.content.GetType().FullName);
			stringBuilder.Append(", Headers:\r\n");
			stringBuilder.Append(HeaderUtilities.DumpHeaders(new HttpHeaders[]
			{
				this.headers,
				(this.content == null) ? null : this.content.Headers
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005E58 File Offset: 0x00004058
		private void InitializeValues(HttpMethod method, Uri requestUri)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (requestUri != null && requestUri.IsAbsoluteUri && !HttpUtilities.IsHttpUri(requestUri))
			{
				throw new ArgumentException(SR.net_http_client_http_baseaddress_required, "requestUri");
			}
			this.method = method;
			this.requestUri = requestUri;
			this.version = HttpUtilities.DefaultVersion;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005EBB File Offset: 0x000040BB
		internal bool MarkAsSent()
		{
			return Interlocked.Exchange(ref this.sendStatus, 1) == 0;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005ECC File Offset: 0x000040CC
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.content != null)
				{
					this.content.Dispose();
				}
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005EF3 File Offset: 0x000040F3
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005F02 File Offset: 0x00004102
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x040000AC RID: 172
		private const int messageAlreadySent = 1;

		// Token: 0x040000AD RID: 173
		private const int messageNotYetSent = 0;

		// Token: 0x040000AE RID: 174
		private int sendStatus;

		// Token: 0x040000AF RID: 175
		private HttpMethod method;

		// Token: 0x040000B0 RID: 176
		private Uri requestUri;

		// Token: 0x040000B1 RID: 177
		private HttpRequestHeaders headers;

		// Token: 0x040000B2 RID: 178
		private Version version;

		// Token: 0x040000B3 RID: 179
		private HttpContent content;

		// Token: 0x040000B4 RID: 180
		private bool disposed;

		// Token: 0x040000B5 RID: 181
		private IDictionary<string, object> properties;
	}
}
