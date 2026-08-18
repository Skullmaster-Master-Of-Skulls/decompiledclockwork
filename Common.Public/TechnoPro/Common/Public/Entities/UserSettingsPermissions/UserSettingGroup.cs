using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000125 RID: 293
	public enum UserSettingGroup
	{
		// Token: 0x04000365 RID: 869
		[UserSettingGroupData("", true)]
		APPOINTMENTS = 10000,
		// Token: 0x04000366 RID: 870
		[UserSettingGroupData("", true)]
		MISCELLANEOUS = 20000,
		// Token: 0x04000367 RID: 871
		[UserSettingGroupData("", true)]
		FORMS = 30000,
		// Token: 0x04000368 RID: 872
		[UserSettingGroupData("", true)]
		BUTTONS = 40000,
		// Token: 0x04000369 RID: 873
		[UserSettingGroupData("", true)]
		USERS = 50000,
		// Token: 0x0400036A RID: 874
		[UserSettingGroupData("", true)]
		STUDENTS = 60000,
		// Token: 0x0400036B RID: 875
		[UserSettingGroupData("", true)]
		PERSONALOPTIONS = 70000,
		// Token: 0x0400036C RID: 876
		[UserSettingGroupData("", true)]
		ACCOMMODATIONS = 80000,
		// Token: 0x0400036D RID: 877
		[UserSettingGroupData("", true)]
		SYSTEM = 90000,
		// Token: 0x0400036E RID: 878
		[UserSettingGroupData("", true)]
		EXAMS = 100000,
		// Token: 0x0400036F RID: 879
		[UserSettingGroupData("", true)]
		COURSES = 110000,
		// Token: 0x04000370 RID: 880
		[UserSettingGroupData("", true)]
		USERINTERFACE = 120000,
		// Token: 0x04000371 RID: 881
		[UserSettingGroupData("Unknown", IsActive = false, Description = "Unknown group")]
		UNKNOWN = 0
	}
}
