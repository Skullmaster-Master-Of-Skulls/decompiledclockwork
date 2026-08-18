using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000117 RID: 279
	internal static class TypeOrMethodDefTag
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0001B31C File Offset: 0x0001951C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint typeOrMethodDef)
		{
			uint num = 1538U >> (int)((int)(typeOrMethodDef & 1U) << 3) << 24;
			uint num2 = typeOrMethodDef >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001B353 File Offset: 0x00019553
		internal static uint ConvertTypeDefRowIdToTag(TypeDefinitionHandle typeDef)
		{
			return (uint)(typeDef.RowId << 1 | 0);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000172DB File Offset: 0x000154DB
		internal static uint ConvertMethodDefToTag(MethodDefinitionHandle methodDef)
		{
			return (uint)(methodDef.RowId << 1 | 1);
		}

		// Token: 0x04000826 RID: 2086
		internal const int NumberOfBits = 1;

		// Token: 0x04000827 RID: 2087
		internal const int LargeRowSize = 32768;

		// Token: 0x04000828 RID: 2088
		internal const uint TypeDef = 0U;

		// Token: 0x04000829 RID: 2089
		internal const uint MethodDef = 1U;

		// Token: 0x0400082A RID: 2090
		internal const uint TagMask = 1U;

		// Token: 0x0400082B RID: 2091
		internal const uint TagToTokenTypeByteVector = 1538U;

		// Token: 0x0400082C RID: 2092
		internal const TableMask TablesReferenced = TableMask.TypeDef | TableMask.MethodDef;
	}
}
