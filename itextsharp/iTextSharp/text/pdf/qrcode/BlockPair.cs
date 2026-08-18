using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x0200039B RID: 923
	public sealed class BlockPair
	{
		// Token: 0x06001FF5 RID: 8181 RVA: 0x000BECD7 File Offset: 0x000BDCD7
		internal BlockPair(ByteArray data, ByteArray errorCorrection)
		{
			this.dataBytes = data;
			this.errorCorrectionBytes = errorCorrection;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000BECED File Offset: 0x000BDCED
		public ByteArray GetDataBytes()
		{
			return this.dataBytes;
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000BECF5 File Offset: 0x000BDCF5
		public ByteArray GetErrorCorrectionBytes()
		{
			return this.errorCorrectionBytes;
		}

		// Token: 0x04001603 RID: 5635
		private ByteArray dataBytes;

		// Token: 0x04001604 RID: 5636
		private ByteArray errorCorrectionBytes;
	}
}
