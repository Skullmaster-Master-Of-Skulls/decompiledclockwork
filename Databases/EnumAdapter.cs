using System;
using System.Reflection;

namespace Databases
{
	// Token: 0x02000004 RID: 4
	internal static class EnumAdapter
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000272C File Offset: 0x0000092C
		public static T GetAttribute<T>(this Enum item) where T : Attribute
		{
			Type type = item.GetType();
			FieldInfo field = type.GetField(item.ToString());
			T[] array = field.GetCustomAttributes(typeof(T), false) as T[];
			return (array != null && array.Length != 0) ? array[0] : default(T);
		}
	}
}
