using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018CA RID: 6346
	internal class RadFilterTypeHelper
	{
		// Token: 0x0600F590 RID: 62864 RVA: 0x0037C10F File Offset: 0x0037A30F
		public static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600F591 RID: 62865 RVA: 0x0037C130 File Offset: 0x0037A330
		public static Type GetNonNullableType(Type type)
		{
			if (!RadFilterTypeHelper.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x0600F592 RID: 62866 RVA: 0x0037C144 File Offset: 0x0037A344
		public static bool IsNumericType(Type type)
		{
			return RadFilterTypeHelper.GetNumericTypeKind(type) != 0;
		}

		// Token: 0x0600F593 RID: 62867 RVA: 0x0037C152 File Offset: 0x0037A352
		public static bool IsDateType(Type type)
		{
			return RadFilterTypeHelper.GetDateTypeKind(type) != 0;
		}

		// Token: 0x0600F594 RID: 62868 RVA: 0x0037C160 File Offset: 0x0037A360
		public static int GetDateTypeKind(Type type)
		{
			type = RadFilterTypeHelper.GetNonNullableType(type);
			if (type == typeof(DateTime))
			{
				return 1;
			}
			if (type == typeof(TimeSpan))
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x0600F595 RID: 62869 RVA: 0x0037C194 File Offset: 0x0037A394
		public static int GetNumericTypeKind(Type type)
		{
			type = RadFilterTypeHelper.GetNonNullableType(type);
			if (type.IsEnum)
			{
				return 0;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return 2;
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return 3;
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return 1;
			default:
				return 0;
			}
		}
	}
}
