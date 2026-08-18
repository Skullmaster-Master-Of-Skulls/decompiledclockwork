using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200080B RID: 2059
	internal enum FramingEncodingType
	{
		// Token: 0x04003023 RID: 12323
		Soap11Utf8,
		// Token: 0x04003024 RID: 12324
		Soap11Utf16,
		// Token: 0x04003025 RID: 12325
		Soap11Utf16FFFE,
		// Token: 0x04003026 RID: 12326
		Soap12Utf8,
		// Token: 0x04003027 RID: 12327
		Soap12Utf16,
		// Token: 0x04003028 RID: 12328
		Soap12Utf16FFFE,
		// Token: 0x04003029 RID: 12329
		MTOM,
		// Token: 0x0400302A RID: 12330
		Binary,
		// Token: 0x0400302B RID: 12331
		BinarySession
	}
}
