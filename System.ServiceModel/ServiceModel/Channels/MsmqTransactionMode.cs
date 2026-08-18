using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008FA RID: 2298
	internal enum MsmqTransactionMode
	{
		// Token: 0x040035EE RID: 13806
		None,
		// Token: 0x040035EF RID: 13807
		Single,
		// Token: 0x040035F0 RID: 13808
		CurrentOrSingle,
		// Token: 0x040035F1 RID: 13809
		CurrentOrNone,
		// Token: 0x040035F2 RID: 13810
		CurrentOrThrow
	}
}
