using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000DC RID: 220
	internal static class MethodDefOrRefTag
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x00017374 File Offset: 0x00015574
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint methodDefOrRef)
		{
			uint num = 2566U >> (int)((int)(methodDefOrRef & 1U) << 3) << 24;
			uint num2 = methodDefOrRef >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0400067C RID: 1660
		internal const int NumberOfBits = 1;

		// Token: 0x0400067D RID: 1661
		internal const int LargeRowSize = 32768;

		// Token: 0x0400067E RID: 1662
		internal const uint MethodDef = 0U;

		// Token: 0x0400067F RID: 1663
		internal const uint MemberRef = 1U;

		// Token: 0x04000680 RID: 1664
		internal const uint TagMask = 1U;

		// Token: 0x04000681 RID: 1665
		internal const TableMask TablesReferenced = TableMask.MethodDef | TableMask.MemberRef;

		// Token: 0x04000682 RID: 1666
		internal const uint TagToTokenTypeByteVector = 2566U;
	}
}
