using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Accommodations
{
	// Token: 0x020003BC RID: 956
	[Serializable]
	public enum eAccommodationLetterInstructorViewType
	{
		// Token: 0x040016E3 RID: 5859
		Unknown,
		// Token: 0x040016E4 RID: 5860
		SelfRegApproved,
		// Token: 0x040016E5 RID: 5861
		LetterWasGenerated,
		// Token: 0x040016E6 RID: 5862
		SelfRegApprovedOrLetterWasGenerated,
		// Token: 0x040016E7 RID: 5863
		StudentHasNonExpiredAccommodations
	}
}
