using System;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities.Settings.Adapters
{
	// Token: 0x020001DF RID: 479
	public static class SettingAdapter
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x00015D24 File Offset: 0x00013F24
		public static SettingDataAttribute GetSettingAttribute(this Setting setting)
		{
			SettingDataAttribute settingDataAttribute = null;
			Type type = setting.GetType();
			FieldInfo field = type.GetField(setting.ToString());
			SettingDataAttribute[] array = field.GetCustomAttributes(typeof(SettingDataAttribute), false) as SettingDataAttribute[];
			bool flag = array != null && array.Length != 0;
			if (flag)
			{
				settingDataAttribute = array[0];
			}
			else
			{
				string name = Enum.GetName(typeof(Setting), setting);
				bool flag2 = name != null;
				if (flag2)
				{
					string[] names = Enum.GetNames(typeof(Group));
					foreach (string text in names)
					{
						bool flag3 = name.IndexOf(text + "_") == 0;
						if (flag3)
						{
							Group group = (Group)Enum.Parse(typeof(Group), text);
							settingDataAttribute = new SettingDataAttribute(setting.ToString(), group, SettingSemantic.TEXT);
						}
					}
				}
			}
			bool flag4 = settingDataAttribute == null;
			if (flag4)
			{
				settingDataAttribute = new SettingDataAttribute(setting.ToString(), Group.UNKNOWN, SettingSemantic.TEXT);
			}
			return settingDataAttribute;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00015E58 File Offset: 0x00014058
		public static Group GetGroup(this Setting setting)
		{
			Type type = setting.GetType();
			FieldInfo field = type.GetField(setting.ToString());
			SettingDataAttribute[] array = field.GetCustomAttributes(typeof(SettingDataAttribute), false) as SettingDataAttribute[];
			bool flag = array != null && array.Length != 0;
			Group result;
			if (flag)
			{
				result = array[0].Group;
			}
			else
			{
				string name = Enum.GetName(typeof(Setting), setting);
				bool flag2 = name != null;
				if (flag2)
				{
					string[] names = Enum.GetNames(typeof(Group));
					foreach (string text in names)
					{
						bool flag3 = name.IndexOf(text + "_") == 0;
						if (flag3)
						{
							return (Group)Enum.Parse(typeof(Group), text);
						}
					}
				}
				result = Group.UNKNOWN;
			}
			return result;
		}
	}
}
