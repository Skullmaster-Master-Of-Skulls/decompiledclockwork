using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000C9 RID: 201
	internal static class HasDeclSecurityTag
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x000166D8 File Offset: 0x000148D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint hasDeclSecurity)
		{
			uint num = 2098690U >> (int)((int)(hasDeclSecurity & 3U) << 3) << 24;
			uint num2 = hasDeclSecurity >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00016714 File Offset: 0x00014914
		internal static uint ConvertToTag(EntityHandle handle)
		{
			uint type = handle.Type;
			uint rowId = (uint)handle.RowId;
			uint num = type >> 24;
			if (num == 2U)
			{
				return rowId << 2 | 0U;
			}
			if (num == 6U)
			{
				return rowId << 2 | 1U;
			}
			if (num != 32U)
			{
				return 0U;
			}
			return rowId << 2 | 2U;
		}

		// Token: 0x04000597 RID: 1431
		internal const int NumberOfBits = 2;

		// Token: 0x04000598 RID: 1432
		internal const int LargeRowSize = 16384;

		// Token: 0x04000599 RID: 1433
		internal const uint TypeDef = 0U;

		// Token: 0x0400059A RID: 1434
		internal const uint MethodDef = 1U;

		// Token: 0x0400059B RID: 1435
		internal const uint Assembly = 2U;

		// Token: 0x0400059C RID: 1436
		internal const uint TagMask = 3U;

		// Token: 0x0400059D RID: 1437
		internal const TableMask TablesReferenced = TableMask.TypeDef | TableMask.MethodDef | TableMask.Assembly;

		// Token: 0x0400059E RID: 1438
		internal const uint TagToTokenTypeByteVector = 2098690U;
	}
}
