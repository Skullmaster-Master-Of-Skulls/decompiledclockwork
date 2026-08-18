using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkSnapshot
{
	// Token: 0x02000449 RID: 1097
	[Flags]
	public enum eSnapshotDataGroup
	{
		// Token: 0x04001924 RID: 6436
		None = 0,
		// Token: 0x04001925 RID: 6437
		DynamicForms = 1,
		// Token: 0x04001926 RID: 6438
		AppointmentCancelled = 2,
		// Token: 0x04001927 RID: 6439
		AppointmentIcon = 4,
		// Token: 0x04001928 RID: 6440
		AppointmentShowTimeAs = 8,
		// Token: 0x04001929 RID: 6441
		AppointmentTypes = 16,
		// Token: 0x0400192A RID: 6442
		Availability = 32,
		// Token: 0x0400192B RID: 6443
		Courses = 64,
		// Token: 0x0400192C RID: 6444
		Templates = 128,
		// Token: 0x0400192D RID: 6445
		Groups = 256,
		// Token: 0x0400192E RID: 6446
		Misc = 512,
		// Token: 0x0400192F RID: 6447
		Settings = 1024,
		// Token: 0x04001930 RID: 6448
		SettingsGroups = 2048,
		// Token: 0x04001931 RID: 6449
		WebSettings = 4096,
		// Token: 0x04001932 RID: 6450
		Permissions = 8192,
		// Token: 0x04001933 RID: 6451
		PermissionsGroups = 16384,
		// Token: 0x04001934 RID: 6452
		TestsExams = 32768,
		// Token: 0x04001935 RID: 6453
		Departments = 65536,
		// Token: 0x04001936 RID: 6454
		Surveys = 131072,
		// Token: 0x04001937 RID: 6455
		DataAllPeople = 262144,
		// Token: 0x04001938 RID: 6456
		DataRoomsOnly = 524288,
		// Token: 0x04001939 RID: 6457
		DataStaffOnly = 1048576,
		// Token: 0x0400193A RID: 6458
		DataPeopleGroupMatchings = 2097152,
		// Token: 0x0400193B RID: 6459
		All = 4194303
	}
}
