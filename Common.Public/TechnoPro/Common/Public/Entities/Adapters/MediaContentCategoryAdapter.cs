using System;
using System.Reflection;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C4 RID: 1476
	public static class MediaContentCategoryAdapter
	{
		// Token: 0x06002F81 RID: 12161 RVA: 0x00036804 File Offset: 0x00034A04
		public static DynamicFormSettingAttribute GetDynamicFormSettingAttribute(this eMediaContentCategory category)
		{
			Type type = category.GetType();
			FieldInfo field = type.GetField(category.ToString());
			DynamicFormSettingAttribute[] array = field.GetCustomAttributes(typeof(DynamicFormSettingAttribute), false) as DynamicFormSettingAttribute[];
			bool flag = array != null && array.Length != 0;
			DynamicFormSettingAttribute result;
			if (flag)
			{
				result = array[0];
			}
			else
			{
				result = new DynamicFormSettingAttribute(eSettingCode.SETTING_AlternativeFormat_AlternateTextBookDynamicFormId);
			}
			return result;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x00036874 File Offset: 0x00034A74
		public static eSettingCode GetDynamicFormSetting(this eMediaContentCategory category)
		{
			return category.GetDynamicFormSettingAttribute().DynamicFormSetting;
		}
	}
}
