using System;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public enum eNotAllowedCode
	{
		// Token: 0x04000072 RID: 114
		Unknown,
		// Token: 0x04000073 RID: 115
		InvalidMinMaxDatesForTestBooking,
		// Token: 0x04000074 RID: 116
		InvalidMinMaxDatesForExamBooking,
		// Token: 0x04000075 RID: 117
		NoCoursesAvailableToBookBecauseSpecialAccBanForTestBooking,
		// Token: 0x04000076 RID: 118
		NoCoursesAvailableToBookBecauseSpecialAccBanForExamBooking
	}
}
