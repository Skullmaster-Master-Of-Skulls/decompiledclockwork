using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007FE RID: 2046
	internal class EncodedUpgrade : EncodedFramingRecord
	{
		// Token: 0x06004D10 RID: 19728 RVA: 0x00119B11 File Offset: 0x00117D11
		public EncodedUpgrade(string contentType) : base(FramingRecordType.UpgradeRequest, contentType)
		{
		}
	}
}
