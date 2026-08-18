using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000DF RID: 223
	internal static class ResolutionScopeTag
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x00017BA8 File Offset: 0x00015DA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint resolutionScope)
		{
			uint num = 19077632U >> (int)((int)(resolutionScope & 3U) << 3) << 24;
			uint num2 = resolutionScope >> 2;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0400068F RID: 1679
		internal const int NumberOfBits = 2;

		// Token: 0x04000690 RID: 1680
		internal const int LargeRowSize = 16384;

		// Token: 0x04000691 RID: 1681
		internal const uint Module = 0U;

		// Token: 0x04000692 RID: 1682
		internal const uint ModuleRef = 1U;

		// Token: 0x04000693 RID: 1683
		internal const uint AssemblyRef = 2U;

		// Token: 0x04000694 RID: 1684
		internal const uint TypeRef = 3U;

		// Token: 0x04000695 RID: 1685
		internal const uint TagMask = 3U;

		// Token: 0x04000696 RID: 1686
		internal const uint TagToTokenTypeByteVector = 19077632U;

		// Token: 0x04000697 RID: 1687
		internal const TableMask TablesReferenced = TableMask.Module | TableMask.TypeRef | TableMask.ModuleRef | TableMask.AssemblyRef;
	}
}
