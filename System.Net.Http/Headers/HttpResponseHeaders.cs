using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x02000034 RID: 52
	[__DynamicallyInvokable]
	public sealed class HttpResponseHeaders : HttpHeaders
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000C038 File Offset: 0x0000A238
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<string> AcceptRanges
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.acceptRanges == null)
				{
					this.acceptRanges = new HttpHeaderValueCollection<string>("Accept-Ranges", this, HeaderUtilities.TokenValidator);
				}
				return this.acceptRanges;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C05E File Offset: 0x0000A25E
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000C06B File Offset: 0x0000A26B
		[__DynamicallyInvokable]
		public TimeSpan? Age
		{
			[__DynamicallyInvokable]
			get
			{
				return HeaderUtilities.GetTimeSpanValue("Age", this);
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Age", value);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000C07E File Offset: 0x0000A27E
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000C090 File Offset: 0x0000A290
		[__DynamicallyInvokable]
		public EntityTagHeaderValue ETag
		{
			[__DynamicallyInvokable]
			get
			{
				return (EntityTagHeaderValue)base.GetParsedValues("ETag");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("ETag", value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C09E File Offset: 0x0000A29E
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		[__DynamicallyInvokable]
		public Uri Location
		{
			[__DynamicallyInvokable]
			get
			{
				return (Uri)base.GetParsedValues("Location");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Location", value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C0BE File Offset: 0x0000A2BE
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<AuthenticationHeaderValue> ProxyAuthenticate
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.proxyAuthenticate == null)
				{
					this.proxyAuthenticate = new HttpHeaderValueCollection<AuthenticationHeaderValue>("Proxy-Authenticate", this);
				}
				return this.proxyAuthenticate;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000C0DF File Offset: 0x0000A2DF
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000C0F1 File Offset: 0x0000A2F1
		[__DynamicallyInvokable]
		public RetryConditionHeaderValue RetryAfter
		{
			[__DynamicallyInvokable]
			get
			{
				return (RetryConditionHeaderValue)base.GetParsedValues("Retry-After");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Retry-After", value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000C0FF File Offset: 0x0000A2FF
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<ProductInfoHeaderValue> Server
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.server == null)
				{
					this.server = new HttpHeaderValueCollection<ProductInfoHeaderValue>("Server", this);
				}
				return this.server;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000C120 File Offset: 0x0000A320
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<string> Vary
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.vary == null)
				{
					this.vary = new HttpHeaderValueCollection<string>("Vary", this, HeaderUtilities.TokenValidator);
				}
				return this.vary;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C146 File Offset: 0x0000A346
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<AuthenticationHeaderValue> WwwAuthenticate
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.wwwAuthenticate == null)
				{
					this.wwwAuthenticate = new HttpHeaderValueCollection<AuthenticationHeaderValue>("WWW-Authenticate", this);
				}
				return this.wwwAuthenticate;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000C167 File Offset: 0x0000A367
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000C174 File Offset: 0x0000A374
		[__DynamicallyInvokable]
		public CacheControlHeaderValue CacheControl
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.CacheControl;
			}
			[__DynamicallyInvokable]
			set
			{
				this.generalHeaders.CacheControl = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000C182 File Offset: 0x0000A382
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<string> Connection
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Connection;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000C18F File Offset: 0x0000A38F
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000C19C File Offset: 0x0000A39C
		[__DynamicallyInvokable]
		public bool? ConnectionClose
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.ConnectionClose;
			}
			[__DynamicallyInvokable]
			set
			{
				this.generalHeaders.ConnectionClose = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C1AA File Offset: 0x0000A3AA
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000C1B7 File Offset: 0x0000A3B7
		[__DynamicallyInvokable]
		public DateTimeOffset? Date
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Date;
			}
			[__DynamicallyInvokable]
			set
			{
				this.generalHeaders.Date = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000C1C5 File Offset: 0x0000A3C5
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Pragma;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C1D2 File Offset: 0x0000A3D2
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<string> Trailer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Trailer;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000C1DF File Offset: 0x0000A3DF
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.TransferEncoding;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000C1F9 File Offset: 0x0000A3F9
		[__DynamicallyInvokable]
		public bool? TransferEncodingChunked
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.TransferEncodingChunked;
			}
			[__DynamicallyInvokable]
			set
			{
				this.generalHeaders.TransferEncodingChunked = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C207 File Offset: 0x0000A407
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Upgrade;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000C214 File Offset: 0x0000A414
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Via;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C221 File Offset: 0x0000A421
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Warning;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C22E File Offset: 0x0000A42E
		internal HttpResponseHeaders()
		{
			this.generalHeaders = new HttpGeneralHeaders(this);
			base.SetConfiguration(HttpResponseHeaders.parserStore, HttpResponseHeaders.invalidHeaders);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C254 File Offset: 0x0000A454
		static HttpResponseHeaders()
		{
			HttpResponseHeaders.parserStore.Add("Accept-Ranges", GenericHeaderParser.TokenListParser);
			HttpResponseHeaders.parserStore.Add("Age", TimeSpanHeaderParser.Parser);
			HttpResponseHeaders.parserStore.Add("ETag", GenericHeaderParser.SingleValueEntityTagParser);
			HttpResponseHeaders.parserStore.Add("Location", UriHeaderParser.RelativeOrAbsoluteUriParser);
			HttpResponseHeaders.parserStore.Add("Proxy-Authenticate", GenericHeaderParser.MultipleValueAuthenticationParser);
			HttpResponseHeaders.parserStore.Add("Retry-After", GenericHeaderParser.RetryConditionParser);
			HttpResponseHeaders.parserStore.Add("Server", ProductInfoHeaderParser.MultipleValueParser);
			HttpResponseHeaders.parserStore.Add("Vary", GenericHeaderParser.TokenListParser);
			HttpResponseHeaders.parserStore.Add("WWW-Authenticate", GenericHeaderParser.MultipleValueAuthenticationParser);
			HttpGeneralHeaders.AddParsers(HttpResponseHeaders.parserStore);
			HttpResponseHeaders.invalidHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HttpContentHeaders.AddKnownHeaders(HttpResponseHeaders.invalidHeaders);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C348 File Offset: 0x0000A548
		internal static void AddKnownHeaders(HashSet<string> headerSet)
		{
			headerSet.Add("Accept-Ranges");
			headerSet.Add("Age");
			headerSet.Add("ETag");
			headerSet.Add("Location");
			headerSet.Add("Proxy-Authenticate");
			headerSet.Add("Retry-After");
			headerSet.Add("Server");
			headerSet.Add("Vary");
			headerSet.Add("WWW-Authenticate");
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		internal override void AddHeaders(HttpHeaders sourceHeaders)
		{
			base.AddHeaders(sourceHeaders);
			HttpResponseHeaders httpResponseHeaders = sourceHeaders as HttpResponseHeaders;
			this.generalHeaders.AddSpecialsFrom(httpResponseHeaders.generalHeaders);
		}

		// Token: 0x04000150 RID: 336
		private static readonly Dictionary<string, HttpHeaderParser> parserStore = new Dictionary<string, HttpHeaderParser>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000151 RID: 337
		private static readonly HashSet<string> invalidHeaders;

		// Token: 0x04000152 RID: 338
		private HttpGeneralHeaders generalHeaders;

		// Token: 0x04000153 RID: 339
		private HttpHeaderValueCollection<string> acceptRanges;

		// Token: 0x04000154 RID: 340
		private HttpHeaderValueCollection<AuthenticationHeaderValue> wwwAuthenticate;

		// Token: 0x04000155 RID: 341
		private HttpHeaderValueCollection<AuthenticationHeaderValue> proxyAuthenticate;

		// Token: 0x04000156 RID: 342
		private HttpHeaderValueCollection<ProductInfoHeaderValue> server;

		// Token: 0x04000157 RID: 343
		private HttpHeaderValueCollection<string> vary;
	}
}
