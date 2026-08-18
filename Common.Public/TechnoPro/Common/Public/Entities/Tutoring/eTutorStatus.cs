using System;

namespace TechnoPro.Common.Public.Entities.Tutoring
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public enum eTutorStatus
	{
		// Token: 0x0400066B RID: 1643
		Unknown,
		// Token: 0x0400066C RID: 1644
		NotATutor,
		// Token: 0x0400066D RID: 1645
		TutorNotActive,
		// Token: 0x0400066E RID: 1646
		TutorActiveNeedsConfidentiality,
		// Token: 0x0400066F RID: 1647
		TutorActive
	}
}
