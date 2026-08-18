using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000037 RID: 55
	internal class MediaTypeHeaderParser : BaseHeaderParser
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		private MediaTypeHeaderParser(bool supportsMultipleValues, Func<MediaTypeHeaderValue> mediaTypeCreator) : base(supportsMultipleValues)
		{
			this.supportsMultipleValues = supportsMultipleValues;
			this.mediaTypeCreator = mediaTypeCreator;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = null;
			int mediaTypeLength = MediaTypeHeaderValue.GetMediaTypeLength(value, startIndex, this.mediaTypeCreator, out mediaTypeHeaderValue);
			parsedValue = mediaTypeHeaderValue;
			return mediaTypeLength;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000C520 File Offset: 0x0000A720
		private static MediaTypeHeaderValue CreateMediaType()
		{
			return new MediaTypeHeaderValue();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C527 File Offset: 0x0000A727
		private static MediaTypeHeaderValue CreateMediaTypeWithQuality()
		{
			return new MediaTypeWithQualityHeaderValue();
		}

		// Token: 0x0400015A RID: 346
		private bool supportsMultipleValues;

		// Token: 0x0400015B RID: 347
		private Func<MediaTypeHeaderValue> mediaTypeCreator;

		// Token: 0x0400015C RID: 348
		internal static readonly MediaTypeHeaderParser SingleValueParser = new MediaTypeHeaderParser(false, new Func<MediaTypeHeaderValue>(MediaTypeHeaderParser.CreateMediaType));

		// Token: 0x0400015D RID: 349
		internal static readonly MediaTypeHeaderParser SingleValueWithQualityParser = new MediaTypeHeaderParser(false, new Func<MediaTypeHeaderValue>(MediaTypeHeaderParser.CreateMediaTypeWithQuality));

		// Token: 0x0400015E RID: 350
		internal static readonly MediaTypeHeaderParser MultipleValuesParser = new MediaTypeHeaderParser(true, new Func<MediaTypeHeaderValue>(MediaTypeHeaderParser.CreateMediaTypeWithQuality));
	}
}
