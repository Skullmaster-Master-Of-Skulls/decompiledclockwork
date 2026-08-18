using System;
using System.Collections;

namespace System.Net.Http.Headers
{
	// Token: 0x0200002C RID: 44
	internal sealed class GenericHeaderParser : BaseHeaderParser
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000093DB File Offset: 0x000075DB
		public override IEqualityComparer Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000093E3 File Offset: 0x000075E3
		private GenericHeaderParser(bool supportsMultipleValues, GenericHeaderParser.GetParsedValueLengthDelegate getParsedValueLength) : this(supportsMultipleValues, getParsedValueLength, null)
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000093EE File Offset: 0x000075EE
		private GenericHeaderParser(bool supportsMultipleValues, GenericHeaderParser.GetParsedValueLengthDelegate getParsedValueLength, IEqualityComparer comparer) : base(supportsMultipleValues)
		{
			this.getParsedValueLength = getParsedValueLength;
			this.comparer = comparer;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009405 File Offset: 0x00007605
		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
			return this.getParsedValueLength(value, startIndex, out parsedValue);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009418 File Offset: 0x00007618
		private static int ParseNameValue(string value, int startIndex, out object parsedValue)
		{
			NameValueHeaderValue nameValueHeaderValue = null;
			int nameValueLength = NameValueHeaderValue.GetNameValueLength(value, startIndex, out nameValueHeaderValue);
			parsedValue = nameValueHeaderValue;
			return nameValueLength;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009438 File Offset: 0x00007638
		private static int ParseProduct(string value, int startIndex, out object parsedValue)
		{
			ProductHeaderValue productHeaderValue = null;
			int productLength = ProductHeaderValue.GetProductLength(value, startIndex, out productHeaderValue);
			parsedValue = productHeaderValue;
			return productLength;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009458 File Offset: 0x00007658
		private static int ParseSingleEntityTag(string value, int startIndex, out object parsedValue)
		{
			EntityTagHeaderValue entityTagHeaderValue = null;
			parsedValue = null;
			int entityTagLength = EntityTagHeaderValue.GetEntityTagLength(value, startIndex, out entityTagHeaderValue);
			if (entityTagHeaderValue == EntityTagHeaderValue.Any)
			{
				return 0;
			}
			parsedValue = entityTagHeaderValue;
			return entityTagLength;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009484 File Offset: 0x00007684
		private static int ParseMultipleEntityTags(string value, int startIndex, out object parsedValue)
		{
			EntityTagHeaderValue entityTagHeaderValue = null;
			int entityTagLength = EntityTagHeaderValue.GetEntityTagLength(value, startIndex, out entityTagHeaderValue);
			parsedValue = entityTagHeaderValue;
			return entityTagLength;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000094A4 File Offset: 0x000076A4
		private static int ParseMailAddress(string value, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (HttpRuleParser.ContainsInvalidNewLine(value, startIndex))
			{
				return 0;
			}
			string text = value.Substring(startIndex);
			if (!HeaderUtilities.IsValidEmailAddress(text))
			{
				return 0;
			}
			parsedValue = text;
			return text.Length;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000094DC File Offset: 0x000076DC
		private static int ParseHost(string value, int startIndex, out object parsedValue)
		{
			string text = null;
			int hostLength = HttpRuleParser.GetHostLength(value, startIndex, false, out text);
			parsedValue = text;
			return hostLength;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000094FC File Offset: 0x000076FC
		private static int ParseTokenList(string value, int startIndex, out object parsedValue)
		{
			int tokenLength = HttpRuleParser.GetTokenLength(value, startIndex);
			parsedValue = value.Substring(startIndex, tokenLength);
			return tokenLength;
		}

		// Token: 0x04000108 RID: 264
		internal static readonly HttpHeaderParser HostParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseHost), StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000109 RID: 265
		internal static readonly HttpHeaderParser TokenListParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseTokenList), StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400010A RID: 266
		internal static readonly HttpHeaderParser SingleValueNameValueWithParametersParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(NameValueWithParametersHeaderValue.GetNameValueWithParametersLength));

		// Token: 0x0400010B RID: 267
		internal static readonly HttpHeaderParser MultipleValueNameValueWithParametersParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(NameValueWithParametersHeaderValue.GetNameValueWithParametersLength));

		// Token: 0x0400010C RID: 268
		internal static readonly HttpHeaderParser SingleValueNameValueParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseNameValue));

		// Token: 0x0400010D RID: 269
		internal static readonly HttpHeaderParser MultipleValueNameValueParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseNameValue));

		// Token: 0x0400010E RID: 270
		internal static readonly HttpHeaderParser MailAddressParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseMailAddress));

		// Token: 0x0400010F RID: 271
		internal static readonly HttpHeaderParser SingleValueProductParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseProduct));

		// Token: 0x04000110 RID: 272
		internal static readonly HttpHeaderParser MultipleValueProductParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseProduct));

		// Token: 0x04000111 RID: 273
		internal static readonly HttpHeaderParser RangeConditionParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(RangeConditionHeaderValue.GetRangeConditionLength));

		// Token: 0x04000112 RID: 274
		internal static readonly HttpHeaderParser SingleValueAuthenticationParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(AuthenticationHeaderValue.GetAuthenticationLength));

		// Token: 0x04000113 RID: 275
		internal static readonly HttpHeaderParser MultipleValueAuthenticationParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(AuthenticationHeaderValue.GetAuthenticationLength));

		// Token: 0x04000114 RID: 276
		internal static readonly HttpHeaderParser RangeParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(RangeHeaderValue.GetRangeLength));

		// Token: 0x04000115 RID: 277
		internal static readonly HttpHeaderParser RetryConditionParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(RetryConditionHeaderValue.GetRetryConditionLength));

		// Token: 0x04000116 RID: 278
		internal static readonly HttpHeaderParser ContentRangeParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(ContentRangeHeaderValue.GetContentRangeLength));

		// Token: 0x04000117 RID: 279
		internal static readonly HttpHeaderParser ContentDispositionParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(ContentDispositionHeaderValue.GetDispositionTypeLength));

		// Token: 0x04000118 RID: 280
		internal static readonly HttpHeaderParser SingleValueStringWithQualityParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(StringWithQualityHeaderValue.GetStringWithQualityLength));

		// Token: 0x04000119 RID: 281
		internal static readonly HttpHeaderParser MultipleValueStringWithQualityParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(StringWithQualityHeaderValue.GetStringWithQualityLength));

		// Token: 0x0400011A RID: 282
		internal static readonly HttpHeaderParser SingleValueEntityTagParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseSingleEntityTag));

		// Token: 0x0400011B RID: 283
		internal static readonly HttpHeaderParser MultipleValueEntityTagParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(GenericHeaderParser.ParseMultipleEntityTags));

		// Token: 0x0400011C RID: 284
		internal static readonly HttpHeaderParser SingleValueViaParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(ViaHeaderValue.GetViaLength));

		// Token: 0x0400011D RID: 285
		internal static readonly HttpHeaderParser MultipleValueViaParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(ViaHeaderValue.GetViaLength));

		// Token: 0x0400011E RID: 286
		internal static readonly HttpHeaderParser SingleValueWarningParser = new GenericHeaderParser(false, new GenericHeaderParser.GetParsedValueLengthDelegate(WarningHeaderValue.GetWarningLength));

		// Token: 0x0400011F RID: 287
		internal static readonly HttpHeaderParser MultipleValueWarningParser = new GenericHeaderParser(true, new GenericHeaderParser.GetParsedValueLengthDelegate(WarningHeaderValue.GetWarningLength));

		// Token: 0x04000120 RID: 288
		private GenericHeaderParser.GetParsedValueLengthDelegate getParsedValueLength;

		// Token: 0x04000121 RID: 289
		private IEqualityComparer comparer;

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x06000458 RID: 1112
		private delegate int GetParsedValueLengthDelegate(string value, int startIndex, out object parsedValue);
	}
}
