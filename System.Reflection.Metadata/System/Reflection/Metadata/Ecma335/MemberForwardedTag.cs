using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000D1 RID: 209
	internal static class MemberForwardedTag
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x000172A4 File Offset: 0x000154A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint memberForwarded)
		{
			uint num = 1540U >> (int)((int)(memberForwarded & 1U) << 3) << 24;
			uint num2 = memberForwarded >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000172DB File Offset: 0x000154DB
		internal static uint ConvertMethodDefToTag(MethodDefinitionHandle methodDef)
		{
			return (uint)(methodDef.RowId << 1 | 1);
		}

		// Token: 0x040005BD RID: 1469
		internal const int NumberOfBits = 1;

		// Token: 0x040005BE RID: 1470
		internal const int LargeRowSize = 32768;

		// Token: 0x040005BF RID: 1471
		internal const uint Field = 0U;

		// Token: 0x040005C0 RID: 1472
		internal const uint MethodDef = 1U;

		// Token: 0x040005C1 RID: 1473
		internal const uint TagMask = 1U;

		// Token: 0x040005C2 RID: 1474
		internal const TableMask TablesReferenced = TableMask.Field | TableMask.MethodDef;

		// Token: 0x040005C3 RID: 1475
		internal const uint TagToTokenTypeByteVector = 1540U;
	}
}
