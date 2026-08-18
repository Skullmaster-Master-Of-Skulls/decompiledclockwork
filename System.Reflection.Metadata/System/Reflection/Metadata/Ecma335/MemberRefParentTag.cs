using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000D2 RID: 210
	internal static class MemberRefParentTag
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x000172E8 File Offset: 0x000154E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint memberRef)
		{
			uint num = (uint)((uint)(116066484482UL >> (int)((int)(memberRef & 7U) << 3)) << 24);
			uint num2 = memberRef >> 3;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x040005C4 RID: 1476
		internal const int NumberOfBits = 3;

		// Token: 0x040005C5 RID: 1477
		internal const int LargeRowSize = 8192;

		// Token: 0x040005C6 RID: 1478
		internal const uint TypeDef = 0U;

		// Token: 0x040005C7 RID: 1479
		internal const uint TypeRef = 1U;

		// Token: 0x040005C8 RID: 1480
		internal const uint ModuleRef = 2U;

		// Token: 0x040005C9 RID: 1481
		internal const uint MethodDef = 3U;

		// Token: 0x040005CA RID: 1482
		internal const uint TypeSpec = 4U;

		// Token: 0x040005CB RID: 1483
		internal const uint TagMask = 7U;

		// Token: 0x040005CC RID: 1484
		internal const TableMask TablesReferenced = TableMask.TypeRef | TableMask.TypeDef | TableMask.MethodDef | TableMask.ModuleRef | TableMask.TypeSpec;

		// Token: 0x040005CD RID: 1485
		internal const ulong TagToTokenTypeByteVector = 116066484482UL;
	}
}
