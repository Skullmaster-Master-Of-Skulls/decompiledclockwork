using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000116 RID: 278
	internal static class TypeDefOrRefTag
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x0001B2E0 File Offset: 0x000194E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint typeDefOrRefTag)
		{
			uint num = 1769730U >> (int)((int)(typeDefOrRefTag & 3U) << 3) << 24;
			uint num2 = typeDefOrRefTag >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0400081E RID: 2078
		internal const int NumberOfBits = 2;

		// Token: 0x0400081F RID: 2079
		internal const int LargeRowSize = 16384;

		// Token: 0x04000820 RID: 2080
		internal const uint TypeDef = 0U;

		// Token: 0x04000821 RID: 2081
		internal const uint TypeRef = 1U;

		// Token: 0x04000822 RID: 2082
		internal const uint TypeSpec = 2U;

		// Token: 0x04000823 RID: 2083
		internal const uint TagMask = 3U;

		// Token: 0x04000824 RID: 2084
		internal const uint TagToTokenTypeByteVector = 1769730U;

		// Token: 0x04000825 RID: 2085
		internal const TableMask TablesReferenced = TableMask.TypeRef | TableMask.TypeDef | TableMask.TypeSpec;
	}
}
