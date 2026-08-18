using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007FF RID: 2047
	internal class EncodedFault : EncodedFramingRecord
	{
		// Token: 0x06004D11 RID: 19729 RVA: 0x00119B1C File Offset: 0x00117D1C
		public EncodedFault(string fault) : base(FramingRecordType.Fault, fault)
		{
		}
	}
}
