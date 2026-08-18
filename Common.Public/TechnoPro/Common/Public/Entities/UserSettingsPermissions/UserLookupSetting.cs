using System;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000126 RID: 294
	public enum UserLookupSetting
	{
		// Token: 0x04000373 RID: 883
		[UserSettingData("", "", "", UserSettingGroup.USERINTERFACE, SettingSemantic.REFERENCE_ARRAY)]
		USERINTERFACE_LegacyFormsToShowInRibbonBar = 99734,
		// Token: 0x04000374 RID: 884
		[UserSettingData("", "", "", UserSettingGroup.USERINTERFACE, SettingSemantic.BOOLEAN)]
		USERINTERFACE_BookAllTestsExamsAsTentative = 99733,
		// Token: 0x04000375 RID: 885
		[UserSettingData("", "", "", UserSettingGroup.USERINTERFACE, SettingSemantic.REFERENCE_ARRAY)]
		USERS_GroupWithStudentForDropList = 355,
		// Token: 0x04000376 RID: 886
		[UserSettingData("", "", "", UserSettingGroup.USERINTERFACE, SettingSemantic.TEXT)]
		USERS_GroupWithStudentForDropList_SQL = 377,
		// Token: 0x04000377 RID: 887
		[UserSettingData("", "", "", UserSettingGroup.USERINTERFACE, SettingSemantic.REFERENCE_ARRAY)]
		USERS_AllowedAppTypes = 359
	}
}
