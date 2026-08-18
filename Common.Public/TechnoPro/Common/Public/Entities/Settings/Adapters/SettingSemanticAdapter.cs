using System;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities.Settings.Adapters
{
	// Token: 0x020001E0 RID: 480
	public static class SettingSemanticAdapter
	{
		// Token: 0x06000DC0 RID: 3520 RVA: 0x00015F54 File Offset: 0x00014154
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
