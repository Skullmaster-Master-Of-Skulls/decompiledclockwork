using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000046 RID: 70
	internal class TransferCodingHeaderParser : BaseHeaderParser
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000E6E6 File Offset: 0x0000C8E6
		private TransferCodingHeaderParser(bool supportsMultipleValues, Func<TransferCodingHeaderValue> transferCodingCreator) : base(supportsMultipleValues)
		{
			this.transferCodingCreator = transferCodingCreator;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
			TransferCodingHeaderValue transferCodingHeaderValue = null;
			int transferCodingLength = TransferCodingHeaderValue.GetTransferCodingLength(value, startIndex, this.transferCodingCreator, out transferCodingHeaderValue);
			parsedValue = transferCodingHeaderValue;
			return transferCodingLength;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E71C File Offset: 0x0000C91C
		private static TransferCodingHeaderValue CreateTransferCoding()
		{
			return new TransferCodingHeaderValue();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E723 File Offset: 0x0000C923
		private static TransferCodingHeaderValue CreateTransferCodingWithQuality()
		{
			return new TransferCodingWithQualityHeaderValue();
		}

		// Token: 0x0400017B RID: 379
		private Func<TransferCodingHeaderValue> transferCodingCreator;

		// Token: 0x0400017C RID: 380
		internal static readonly TransferCodingHeaderParser SingleValueParser = new TransferCodingHeaderParser(false, new Func<TransferCodingHeaderValue>(TransferCodingHeaderParser.CreateTransferCoding));

		// Token: 0x0400017D RID: 381
		internal static readonly TransferCodingHeaderParser MultipleValueParser = new TransferCodingHeaderParser(true, new Func<TransferCodingHeaderValue>(TransferCodingHeaderParser.CreateTransferCoding));

		// Token: 0x0400017E RID: 382
		internal static readonly TransferCodingHeaderParser SingleValueWithQualityParser = new TransferCodingHeaderParser(false, new Func<TransferCodingHeaderValue>(TransferCodingHeaderParser.CreateTransferCodingWithQuality));

		// Token: 0x0400017F RID: 383
		internal static readonly TransferCodingHeaderParser MultipleValueWithQualityParser = new TransferCodingHeaderParser(true, new Func<TransferCodingHeaderValue>(TransferCodingHeaderParser.CreateTransferCodingWithQuality));
	}
}
