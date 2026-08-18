using System;
using System.Reflection;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.Adapters
{
	// Token: 0x02000136 RID: 310
	public static class SettingSemanticAdapter
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x00010584 File Offset: 0x0000E784
		public static Type SemanticSettingType(this SettingSemantic sem)
		{
			Type type = sem.GetType();
			FieldInfo field = type.GetField(sem.ToString());
			SemanticTypeAttribute[] array = field.GetCustomAttributes(typeof(SemanticTypeAttribute), false) as SemanticTypeAttribute[];
			bool flag = array != null && array.Length != 0;
			Type result;
			if (flag)
			{
				result = array[0].SystemType;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
