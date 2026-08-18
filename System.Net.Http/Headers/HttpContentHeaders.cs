using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200002E RID: 46
	[__DynamicallyInvokable]
	public sealed class HttpContentHeaders : HttpHeaders
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00009D1A File Offset: 0x00007F1A
		[__DynamicallyInvokable]
		public ICollection<string> Allow
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.allow == null)
				{
					this.allow = new HttpHeaderValueCollection<string>("Allow", this, HeaderUtilities.TokenValidator);
				}
				return this.allow;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00009D40 File Offset: 0x00007F40
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00009D52 File Offset: 0x00007F52
		[__DynamicallyInvokable]
		public ContentDispositionHeaderValue ContentDisposition
		{
			[__DynamicallyInvokable]
			get
			{
				return (ContentDispositionHeaderValue)base.GetParsedValues("Content-Disposition");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Content-Disposition", value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00009D60 File Offset: 0x00007F60
		[__DynamicallyInvokable]
		public ICollection<string> ContentEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.contentEncoding == null)
				{
					this.contentEncoding = new HttpHeaderValueCollection<string>("Content-Encoding", this, HeaderUtilities.TokenValidator);
				}
				return this.contentEncoding;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00009D86 File Offset: 0x00007F86
		[__DynamicallyInvokable]
		public ICollection<string> ContentLanguage
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.contentLanguage == null)
				{
					this.contentLanguage = new HttpHeaderValueCollection<string>("Content-Language", this, HeaderUtilities.TokenValidator);
				}
				return this.contentLanguage;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00009DAC File Offset: 0x00007FAC
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00009E16 File Offset: 0x00008016
		[__DynamicallyInvokable]
		public long? ContentLength
		{
			[__DynamicallyInvokable]
			get
			{
				object parsedValues = base.GetParsedValues("Content-Length");
				if (!this.contentLengthSet && parsedValues == null)
				{
					long? result = this.calculateLengthFunc();
					if (result != null)
					{
						base.SetParsedValue("Content-Length", result.Value);
					}
					return result;
				}
				if (parsedValues == null)
				{
					return null;
				}
				return new long?((long)parsedValues);
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Content-Length", value);
				this.contentLengthSet = true;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00009E30 File Offset: 0x00008030
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00009E42 File Offset: 0x00008042
		[__DynamicallyInvokable]
		public Uri ContentLocation
		{
			[__DynamicallyInvokable]
			get
			{
				return (Uri)base.GetParsedValues("Content-Location");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Content-Location", value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00009E50 File Offset: 0x00008050
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00009E62 File Offset: 0x00008062
		[__DynamicallyInvokable]
		public byte[] ContentMD5
		{
			[__DynamicallyInvokable]
			get
			{
				return (byte[])base.GetParsedValues("Content-MD5");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Content-MD5", value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009E70 File Offset: 0x00008070
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00009E82 File Offset: 0x00008082
		[__DynamicallyInvokable]
		public ContentRangeHeaderValue ContentRange
		{
			[__DynamicallyInvokable]
			get
			{
				return (ContentRangeHeaderValue)base.GetParsedValues("Content-Range");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Content-Range", value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009E90 File Offset: 0x00008090
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00009EA2 File Offset: 0x000080A2
		[__DynamicallyInvokable]
		public MediaTypeHeaderValue ContentType
		{
			[__DynamicallyInvokable]
			get
			{
				return (MediaTypeHeaderValue)base.GetParsedValues("Content-Type");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Content-Type", value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00009EB0 File Offset: 0x000080B0
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00009EBD File Offset: 0x000080BD
		[__DynamicallyInvokable]
		public DateTimeOffset? Expires
		{
			[__DynamicallyInvokable]
			get
			{
				return HeaderUtilities.GetDateTimeOffsetValue("Expires", this);
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Expires", value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009ED0 File Offset: 0x000080D0
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00009EDD File Offset: 0x000080DD
		[__DynamicallyInvokable]
		public DateTimeOffset? LastModified
		{
			[__DynamicallyInvokable]
			get
			{
				return HeaderUtilities.GetDateTimeOffsetValue("Last-Modified", this);
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Last-Modified", value);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009EF0 File Offset: 0x000080F0
		internal HttpContentHeaders(Func<long?> calculateLengthFunc)
		{
			this.calculateLengthFunc = calculateLengthFunc;
			base.SetConfiguration(HttpContentHeaders.parserStore, HttpContentHeaders.invalidHeaders);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009F10 File Offset: 0x00008110
		static HttpContentHeaders()
		{
			HttpContentHeaders.parserStore.Add("Allow", GenericHeaderParser.TokenListParser);
			HttpContentHeaders.parserStore.Add("Content-Disposition", GenericHeaderParser.ContentDispositionParser);
			HttpContentHeaders.parserStore.Add("Content-Encoding", GenericHeaderParser.TokenListParser);
			HttpContentHeaders.parserStore.Add("Content-Language", GenericHeaderParser.TokenListParser);
			HttpContentHeaders.parserStore.Add("Content-Length", Int64NumberHeaderParser.Parser);
			HttpContentHeaders.parserStore.Add("Content-Location", UriHeaderParser.RelativeOrAbsoluteUriParser);
			HttpContentHeaders.parserStore.Add("Content-MD5", ByteArrayHeaderParser.Parser);
			HttpContentHeaders.parserStore.Add("Content-Range", GenericHeaderParser.ContentRangeParser);
			HttpContentHeaders.parserStore.Add("Content-Type", MediaTypeHeaderParser.SingleValueParser);
			HttpContentHeaders.parserStore.Add("Expires", DateHeaderParser.Parser);
			HttpContentHeaders.parserStore.Add("Last-Modified", DateHeaderParser.Parser);
			HttpContentHeaders.invalidHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HttpRequestHeaders.AddKnownHeaders(HttpContentHeaders.invalidHeaders);
			HttpResponseHeaders.AddKnownHeaders(HttpContentHeaders.invalidHeaders);
			HttpGeneralHeaders.AddKnownHeaders(HttpContentHeaders.invalidHeaders);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A038 File Offset: 0x00008238
		internal static void AddKnownHeaders(HashSet<string> headerSet)
		{
			headerSet.Add("Allow");
			headerSet.Add("Content-Disposition");
			headerSet.Add("Content-Encoding");
			headerSet.Add("Content-Language");
			headerSet.Add("Content-Length");
			headerSet.Add("Content-Location");
			headerSet.Add("Content-MD5");
			headerSet.Add("Content-Range");
			headerSet.Add("Content-Type");
			headerSet.Add("Expires");
			headerSet.Add("Last-Modified");
		}

		// Token: 0x04000128 RID: 296
		private static readonly Dictionary<string, HttpHeaderParser> parserStore = new Dictionary<string, HttpHeaderParser>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000129 RID: 297
		private static readonly HashSet<string> invalidHeaders;

		// Token: 0x0400012A RID: 298
		private Func<long?> calculateLengthFunc;

		// Token: 0x0400012B RID: 299
		private bool contentLengthSet;

		// Token: 0x0400012C RID: 300
		private HttpHeaderValueCollection<string> allow;

		// Token: 0x0400012D RID: 301
		private HttpHeaderValueCollection<string> contentEncoding;

		// Token: 0x0400012E RID: 302
		private HttpHeaderValueCollection<string> contentLanguage;
	}
}
