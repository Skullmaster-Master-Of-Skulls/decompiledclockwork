using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	public enum eOldUserSettingGroup
	{
		// Token: 0x04000379 RID: 889
		[OldUserSettingGroup("Unknown", IsHidden = true, IconName = "box_white", OrderNum = 0)]
		Unknown,
		// Token: 0x0400037A RID: 890
		[OldUserSettingGroup("Miscellaneous", IconName = "index", OrderNum = 10)]
		Misc,
		// Token: 0x0400037B RID: 891
		[OldUserSettingGroup("Forms", IconName = "form_blue", OrderNum = 20)]
		Forms,
		// Token: 0x0400037C RID: 892
		[OldUserSettingGroup("Ribbon", IconName = "bullet_square_yellow", OrderNum = 30)]
		Buttons,
		// Token: 0x0400037D RID: 893
		[OldUserSettingGroup("Appointments", IconName = "clock", OrderNum = 40)]
		Appointments,
		// Token: 0x0400037E RID: 894
		[OldUserSettingGroup("Users", IconName = "users1", OrderNum = 50)]
		Users,
		// Token: 0x0400037F RID: 895
		[OldUserSettingGroup("Students", IconName = "id_card", OrderNum = 60)]
		Students,
		// Token: 0x04000380 RID: 896
		[OldUserSettingGroup("PersonalOptions", IconName = "preferences", OrderNum = 70)]
		PersonalOptions,
		// Token: 0x04000381 RID: 897
		[OldUserSettingGroup("Accommodations", IconName = "presentation_chart", OrderNum = 80)]
		Accommodations,
		// Token: 0x04000382 RID: 898
		[OldUserSettingGroup("Exams", IconName = "graphics_tablet", OrderNum = 100)]
		Exams,
		// Token: 0x04000383 RID: 899
		[OldUserSettingGroup("Courses", IconName = "book_open", OrderNum = 110)]
		Courses,
		// Token: 0x04000384 RID: 900
		[OldUserSettingGroup("Alternative Format", IconName = "document_out", OrderNum = 120)]
		AlternativeFormat,
		// Token: 0x04000385 RID: 901
		[OldUserSettingGroup("Inventory", IconName = "catalogs", OrderNum = 130)]
		Inventory,
		// Token: 0x04000386 RID: 902
		[OldUserSettingGroup("System", IconName = "gear_time", OrderNum = 140)]
		System
	}
}
