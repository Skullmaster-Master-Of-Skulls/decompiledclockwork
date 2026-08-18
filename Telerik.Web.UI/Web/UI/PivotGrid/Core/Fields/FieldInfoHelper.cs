using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CA9 RID: 3241
	internal static class FieldInfoHelper
	{
		// Token: 0x06007985 RID: 31109 RVA: 0x001BEA9C File Offset: 0x001BCC9C
		public static FieldRoles GetRoleForType(Type fieldType)
		{
			IEnumerable<Type> source = new List<Type>
			{
				typeof(Enum),
				typeof(char)
			};
			FieldRoles result = FieldRoles.Row;
			if (FieldInfoHelper.IsNumericType(fieldType))
			{
				result = FieldRoles.Value;
			}
			else if (source.Any((Type t) => fieldType == t || fieldType.IsSubclassOf(t)))
			{
				result = FieldRoles.Column;
			}
			return result;
		}

		// Token: 0x06007986 RID: 31110 RVA: 0x001BEB08 File Offset: 0x001BCD08
		public static bool IsNumericType(Type type)
		{
			return type == typeof(double) || type == typeof(double?) || type == typeof(int) || type == typeof(int?) || type == typeof(byte) || type == typeof(byte?) || type == typeof(short) || type == typeof(short?) || type == typeof(decimal) || type == typeof(decimal?) || type == typeof(float) || type == typeof(float?) || type == typeof(long) || type == typeof(long?) || type == typeof(uint) || type == typeof(uint?) || type == typeof(sbyte) || type == typeof(sbyte?) || type == typeof(ushort) || type == typeof(ushort?) || type == typeof(ulong) || type == typeof(ulong?);
		}
	}
}
