using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F4 RID: 244
	internal struct EventPtrTableReader
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x00019558 File Offset: 0x00017758
		internal EventPtrTableReader(int numberOfRows, int eventTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsEventTableRowRefSizeSmall = (eventTableRowRefSize == 2);
			this._EventOffset = 0;
			this.RowSize = this._EventOffset + eventTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00019598 File Offset: 0x00017798
		internal EventDefinitionHandle GetEventFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return EventDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._EventOffset, this._IsEventTableRowRefSizeSmall));
		}

		// Token: 0x0400072E RID: 1838
		internal readonly int NumberOfRows;

		// Token: 0x0400072F RID: 1839
		private readonly bool _IsEventTableRowRefSizeSmall;

		// Token: 0x04000730 RID: 1840
		private readonly int _EventOffset;

		// Token: 0x04000731 RID: 1841
		internal readonly int RowSize;

		// Token: 0x04000732 RID: 1842
		internal readonly MemoryBlock Block;
	}
}
