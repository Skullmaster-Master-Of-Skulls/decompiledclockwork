using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000025 RID: 37
	[Flags]
	internal enum EsUriFlags
	{
		// Token: 0x04000230 RID: 560
		HasMoniker = 1,
		// Token: 0x04000231 RID: 561
		IsAbsolute = 2,
		// Token: 0x04000232 RID: 562
		SiteGaveDisplayName = 4,
		// Token: 0x04000233 RID: 563
		HasLocationStr = 8,
		// Token: 0x04000234 RID: 564
		HasDisplayName = 16,
		// Token: 0x04000235 RID: 565
		HasGUID = 32,
		// Token: 0x04000236 RID: 566
		HasCreationTime = 64,
		// Token: 0x04000237 RID: 567
		HasFrameName = 128,
		// Token: 0x04000238 RID: 568
		MonikerSavedAsStr = 256,
		// Token: 0x04000239 RID: 569
		AbsFromGetdataRel = 512
	}
}
