using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	public enum eSpecialControlTypeGroup
	{
		// Token: 0x04001553 RID: 5459
		[SpecialControlTypeGroup]
		Unknown,
		// Token: 0x04001554 RID: 5460
		[SpecialControlTypeGroup("Student (general)")]
		StudentGeneral,
		// Token: 0x04001555 RID: 5461
		[SpecialControlTypeGroup("Tutoring")]
		Tutoring
	}
}
