using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000C6 RID: 198
	internal static class CustomAttributeTypeTag
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00016468 File Offset: 0x00014668
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint customAttributeType)
		{
			uint num = (uint)(168165376UL >> (int)((int)(customAttributeType & 7U) << 3)) << 24;
			uint num2 = customAttributeType >> 3;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0400056C RID: 1388
		internal const int NumberOfBits = 3;

		// Token: 0x0400056D RID: 1389
		internal const int LargeRowSize = 8192;

		// Token: 0x0400056E RID: 1390
		internal const uint MethodDef = 2U;

		// Token: 0x0400056F RID: 1391
		internal const uint MemberRef = 3U;

		// Token: 0x04000570 RID: 1392
		internal const uint TagMask = 7U;

		// Token: 0x04000571 RID: 1393
		internal const ulong TagToTokenTypeByteVector = 168165376UL;

		// Token: 0x04000572 RID: 1394
		internal const TableMask TablesReferenced = TableMask.MethodDef | TableMask.MemberRef;
	}
}
