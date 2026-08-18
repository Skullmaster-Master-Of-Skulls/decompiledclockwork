using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000C7 RID: 199
	internal static class HasConstantTag
	{
		// Token: 0x06000852 RID: 2130 RVA: 0x000164A4 File Offset: 0x000146A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint hasConstant)
		{
			uint num = 1509380U >> (int)((int)(hasConstant & 3U) << 3) << 24;
			uint num2 = hasConstant >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000164E0 File Offset: 0x000146E0
		internal static uint ConvertToTag(EntityHandle token)
		{
			HandleKind kind = token.Kind;
			uint rowId = (uint)token.RowId;
			if (kind == HandleKind.FieldDefinition)
			{
				return rowId << 2 | 0U;
			}
			if (kind == HandleKind.Parameter)
			{
				return rowId << 2 | 1U;
			}
			if (kind == HandleKind.PropertyDefinition)
			{
				return rowId << 2 | 2U;
			}
			return 0U;
		}

		// Token: 0x04000573 RID: 1395
		internal const int NumberOfBits = 2;

		// Token: 0x04000574 RID: 1396
		internal const int LargeRowSize = 16384;

		// Token: 0x04000575 RID: 1397
		internal const uint Field = 0U;

		// Token: 0x04000576 RID: 1398
		internal const uint Param = 1U;

		// Token: 0x04000577 RID: 1399
		internal const uint Property = 2U;

		// Token: 0x04000578 RID: 1400
		internal const uint TagMask = 3U;

		// Token: 0x04000579 RID: 1401
		internal const TableMask TablesReferenced = TableMask.Field | TableMask.Param | TableMask.Property;

		// Token: 0x0400057A RID: 1402
		internal const uint TagToTokenTypeByteVector = 1509380U;
	}
}
