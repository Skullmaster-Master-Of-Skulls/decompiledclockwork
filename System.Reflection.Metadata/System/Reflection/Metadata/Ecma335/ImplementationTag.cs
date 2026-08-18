using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000D0 RID: 208
	internal static class ImplementationTag
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00017268 File Offset: 0x00015468
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint implementation)
		{
			uint num = 2564902U >> (int)((int)(implementation & 3U) << 3) << 24;
			uint num2 = implementation >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x040005B5 RID: 1461
		internal const int NumberOfBits = 2;

		// Token: 0x040005B6 RID: 1462
		internal const int LargeRowSize = 16384;

		// Token: 0x040005B7 RID: 1463
		internal const uint File = 0U;

		// Token: 0x040005B8 RID: 1464
		internal const uint AssemblyRef = 1U;

		// Token: 0x040005B9 RID: 1465
		internal const uint ExportedType = 2U;

		// Token: 0x040005BA RID: 1466
		internal const uint TagMask = 3U;

		// Token: 0x040005BB RID: 1467
		internal const uint TagToTokenTypeByteVector = 2564902U;

		// Token: 0x040005BC RID: 1468
		internal const TableMask TablesReferenced = TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType;
	}
}
