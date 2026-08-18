using System;
using System.Reflection;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.Adapters
{
	// Token: 0x02000135 RID: 309
	public static class SettingAdapter
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00010354 File Offset: 0x0000E554
		public static UserSettingDataAttribute GetSettingAttribute(this UserLookupSetting setting)
		{
			UserSettingDataAttribute userSettingDataAttribute = null;
			Type type = setting.GetType();
			FieldInfo field = type.GetField(setting.ToString());
			UserSettingDataAttribute[] array = field.GetCustomAttributes(typeof(UserSettingDataAttribute), false) as UserSettingDataAttribute[];
			bool flag = array != null && array.Length != 0;
			if (flag)
			{
				userSettingDataAttribute = array[0];
			}
			else
			{
				string name = Enum.GetName(typeof(UserLookupSetting), setting);
				bool flag2 = name != null;
				if (flag2)
				{
					string[] names = Enum.GetNames(typeof(UserSettingGroup));
					foreach (string text in names)
					{
						bool flag3 = name.IndexOf(text + "_") == 0;
						if (flag3)
						{
							UserSettingGroup group = (UserSettingGroup)Enum.Parse(typeof(UserSettingGroup), text);
							userSettingDataAttribute = new UserSettingDataAttribute(setting.ToString(), group, SettingSemantic.TEXT);
						}
					}
				}
			}
			bool flag4 = userSettingDataAttribute == null;
			if (flag4)
			{
				userSettingDataAttribute = new UserSettingDataAttribute(setting.ToString(), UserSettingGroup.UNKNOWN, SettingSemantic.TEXT);
			}
			return userSettingDataAttribute;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00010488 File Offset: 0x0000E688
		public static UserSettingGroup GetGroup(this UserLookupSetting setting)
		{
			Type type = setting.GetType();
			FieldInfo field = type.GetField(setting.ToString());
			UserSettingDataAttribute[] array = field.GetCustomAttributes(typeof(UserSettingDataAttribute), false) as UserSettingDataAttribute[];
			bool flag = array != null && array.Length != 0;
			UserSettingGroup result;
			if (flag)
			{
				result = array[0].Group;
			}
			else
			{
				string name = Enum.GetName(typeof(UserLookupSetting), setting);
				bool flag2 = name != null;
				if (flag2)
				{
					string[] names = Enum.GetNames(typeof(UserSettingGroup));
					foreach (string text in names)
					{
						bool flag3 = name.IndexOf(text + "_") == 0;
						if (flag3)
						{
							return (UserSettingGroup)Enum.Parse(typeof(UserSettingGroup), text);
						}
					}
				}
				result = UserSettingGroup.UNKNOWN;
			}
			return result;
		}
	}
}
