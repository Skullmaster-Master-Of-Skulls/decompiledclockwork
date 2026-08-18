using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C2 RID: 1218
	internal enum NodeQNameType : byte
	{
		// Token: 0x0400252D RID: 9517
		Empty,
		// Token: 0x0400252E RID: 9518
		Name,
		// Token: 0x0400252F RID: 9519
		Namespace,
		// Token: 0x04002530 RID: 9520
		Standard,
		// Token: 0x04002531 RID: 9521
		NameWildcard,
		// Token: 0x04002532 RID: 9522
		NamespaceWildcard = 8,
		// Token: 0x04002533 RID: 9523
		Wildcard = 12
	}
}
