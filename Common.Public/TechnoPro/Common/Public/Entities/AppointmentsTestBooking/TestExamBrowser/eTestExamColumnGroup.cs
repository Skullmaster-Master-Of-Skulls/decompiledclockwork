using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser
{
	// Token: 0x02000518 RID: 1304
	[Flags]
	[Serializable]
	public enum eTestExamColumnGroup
	{
		// Token: 0x04001D08 RID: 7432
		None = 0,
		// Token: 0x04001D09 RID: 7433
		BaseColumns = 1,
		// Token: 0x04001D0A RID: 7434
		InvigilatorInfo = 2,
		// Token: 0x04001D0B RID: 7435
		AlternateContactInfo = 4,
		// Token: 0x04001D0C RID: 7436
		InstructorInfo = 8,
		// Token: 0x04001D0D RID: 7437
		CourseInfo = 16,
		// Token: 0x04001D0E RID: 7438
		ClassDateTime = 32,
		// Token: 0x04001D0F RID: 7439
		ActualDateTime = 64,
		// Token: 0x04001D10 RID: 7440
		Accommodations = 128,
		// Token: 0x04001D11 RID: 7441
		InstructorContactedInfo = 256,
		// Token: 0x04001D12 RID: 7442
		TestPickedUpInfo = 512,
		// Token: 0x04001D13 RID: 7443
		SittingInfo = 1024,
		// Token: 0x04001D14 RID: 7444
		StudentReportedClassTime = 2048,
		// Token: 0x04001D15 RID: 7445
		AssignedAdvisor = 4096,
		// Token: 0x04001D16 RID: 7446
		BreakMinutes = 8192,
		// Token: 0x04001D17 RID: 7447
		All = 16384
	}
}
