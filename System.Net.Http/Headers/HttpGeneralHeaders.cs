using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200002F RID: 47
	internal sealed class HttpGeneralHeaders
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A0C9 File Offset: 0x000082C9
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000A0E0 File Offset: 0x000082E0
		public CacheControlHeaderValue CacheControl
		{
			get
			{
				return (CacheControlHeaderValue)this.parent.GetParsedValues("Cache-Control");
			}
			set
			{
				this.parent.SetOrRemoveParsedValue("Cache-Control", value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A0F3 File Offset: 0x000082F3
		public HttpHeaderValueCollection<string> Connection
		{
			get
			{
				return this.ConnectionCore;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A0FC File Offset: 0x000082FC
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000A138 File Offset: 0x00008338
		public bool? ConnectionClose
		{
			get
			{
				if (this.ConnectionCore.IsSpecialValueSet)
				{
					return new bool?(true);
				}
				if (this.connectionCloseSet)
				{
					return new bool?(false);
				}
				return null;
			}
			set
			{
				bool? flag = value;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
				{
					this.connectionCloseSet = true;
					this.ConnectionCore.SetSpecialValue();
					return;
				}
				this.connectionCloseSet = (value != null);
				this.ConnectionCore.RemoveSpecialValue();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A188 File Offset: 0x00008388
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000A19A File Offset: 0x0000839A
		public DateTimeOffset? Date
		{
			get
			{
				return HeaderUtilities.GetDateTimeOffsetValue("Date", this.parent);
			}
			set
			{
				this.parent.SetOrRemoveParsedValue("Date", value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A1B2 File Offset: 0x000083B2
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			get
			{
				if (this.pragma == null)
				{
					this.pragma = new HttpHeaderValueCollection<NameValueHeaderValue>("Pragma", this.parent);
				}
				return this.pragma;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A1D8 File Offset: 0x000083D8
		public HttpHeaderValueCollection<string> Trailer
		{
			get
			{
				if (this.trailer == null)
				{
					this.trailer = new HttpHeaderValueCollection<string>("Trailer", this.parent, HeaderUtilities.TokenValidator);
				}
				return this.trailer;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A203 File Offset: 0x00008403
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			get
			{
				return this.TransferEncodingCore;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A20C File Offset: 0x0000840C
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000A248 File Offset: 0x00008448
		public bool? TransferEncodingChunked
		{
			get
			{
				if (this.TransferEncodingCore.IsSpecialValueSet)
				{
					return new bool?(true);
				}
				if (this.transferEncodingChunkedSet)
				{
					return new bool?(false);
				}
				return null;
			}
			set
			{
				bool? flag = value;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
				{
					this.transferEncodingChunkedSet = true;
					this.TransferEncodingCore.SetSpecialValue();
					return;
				}
				this.transferEncodingChunkedSet = (value != null);
				this.TransferEncodingCore.RemoveSpecialValue();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000A298 File Offset: 0x00008498
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			get
			{
				if (this.upgrade == null)
				{
					this.upgrade = new HttpHeaderValueCollection<ProductHeaderValue>("Upgrade", this.parent);
				}
				return this.upgrade;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A2BE File Offset: 0x000084BE
		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			get
			{
				if (this.via == null)
				{
					this.via = new HttpHeaderValueCollection<ViaHeaderValue>("Via", this.parent);
				}
				return this.via;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			get
			{
				if (this.warning == null)
				{
					this.warning = new HttpHeaderValueCollection<WarningHeaderValue>("Warning", this.parent);
				}
				return this.warning;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A30A File Offset: 0x0000850A
		private HttpHeaderValueCollection<string> ConnectionCore
		{
			get
			{
				if (this.connection == null)
				{
					this.connection = new HttpHeaderValueCollection<string>("Connection", this.parent, "close", HeaderUtilities.TokenValidator);
				}
				return this.connection;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A33A File Offset: 0x0000853A
		private HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncodingCore
		{
			get
			{
				if (this.transferEncoding == null)
				{
					this.transferEncoding = new HttpHeaderValueCollection<TransferCodingHeaderValue>("Transfer-Encoding", this.parent, HeaderUtilities.TransferEncodingChunked);
				}
				return this.transferEncoding;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A365 File Offset: 0x00008565
		internal HttpGeneralHeaders(HttpHeaders parent)
		{
			this.parent = parent;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A374 File Offset: 0x00008574
		internal static void AddParsers(Dictionary<string, HttpHeaderParser> parserStore)
		{
			parserStore.Add("Cache-Control", CacheControlHeaderParser.Parser);
			parserStore.Add("Connection", GenericHeaderParser.TokenListParser);
			parserStore.Add("Date", DateHeaderParser.Parser);
			parserStore.Add("Pragma", GenericHeaderParser.MultipleValueNameValueParser);
			parserStore.Add("Trailer", GenericHeaderParser.TokenListParser);
			parserStore.Add("Transfer-Encoding", TransferCodingHeaderParser.MultipleValueParser);
			parserStore.Add("Upgrade", GenericHeaderParser.MultipleValueProductParser);
			parserStore.Add("Via", GenericHeaderParser.MultipleValueViaParser);
			parserStore.Add("Warning", GenericHeaderParser.MultipleValueWarningParser);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A414 File Offset: 0x00008614
		internal static void AddKnownHeaders(HashSet<string> headerSet)
		{
			headerSet.Add("Cache-Control");
			headerSet.Add("Connection");
			headerSet.Add("Date");
			headerSet.Add("Pragma");
			headerSet.Add("Trailer");
			headerSet.Add("Transfer-Encoding");
			headerSet.Add("Upgrade");
			headerSet.Add("Via");
			headerSet.Add("Warning");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A490 File Offset: 0x00008690
		internal void AddSpecialsFrom(HttpGeneralHeaders sourceHeaders)
		{
			if (this.TransferEncodingChunked == null)
			{
				this.TransferEncodingChunked = sourceHeaders.TransferEncodingChunked;
			}
			if (this.ConnectionClose == null)
			{
				this.ConnectionClose = sourceHeaders.ConnectionClose;
			}
		}

		// Token: 0x0400012F RID: 303
		private HttpHeaderValueCollection<string> connection;

		// Token: 0x04000130 RID: 304
		private HttpHeaderValueCollection<string> trailer;

		// Token: 0x04000131 RID: 305
		private HttpHeaderValueCollection<TransferCodingHeaderValue> transferEncoding;

		// Token: 0x04000132 RID: 306
		private HttpHeaderValueCollection<ProductHeaderValue> upgrade;

		// Token: 0x04000133 RID: 307
		private HttpHeaderValueCollection<ViaHeaderValue> via;

		// Token: 0x04000134 RID: 308
		private HttpHeaderValueCollection<WarningHeaderValue> warning;

		// Token: 0x04000135 RID: 309
		private HttpHeaderValueCollection<NameValueHeaderValue> pragma;

		// Token: 0x04000136 RID: 310
		private HttpHeaders parent;

		// Token: 0x04000137 RID: 311
		private bool transferEncodingChunkedSet;

		// Token: 0x04000138 RID: 312
		private bool connectionCloseSet;
	}
}
