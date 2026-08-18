using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000032 RID: 50
	[Flags]
	internal enum EsSignatureLineFlags
	{
		// Token: 0x0400028E RID: 654
		IsSignatureLine = 1,
		// Token: 0x0400028F RID: 655
		SigSetupSignInstSet = 2,
		// Token: 0x04000290 RID: 656
		SigSetupAllowComments = 4,
		// Token: 0x04000291 RID: 657
		SigSetupShowSignDate = 8,
		// Token: 0x04000292 RID: 658
		UseIsSignatureLine = 65536,
		// Token: 0x04000293 RID: 659
		UseSigSetupSignInstSet = 131072,
		// Token: 0x04000294 RID: 660
		UseSigSetupAllowComments = 262144,
		// Token: 0x04000295 RID: 661
		UseSigSetupShowSignDate = 524288
	}
}
