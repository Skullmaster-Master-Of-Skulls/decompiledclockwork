using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200123F RID: 4671
	internal class TreeListTypeHelper
	{
		// Token: 0x0600C0C5 RID: 49349 RVA: 0x002AE990 File Offset: 0x002ACB90
		public static bool IsBindableType(Type type)
		{
			type = TreeListTypeHelper.GetNonNullableType(type);
			return !type.IsEnum && (type.IsPrimitive || !(type != typeof(string)) || !(type != typeof(DateTime)) || !(type != typeof(TimeSpan)) || !(type != typeof(decimal)) || !(type != typeof(Guid)) || (type.IsValueType && type.IsGenericType && type.GetGenericArguments().Length == 1 && TreeListTypeHelper.IsBindableType(type.GetGenericArguments()[0])));
		}

		// Token: 0x0600C0C6 RID: 49350 RVA: 0x002AEA40 File Offset: 0x002ACC40
		public static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600C0C7 RID: 49351 RVA: 0x002AEA61 File Offset: 0x002ACC61
		public static Type GetNonNullableType(Type type)
		{
			if (!TreeListTypeHelper.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x0600C0C8 RID: 49352 RVA: 0x002AEA75 File Offset: 0x002ACC75
		public static bool IsNumericType(Type type)
		{
			return TreeListTypeHelper.GetNumericTypeKind(type) != 0;
		}

		// Token: 0x0600C0C9 RID: 49353 RVA: 0x002AEA83 File Offset: 0x002ACC83
		public static bool IsDateType(Type type)
		{
			return TreeListTypeHelper.GetDateTypeKind(type) != 0;
		}

		// Token: 0x0600C0CA RID: 49354 RVA: 0x002AEA91 File Offset: 0x002ACC91
		public static int GetDateTypeKind(Type type)
		{
			type = TreeListTypeHelper.GetNonNullableType(type);
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

		// Token: 0x0600C0CB RID: 49355 RVA: 0x002AEAC4 File Offset: 0x002ACCC4
		public static int GetNumericTypeKind(Type type)
		{
			type = TreeListTypeHelper.GetNonNullableType(type);
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
