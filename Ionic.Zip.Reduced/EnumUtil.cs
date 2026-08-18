using System;
using System.ComponentModel;
using System.Reflection;

namespace Ionic
{
	// Token: 0x02000023 RID: 35
	internal sealed class EnumUtil
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003ECA File Offset: 0x000020CA
		private EnumUtil()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal static string GetDescription(Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length > 0)
			{
				return array[0].Description;
			}
			return value.ToString();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003F1F File Offset: 0x0000211F
		internal static object Parse(Type enumType, string stringRepresentation)
		{
			return EnumUtil.Parse(enumType, stringRepresentation, false);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F2C File Offset: 0x0000212C
		internal static object Parse(Type enumType, string stringRepresentation, bool ignoreCase)
		{
			if (ignoreCase)
			{
				stringRepresentation = stringRepresentation.ToLower();
			}
			foreach (object obj in Enum.GetValues(enumType))
			{
				Enum @enum = (Enum)obj;
				string text = EnumUtil.GetDescription(@enum);
				if (ignoreCase)
				{
					text = text.ToLower();
				}
				if (text == stringRepresentation)
				{
					return @enum;
				}
			}
			return Enum.Parse(enumType, stringRepresentation, ignoreCase);
		}
	}
}
