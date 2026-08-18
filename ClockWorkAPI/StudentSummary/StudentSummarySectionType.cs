using System;

namespace ClockWorkAPI.StudentSummary
{
	// Token: 0x02000095 RID: 149
	public enum StudentSummarySectionType
	{
		// Token: 0x040003DB RID: 987
		none,
		// Token: 0x040003DC RID: 988
		PerStudentForm,
		// Token: 0x040003DD RID: 989
		PerAppointmentForm,
		// Token: 0x040003DE RID: 990
		PerDateForm = 4,
		// Token: 0x040003DF RID: 991
		Accommodations = 8,
		// Token: 0x040003E0 RID: 992
		Courses = 16,
		// Token: 0x040003E1 RID: 993
		Appointments = 32,
		// Token: 0x040003E2 RID: 994
		Groups = 64
	}
}
