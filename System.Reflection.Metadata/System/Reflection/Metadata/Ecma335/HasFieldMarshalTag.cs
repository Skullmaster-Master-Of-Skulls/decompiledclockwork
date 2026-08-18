using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000CA RID: 202
	internal static class HasFieldMarshalTag
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00016758 File Offset: 0x00014958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint hasFieldMarshal)
		{
			uint num = 2052U >> (int)((int)(hasFieldMarshal & 1U) << 3) << 24;
			uint num2 = hasFieldMarshal >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001678F File Offset: 0x0001498F
		internal static uint ConvertToTag(EntityHandle handle)
		{
			if (handle.Type == 67108864U)
			{
				return (uint)(handle.RowId << 1 | 0);
			}
			if (handle.Type == 134217728U)
			{
				return (uint)(handle.RowId << 1 | 1);
			}
			return 0U;
		}

		// Token: 0x0400059F RID: 1439
		internal const int NumberOfBits = 1;

		// Token: 0x040005A0 RID: 1440
		internal const int LargeRowSize = 32768;

		// Token: 0x040005A1 RID: 1441
		internal const uint Field = 0U;

		// Token: 0x040005A2 RID: 1442
		internal const uint Param = 1U;

		// Token: 0x040005A3 RID: 1443
		internal const uint TagMask = 1U;

		// Token: 0x040005A4 RID: 1444
		internal const TableMask TablesReferenced = TableMask.Field | TableMask.Param;

		// Token: 0x040005A5 RID: 1445
		internal const uint TagToTokenTypeByteVector = 2052U;
	}
}
