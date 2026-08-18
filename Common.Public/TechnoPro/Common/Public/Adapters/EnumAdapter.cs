using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005ED RID: 1517
	public static class EnumAdapter
	{
		// Token: 0x060030CC RID: 12492 RVA: 0x00042948 File Offset: 0x00040B48
		public static bool HasFlag(this Enum variable, Enum value)
		{
			bool flag = variable == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = value == null;
				if (flag2)
				{
					throw new ArgumentNullException("value");
				}
				bool flag3 = !Enum.IsDefined(variable.GetType(), value);
				if (flag3)
				{
					throw new ArgumentException(string.Format("Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.", value.GetType(), variable.GetType()));
				}
				ulong num = Convert.ToUInt64(value);
				result = ((Convert.ToUInt64(variable) & num) == num);
			}
			return result;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000429C0 File Offset: 0x00040BC0
		public static string MaskToString<T>(this Enum mask)
		{
			bool flag = !typeof(T).IsSubclassOf(typeof(Enum));
			if (flag)
			{
				throw new ArgumentException();
			}
			return string.Join(",", (from e in Enum.GetValues(typeof(T)).Cast<Enum>().Where(new Func<Enum, bool>(mask.HasFlag))
			select Enum.GetName(typeof(T), e)).ToArray<string>());
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x00042A50 File Offset: 0x00040C50
		public static T GetAttribute<T>(this Enum item) where T : Attribute
		{
			Type type = item.GetType();
			FieldInfo field = type.GetField(item.ToString());
			T[] array = field.GetCustomAttributes(typeof(T), false) as T[];
			return (array != null && array.Length != 0) ? array[0] : default(T);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x00042AA8 File Offset: 0x00040CA8
		public static IList<T> SplitEnumValues<T>(this string commaSeparatedValues) where T : struct
		{
			bool flag = string.IsNullOrEmpty(commaSeparatedValues);
			IList<T> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] source = commaSeparatedValues.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				result = (from value in source
				where Enum.IsDefined(typeof(T), value.Trim())
				select (T)((object)Enum.Parse(typeof(T), value))).ToList<T>();
			}
			return result;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x00042B2C File Offset: 0x00040D2C
		public static T ParseEnumFromIntString<T>(this string intAsString) where T : struct
		{
			T t = default(T);
			int num;
			bool flag = string.IsNullOrEmpty(intAsString) || !int.TryParse(intAsString, out num) || num < 1 || !Enum.IsDefined(typeof(T), num);
			T result;
			if (flag)
			{
				result = t;
			}
			else
			{
				object obj = num;
				result = (T)((object)obj);
			}
			return result;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x00042B90 File Offset: 0x00040D90
		public static string IntEnumToString<T>(this T enumValue) where T : struct
		{
			object obj = enumValue;
			return ((int)obj).ToString();
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x00042BB8 File Offset: 0x00040DB8
		public static string ToDisplayString<T>(this T enumValue) where T : struct
		{
			return enumValue.ToString().Replace('_', ' ');
		}
	}
}
