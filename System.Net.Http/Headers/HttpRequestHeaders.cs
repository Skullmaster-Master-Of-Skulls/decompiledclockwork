using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x02000033 RID: 51
	[__DynamicallyInvokable]
	public sealed class HttpRequestHeaders : HttpHeaders
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000B8C6 File Offset: 0x00009AC6
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.accept == null)
				{
					this.accept = new HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue>("Accept", this);
				}
				return this.accept;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B8E7 File Offset: 0x00009AE7
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.acceptCharset == null)
				{
					this.acceptCharset = new HttpHeaderValueCollection<StringWithQualityHeaderValue>("Accept-Charset", this);
				}
				return this.acceptCharset;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B908 File Offset: 0x00009B08
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.acceptEncoding == null)
				{
					this.acceptEncoding = new HttpHeaderValueCollection<StringWithQualityHeaderValue>("Accept-Encoding", this);
				}
				return this.acceptEncoding;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B929 File Offset: 0x00009B29
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.acceptLanguage == null)
				{
					this.acceptLanguage = new HttpHeaderValueCollection<StringWithQualityHeaderValue>("Accept-Language", this);
				}
				return this.acceptLanguage;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B94A File Offset: 0x00009B4A
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000B95C File Offset: 0x00009B5C
		[__DynamicallyInvokable]
		public AuthenticationHeaderValue Authorization
		{
			[__DynamicallyInvokable]
			get
			{
				return (AuthenticationHeaderValue)base.GetParsedValues("Authorization");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Authorization", value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B96A File Offset: 0x00009B6A
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<NameValueWithParametersHeaderValue> Expect
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ExpectCore;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B974 File Offset: 0x00009B74
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		[__DynamicallyInvokable]
		public bool? ExpectContinue
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.ExpectCore.IsSpecialValueSet)
				{
					return new bool?(true);
				}
				if (this.expectContinueSet)
				{
					return new bool?(false);
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				bool? flag = value;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
				{
					this.expectContinueSet = true;
					this.ExpectCore.SetSpecialValue();
					return;
				}
				this.expectContinueSet = (value != null);
				this.ExpectCore.RemoveSpecialValue();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000BA00 File Offset: 0x00009C00
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000BA12 File Offset: 0x00009C12
		[__DynamicallyInvokable]
		public string From
		{
			[__DynamicallyInvokable]
			get
			{
				return (string)base.GetParsedValues("From");
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == string.Empty)
				{
					value = null;
				}
				if (value != null && !HeaderUtilities.IsValidEmailAddress(value))
				{
					throw new FormatException(SR.net_http_headers_invalid_from_header);
				}
				base.SetOrRemoveParsedValue("From", value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000BA46 File Offset: 0x00009C46
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000BA58 File Offset: 0x00009C58
		[__DynamicallyInvokable]
		public string Host
		{
			[__DynamicallyInvokable]
			get
			{
				return (string)base.GetParsedValues("Host");
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == string.Empty)
				{
					value = null;
				}
				string text = null;
				if (value != null && HttpRuleParser.GetHostLength(value, 0, false, out text) != value.Length)
				{
					throw new FormatException(SR.net_http_headers_invalid_host_header);
				}
				base.SetOrRemoveParsedValue("Host", value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfMatch
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.ifMatch == null)
				{
					this.ifMatch = new HttpHeaderValueCollection<EntityTagHeaderValue>("If-Match", this);
				}
				return this.ifMatch;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		[__DynamicallyInvokable]
		public DateTimeOffset? IfModifiedSince
		{
			[__DynamicallyInvokable]
			get
			{
				return HeaderUtilities.GetDateTimeOffsetValue("If-Modified-Since", this);
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("If-Modified-Since", value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<EntityTagHeaderValue> IfNoneMatch
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.ifNoneMatch == null)
				{
					this.ifNoneMatch = new HttpHeaderValueCollection<EntityTagHeaderValue>("If-None-Match", this);
				}
				return this.ifNoneMatch;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000BB05 File Offset: 0x00009D05
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000BB17 File Offset: 0x00009D17
		[__DynamicallyInvokable]
		public RangeConditionHeaderValue IfRange
		{
			[__DynamicallyInvokable]
			get
			{
				return (RangeConditionHeaderValue)base.GetParsedValues("If-Range");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("If-Range", value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000BB25 File Offset: 0x00009D25
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000BB32 File Offset: 0x00009D32
		[__DynamicallyInvokable]
		public DateTimeOffset? IfUnmodifiedSince
		{
			[__DynamicallyInvokable]
			get
			{
				return HeaderUtilities.GetDateTimeOffsetValue("If-Unmodified-Since", this);
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("If-Unmodified-Since", value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000BB48 File Offset: 0x00009D48
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000BB79 File Offset: 0x00009D79
		[__DynamicallyInvokable]
		public int? MaxForwards
		{
			[__DynamicallyInvokable]
			get
			{
				object parsedValues = base.GetParsedValues("Max-Forwards");
				if (parsedValues != null)
				{
					return new int?((int)parsedValues);
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Max-Forwards", value);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000BB8C File Offset: 0x00009D8C
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000BB9E File Offset: 0x00009D9E
		[__DynamicallyInvokable]
		public AuthenticationHeaderValue ProxyAuthorization
		{
			[__DynamicallyInvokable]
			get
			{
				return (AuthenticationHeaderValue)base.GetParsedValues("Proxy-Authorization");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Proxy-Authorization", value);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000BBAC File Offset: 0x00009DAC
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000BBBE File Offset: 0x00009DBE
		[__DynamicallyInvokable]
		public RangeHeaderValue Range
		{
			[__DynamicallyInvokable]
			get
			{
				return (RangeHeaderValue)base.GetParsedValues("Range");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Range", value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000BBCC File Offset: 0x00009DCC
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000BBDE File Offset: 0x00009DDE
		[__DynamicallyInvokable]
		public Uri Referrer
		{
			[__DynamicallyInvokable]
			get
			{
				return (Uri)base.GetParsedValues("Referer");
			}
			[__DynamicallyInvokable]
			set
			{
				base.SetOrRemoveParsedValue("Referer", value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000BBEC File Offset: 0x00009DEC
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> TE
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.te == null)
				{
					this.te = new HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue>("TE", this);
				}
				return this.te;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000BC0D File Offset: 0x00009E0D
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<ProductInfoHeaderValue> UserAgent
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.userAgent == null)
				{
					this.userAgent = new HttpHeaderValueCollection<ProductInfoHeaderValue>("User-Agent", this);
				}
				return this.userAgent;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000BC2E File Offset: 0x00009E2E
		private HttpHeaderValueCollection<NameValueWithParametersHeaderValue> ExpectCore
		{
			get
			{
				if (this.expect == null)
				{
					this.expect = new HttpHeaderValueCollection<NameValueWithParametersHeaderValue>("Expect", this, HeaderUtilities.ExpectContinue);
				}
				return this.expect;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000BC54 File Offset: 0x00009E54
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000BC61 File Offset: 0x00009E61
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000BC6F File Offset: 0x00009E6F
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<string> Connection
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Connection;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000BC7C File Offset: 0x00009E7C
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000BC89 File Offset: 0x00009E89
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000BC97 File Offset: 0x00009E97
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000BCA4 File Offset: 0x00009EA4
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000BCB2 File Offset: 0x00009EB2
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Pragma;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BCBF File Offset: 0x00009EBF
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<string> Trailer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Trailer;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000BCCC File Offset: 0x00009ECC
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.TransferEncoding;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BCD9 File Offset: 0x00009ED9
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000BCE6 File Offset: 0x00009EE6
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Upgrade;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000BD01 File Offset: 0x00009F01
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Via;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000BD0E File Offset: 0x00009F0E
		[__DynamicallyInvokable]
		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			[__DynamicallyInvokable]
			get
			{
				return this.generalHeaders.Warning;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000BD1B File Offset: 0x00009F1B
		internal HttpRequestHeaders()
		{
			this.generalHeaders = new HttpGeneralHeaders(this);
			base.SetConfiguration(HttpRequestHeaders.parserStore, HttpRequestHeaders.invalidHeaders);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000BD40 File Offset: 0x00009F40
		static HttpRequestHeaders()
		{
			HttpRequestHeaders.parserStore.Add("Accept", MediaTypeHeaderParser.MultipleValuesParser);
			HttpRequestHeaders.parserStore.Add("Accept-Charset", GenericHeaderParser.MultipleValueStringWithQualityParser);
			HttpRequestHeaders.parserStore.Add("Accept-Encoding", GenericHeaderParser.MultipleValueStringWithQualityParser);
			HttpRequestHeaders.parserStore.Add("Accept-Language", GenericHeaderParser.MultipleValueStringWithQualityParser);
			HttpRequestHeaders.parserStore.Add("Authorization", GenericHeaderParser.SingleValueAuthenticationParser);
			HttpRequestHeaders.parserStore.Add("Expect", GenericHeaderParser.MultipleValueNameValueWithParametersParser);
			HttpRequestHeaders.parserStore.Add("From", GenericHeaderParser.MailAddressParser);
			HttpRequestHeaders.parserStore.Add("Host", GenericHeaderParser.HostParser);
			HttpRequestHeaders.parserStore.Add("If-Match", GenericHeaderParser.MultipleValueEntityTagParser);
			HttpRequestHeaders.parserStore.Add("If-Modified-Since", DateHeaderParser.Parser);
			HttpRequestHeaders.parserStore.Add("If-None-Match", GenericHeaderParser.MultipleValueEntityTagParser);
			HttpRequestHeaders.parserStore.Add("If-Range", GenericHeaderParser.RangeConditionParser);
			HttpRequestHeaders.parserStore.Add("If-Unmodified-Since", DateHeaderParser.Parser);
			HttpRequestHeaders.parserStore.Add("Max-Forwards", Int32NumberHeaderParser.Parser);
			HttpRequestHeaders.parserStore.Add("Proxy-Authorization", GenericHeaderParser.SingleValueAuthenticationParser);
			HttpRequestHeaders.parserStore.Add("Range", GenericHeaderParser.RangeParser);
			HttpRequestHeaders.parserStore.Add("Referer", UriHeaderParser.RelativeOrAbsoluteUriParser);
			HttpRequestHeaders.parserStore.Add("TE", TransferCodingHeaderParser.MultipleValueWithQualityParser);
			HttpRequestHeaders.parserStore.Add("User-Agent", ProductInfoHeaderParser.MultipleValueParser);
			HttpGeneralHeaders.AddParsers(HttpRequestHeaders.parserStore);
			HttpRequestHeaders.invalidHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HttpContentHeaders.AddKnownHeaders(HttpRequestHeaders.invalidHeaders);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		internal static void AddKnownHeaders(HashSet<string> headerSet)
		{
			headerSet.Add("Accept");
			headerSet.Add("Accept-Charset");
			headerSet.Add("Accept-Encoding");
			headerSet.Add("Accept-Language");
			headerSet.Add("Authorization");
			headerSet.Add("Expect");
			headerSet.Add("From");
			headerSet.Add("Host");
			headerSet.Add("If-Match");
			headerSet.Add("If-Modified-Since");
			headerSet.Add("If-None-Match");
			headerSet.Add("If-Range");
			headerSet.Add("If-Unmodified-Since");
			headerSet.Add("Max-Forwards");
			headerSet.Add("Proxy-Authorization");
			headerSet.Add("Range");
			headerSet.Add("Referer");
			headerSet.Add("TE");
			headerSet.Add("User-Agent");
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		internal override void AddHeaders(HttpHeaders sourceHeaders)
		{
			base.AddHeaders(sourceHeaders);
			HttpRequestHeaders httpRequestHeaders = sourceHeaders as HttpRequestHeaders;
			this.generalHeaders.AddSpecialsFrom(httpRequestHeaders.generalHeaders);
			if (this.ExpectContinue == null)
			{
				this.ExpectContinue = httpRequestHeaders.ExpectContinue;
			}
		}

		// Token: 0x04000143 RID: 323
		private static readonly Dictionary<string, HttpHeaderParser> parserStore = new Dictionary<string, HttpHeaderParser>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000144 RID: 324
		private static readonly HashSet<string> invalidHeaders;

		// Token: 0x04000145 RID: 325
		private HttpGeneralHeaders generalHeaders;

		// Token: 0x04000146 RID: 326
		private HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> accept;

		// Token: 0x04000147 RID: 327
		private HttpHeaderValueCollection<NameValueWithParametersHeaderValue> expect;

		// Token: 0x04000148 RID: 328
		private bool expectContinueSet;

		// Token: 0x04000149 RID: 329
		private HttpHeaderValueCollection<EntityTagHeaderValue> ifMatch;

		// Token: 0x0400014A RID: 330
		private HttpHeaderValueCollection<EntityTagHeaderValue> ifNoneMatch;

		// Token: 0x0400014B RID: 331
		private HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> te;

		// Token: 0x0400014C RID: 332
		private HttpHeaderValueCollection<ProductInfoHeaderValue> userAgent;

		// Token: 0x0400014D RID: 333
		private HttpHeaderValueCollection<StringWithQualityHeaderValue> acceptCharset;

		// Token: 0x0400014E RID: 334
		private HttpHeaderValueCollection<StringWithQualityHeaderValue> acceptEncoding;

		// Token: 0x0400014F RID: 335
		private HttpHeaderValueCollection<StringWithQualityHeaderValue> acceptLanguage;
	}
}
