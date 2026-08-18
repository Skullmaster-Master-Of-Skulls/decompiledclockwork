using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002E3 RID: 739
	[Flags]
	public enum ePermissionForCourse
	{
		// Token: 0x04001337 RID: 4919
		NoPermission = -1,
		// Token: 0x04001338 RID: 4920
		PassiveAcceptAll = 0,
		// Token: 0x04001339 RID: 4921
		ReceiveEmails = 1,
		// Token: 0x0400133A RID: 4922
		AccessTestInfoOnline = 2,
		// Token: 0x0400133B RID: 4923
		AccessAccommodationLettersOnline = 4,
		// Token: 0x0400133C RID: 4924
		ReceiveEmailsAndAccessTestInfoOnline = 3,
		// Token: 0x0400133D RID: 4925
		ReceiveEmailsAndAccessTestInfoAndAccommodationLettersOnline = 7,
		// Token: 0x0400133E RID: 4926
		ReceiveEmailsAndAccessAccommodationLettersOnline = 5,
		// Token: 0x0400133F RID: 4927
		AccessTestInfoAndAccommodationLettersOnline = 6
	}
}
