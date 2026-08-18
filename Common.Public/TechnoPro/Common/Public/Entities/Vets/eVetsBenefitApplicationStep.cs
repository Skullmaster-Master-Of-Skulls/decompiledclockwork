using System;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public enum eVetsBenefitApplicationStep
	{
		// Token: 0x0400027F RID: 639
		[VetsBenefitApplicationStep("Register")]
		Registration,
		// Token: 0x04000280 RID: 640
		[VetsBenefitApplicationStep("Chapter")]
		ChapterSelection,
		// Token: 0x04000281 RID: 641
		[VetsBenefitApplicationStep("Application")]
		Application,
		// Token: 0x04000282 RID: 642
		[VetsBenefitApplicationStep("Agreement")]
		Agreement,
		// Token: 0x04000283 RID: 643
		[VetsBenefitApplicationStep("Status")]
		Status
	}
}
