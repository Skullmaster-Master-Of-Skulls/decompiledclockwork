using System;

namespace ReportFunctions.ClockWorkDataSync
{
	// Token: 0x02000047 RID: 71
	public enum DataSyncActionType
	{
		// Token: 0x0400023D RID: 573
		Unknown,
		// Token: 0x0400023E RID: 574
		Course_AddToClockWork,
		// Token: 0x0400023F RID: 575
		Course_RegisterWithStudent,
		// Token: 0x04000240 RID: 576
		Course_DropWithStudent,
		// Token: 0x04000241 RID: 577
		Course_AddTimeTableItem,
		// Token: 0x04000242 RID: 578
		Course_RemoveTimeTableItem,
		// Token: 0x04000243 RID: 579
		Course_AddInstructor,
		// Token: 0x04000244 RID: 580
		Course_RemoveInstructor,
		// Token: 0x04000245 RID: 581
		Course_UpdateInstructor,
		// Token: 0x04000246 RID: 582
		Course_UnDropWithStudent,
		// Token: 0x04000247 RID: 583
		Course_UpdateCourse,
		// Token: 0x04000248 RID: 584
		Course_CreateInstructor,
		// Token: 0x04000249 RID: 585
		Course_UpdateInstructorUsername,
		// Token: 0x0400024A RID: 586
		ServiceProviderDataItem_AddUpdateClockWork
	}
}
