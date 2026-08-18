using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007FD RID: 2045
	internal class EncodedVia : EncodedFramingRecord
	{
		// Token: 0x06004D0F RID: 19727 RVA: 0x00119B07 File Offset: 0x00117D07
		public EncodedVia(string via) : base(FramingRecordType.Via, via)
		{
		}
	}
}
