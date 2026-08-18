using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	// Token: 0x02000017 RID: 23
	[__DynamicallyInvokable]
	public class HttpResponseMessage : IDisposable
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005F1D File Offset: 0x0000411D
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00005F25 File Offset: 0x00004125
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005F48 File Offset: 0x00004148
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005F50 File Offset: 0x00004150
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005F87 File Offset: 0x00004187
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005F8F File Offset: 0x0000418F
		[__DynamicallyInvokable]
		public HttpStatusCode StatusCode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.statusCode;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < (HttpStatusCode)0 || value > (HttpStatusCode)999)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckDisposed();
				this.statusCode = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005FB5 File Offset: 0x000041B5
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005FD1 File Offset: 0x000041D1
		[__DynamicallyInvokable]
		public string ReasonPhrase
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.reasonPhrase != null)
				{
					return this.reasonPhrase;
				}
				return HttpStatusDescription.Get(this.StatusCode);
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null && this.ContainsNewLineCharacter(value))
				{
					throw new FormatException(SR.net_http_reasonphrase_format_error);
				}
				this.CheckDisposed();
				this.reasonPhrase = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005FF7 File Offset: 0x000041F7
		[__DynamicallyInvokable]
		public HttpResponseHeaders Headers
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.headers == null)
				{
					this.headers = new HttpResponseHeaders();
				}
				return this.headers;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006012 File Offset: 0x00004212
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000601A File Offset: 0x0000421A
		[__DynamicallyInvokable]
		public HttpRequestMessage RequestMessage
		{
			[__DynamicallyInvokable]
			get
			{
				return this.requestMessage;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckDisposed();
				if (Logging.On && value != null)
				{
					Logging.Associate(Logging.Http, this, value);
				}
				this.requestMessage = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000603F File Offset: 0x0000423F
		[__DynamicallyInvokable]
		public bool IsSuccessStatusCode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.statusCode >= HttpStatusCode.OK && this.statusCode <= (HttpStatusCode)299;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006060 File Offset: 0x00004260
		[__DynamicallyInvokable]
		public HttpResponseMessage() : this(HttpStatusCode.OK)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006070 File Offset: 0x00004270
		[__DynamicallyInvokable]
		public HttpResponseMessage(HttpStatusCode statusCode)
		{
			if (Logging.On)
			{
				TraceSource http = Logging.Http;
				string method = ".ctor";
				string[] array = new string[5];
				array[0] = "StatusCode: ";
				int num = 1;
				int num2 = (int)statusCode;
				array[num] = num2.ToString();
				array[2] = ", ReasonPhrase: '";
				array[3] = this.reasonPhrase;
				array[4] = "'";
				Logging.Enter(http, this, method, string.Concat(array));
			}
			if (statusCode < (HttpStatusCode)0 || statusCode > (HttpStatusCode)999)
			{
				throw new ArgumentOutOfRangeException("statusCode");
			}
			this.statusCode = statusCode;
			this.version = HttpUtilities.DefaultVersion;
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, ".ctor", null);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006114 File Offset: 0x00004314
		[__DynamicallyInvokable]
		public HttpResponseMessage EnsureSuccessStatusCode()
		{
			if (!this.IsSuccessStatusCode)
			{
				if (this.content != null)
				{
					this.content.Dispose();
				}
				throw new HttpRequestException(string.Format(CultureInfo.InvariantCulture, SR.net_http_message_not_success_statuscode, new object[]
				{
					(int)this.statusCode,
					this.ReasonPhrase
				}));
			}
			return this;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006170 File Offset: 0x00004370
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StatusCode: ");
			stringBuilder.Append((int)this.statusCode);
			stringBuilder.Append(", ReasonPhrase: '");
			stringBuilder.Append(this.ReasonPhrase ?? "<null>");
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

		// Token: 0x06000151 RID: 337 RVA: 0x00006250 File Offset: 0x00004450
		private bool ContainsNewLineCharacter(string value)
		{
			foreach (char c in value)
			{
				if (c == '\r' || c == '\n')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006285 File Offset: 0x00004485
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

		// Token: 0x06000153 RID: 339 RVA: 0x000062AC File Offset: 0x000044AC
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000062BB File Offset: 0x000044BB
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x040000B6 RID: 182
		private const HttpStatusCode defaultStatusCode = HttpStatusCode.OK;

		// Token: 0x040000B7 RID: 183
		private HttpStatusCode statusCode;

		// Token: 0x040000B8 RID: 184
		private HttpResponseHeaders headers;

		// Token: 0x040000B9 RID: 185
		private string reasonPhrase;

		// Token: 0x040000BA RID: 186
		private HttpRequestMessage requestMessage;

		// Token: 0x040000BB RID: 187
		private Version version;

		// Token: 0x040000BC RID: 188
		private HttpContent content;

		// Token: 0x040000BD RID: 189
		private bool disposed;
	}
}
