using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000CB RID: 203
	internal static class HasSemanticsTag
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x000167C8 File Offset: 0x000149C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static EntityHandle ConvertToHandle(uint hasSemantic)
		{
			uint num = 5908U >> (int)((int)(hasSemantic & 1U) << 3) << 24;
			uint num2 = hasSemantic >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000167FF File Offset: 0x000149FF
		internal static uint ConvertEventHandleToTag(EventDefinitionHandle eventDef)
		{
			return (uint)(eventDef.RowId << 1 | 0);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001680C File Offset: 0x00014A0C
		internal static uint ConvertPropertyHandleToTag(PropertyDefinitionHandle propertyDef)
		{
			return (uint)(propertyDef.RowId << 1 | 1);
		}

		// Token: 0x040005A6 RID: 1446
		internal const int NumberOfBits = 1;

		// Token: 0x040005A7 RID: 1447
		internal const int LargeRowSize = 32768;

		// Token: 0x040005A8 RID: 1448
		internal const uint Event = 0U;

		// Token: 0x040005A9 RID: 1449
		internal const uint Property = 1U;

		// Token: 0x040005AA RID: 1450
		internal const uint TagMask = 1U;

		// Token: 0x040005AB RID: 1451
		internal const TableMask TablesReferenced = TableMask.Event | TableMask.Property;

		// Token: 0x040005AC RID: 1452
		internal const uint TagToTokenTypeByteVector = 5908U;
	}
}
