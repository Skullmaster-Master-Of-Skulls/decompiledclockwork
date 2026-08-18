using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000808 RID: 2056
	internal enum FramingRecordType
	{
		// Token: 0x0400300E RID: 12302
		Version,
		// Token: 0x0400300F RID: 12303
		Mode,
		// Token: 0x04003010 RID: 12304
		Via,
		// Token: 0x04003011 RID: 12305
		KnownEncoding,
		// Token: 0x04003012 RID: 12306
		ExtensibleEncoding,
		// Token: 0x04003013 RID: 12307
		UnsizedEnvelope,
		// Token: 0x04003014 RID: 12308
		SizedEnvelope,
		// Token: 0x04003015 RID: 12309
		End,
		// Token: 0x04003016 RID: 12310
		Fault,
		// Token: 0x04003017 RID: 12311
		UpgradeRequest,
		// Token: 0x04003018 RID: 12312
		UpgradeResponse,
		// Token: 0x04003019 RID: 12313
		PreambleAck,
		// Token: 0x0400301A RID: 12314
		PreambleEnd
	}
}
