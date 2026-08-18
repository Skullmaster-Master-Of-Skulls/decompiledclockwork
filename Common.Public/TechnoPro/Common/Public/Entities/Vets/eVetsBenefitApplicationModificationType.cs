using System;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x020000FB RID: 251
	[Flags]
	[Serializable]
	public enum eVetsBenefitApplicationModificationType
	{
		// Token: 0x04000274 RID: 628
		Unknown = 0,
		// Token: 0x04000275 RID: 629
		UpdatedFormData = 1,
		// Token: 0x04000276 RID: 630
		UpdatedChapter = 2,
		// Token: 0x04000277 RID: 631
		UpdatedStudentAgreeCompleted = 4,
		// Token: 0x04000278 RID: 632
		UpdatedBenAppCompleted = 8,
		// Token: 0x04000279 RID: 633
		UpdatedRegistrationCompleted = 16,
		// Token: 0x0400027A RID: 634
		UpdatedPreferredStep = 32,
		// Token: 0x0400027B RID: 635
		UpdatedFinalStatus = 64,
		// Token: 0x0400027C RID: 636
		UpdatedAssignedCounsellor = 128,
		// Token: 0x0400027D RID: 637
		UpdatedProgress = 256
	}
}
