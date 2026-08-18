using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000352 RID: 850
	[Serializable]
	public enum eSpecialControlType
	{
		// Token: 0x0400154A RID: 5450
		[SpecialControlType]
		Unknown,
		// Token: 0x0400154B RID: 5451
		[SpecialControlType(eSpecialControlTypeGroup.Tutoring, "Tutor specialization", "", new eControlCode[]
		{
			eControlCode.TextBox,
			eControlCode.MyTextBox,
			eControlCode.MaskedTextBox
		})]
		TutorSpecialization = 3,
		// Token: 0x0400154C RID: 5452
		[SpecialControlType(eSpecialControlTypeGroup.Tutoring, "Tutor public note", "This note can be seen by students", new eControlCode[]
		{
			eControlCode.TextBox,
			eControlCode.MyTextBox,
			eControlCode.MaskedTextBox
		})]
		TutorPublicNote
	}
}
