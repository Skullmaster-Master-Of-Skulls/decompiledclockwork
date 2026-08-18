using System;
using System.Reflection;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.Adapters
{
	// Token: 0x02000134 RID: 308
	public static class UserSettingGroupAdapter
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public static UserSettingGroupDataAttribute GetGroupAttribute(this UserSettingGroup group)
		{
			Type type = group.GetType();
			FieldInfo field = type.GetField(group.ToString());
			UserSettingGroupDataAttribute[] array = field.GetCustomAttributes(typeof(UserSettingGroupDataAttribute), false) as UserSettingGroupDataAttribute[];
			bool flag = array != null && array.Length != 0;
			UserSettingGroupDataAttribute result;
			if (flag)
			{
				result = array[0];
			}
			else
			{
				result = new UserSettingGroupDataAttribute(group.ToString());
			}
			return result;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00010268 File Offset: 0x0000E468
		public static string GetName(this UserSettingGroup group)
		{
			return group.GetGroupAttribute().Name;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00010288 File Offset: 0x0000E488
		public static string GetDescription(this UserSettingGroup group)
		{
			return group.GetGroupAttribute().Description;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public static LookupUserSetting GetModifiedTimeSetting(this UserSettingGroup group)
		{
			UserLookupSetting[] array = (UserLookupSetting[])Enum.GetValues(typeof(UserLookupSetting));
			foreach (UserLookupSetting userLookupSetting in array)
			{
				bool flag = userLookupSetting.GetGroup() == group;
				if (flag)
				{
					Type type = userLookupSetting.GetType();
					FieldInfo field = type.GetField(userLookupSetting.ToString());
					DatetimeModifiedSettingAttribute[] array3 = field.GetCustomAttributes(typeof(DatetimeModifiedSettingAttribute), false) as DatetimeModifiedSettingAttribute[];
					bool flag2 = array3 != null && array3.Length != 0;
					if (flag2)
					{
						return new LookupUserSetting(userLookupSetting);
					}
				}
			}
			return null;
		}
	}
}
