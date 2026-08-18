using System;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities.Settings.Adapters
{
	// Token: 0x020001DE RID: 478
	public static class GroupAdapter
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x00015BC0 File Offset: 0x00013DC0
		public static GroupDataAttribute GetGroupAttribute(this Group group)
		{
			Type type = group.GetType();
			FieldInfo field = type.GetField(group.ToString());
			GroupDataAttribute[] array = field.GetCustomAttributes(typeof(GroupDataAttribute), false) as GroupDataAttribute[];
			bool flag = array != null && array.Length != 0;
			GroupDataAttribute result;
			if (flag)
			{
				result = array[0];
			}
			else
			{
				result = new GroupDataAttribute(group.ToString());
			}
			return result;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00015C38 File Offset: 0x00013E38
		public static string GetName(this Group group)
		{
			return group.GetGroupAttribute().Name;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00015C58 File Offset: 0x00013E58
		public static string GetDescription(this Group group)
		{
			return group.GetGroupAttribute().Description;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00015C78 File Offset: 0x00013E78
		public static LookupSetting GetModifiedTimeSetting(this Group group)
		{
			Setting[] array = (Setting[])Enum.GetValues(typeof(Setting));
			foreach (Setting setting in array)
			{
				bool flag = setting.GetGroup() == group;
				if (flag)
				{
					Type type = setting.GetType();
					FieldInfo field = type.GetField(setting.ToString());
					DatetimeModifiedSettingAttribute[] array3 = field.GetCustomAttributes(typeof(DatetimeModifiedSettingAttribute), false) as DatetimeModifiedSettingAttribute[];
					bool flag2 = array3 != null && array3.Length != 0;
					if (flag2)
					{
						return new LookupSetting(setting);
					}
				}
			}
			return null;
		}
	}
}
