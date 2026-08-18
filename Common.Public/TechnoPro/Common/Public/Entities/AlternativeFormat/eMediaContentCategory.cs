using System;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000572 RID: 1394
	[Serializable]
	public enum eMediaContentCategory
	{
		// Token: 0x04001F8C RID: 8076
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_AlternateTextBookDynamicFormId)]
		AlternateTextBook,
		// Token: 0x04001F8D RID: 8077
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_AudioFileDynamicFormId)]
		AudioFile,
		// Token: 0x04001F8E RID: 8078
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_VideoFileDynamicFormId)]
		VideoFile,
		// Token: 0x04001F8F RID: 8079
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_CoursePackDynamicFormId)]
		CoursePack,
		// Token: 0x04001F90 RID: 8080
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_CoursePackDynamicFormId)]
		Document,
		// Token: 0x04001F91 RID: 8081
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_CoursePackDynamicFormId)]
		Article,
		// Token: 0x04001F92 RID: 8082
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_CoursePackDynamicFormId)]
		Exam,
		// Token: 0x04001F93 RID: 8083
		[DynamicFormSetting(eSettingCode.SETTING_AlternativeFormat_CoursePackDynamicFormId)]
		Other
	}
}
