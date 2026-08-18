using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F7 RID: 247
	internal struct PropertyPtrTableReader
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x000197FC File Offset: 0x000179FC
		internal PropertyPtrTableReader(int numberOfRows, int propertyTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsPropertyTableRowRefSizeSmall = (propertyTableRowRefSize == 2);
			this._PropertyOffset = 0;
			this.RowSize = this._PropertyOffset + propertyTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001983C File Offset: 0x00017A3C
		internal PropertyDefinitionHandle GetPropertyFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return PropertyDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._PropertyOffset, this._IsPropertyTableRowRefSizeSmall));
		}

		// Token: 0x04000742 RID: 1858
		internal readonly int NumberOfRows;

		// Token: 0x04000743 RID: 1859
		private readonly bool _IsPropertyTableRowRefSizeSmall;

		// Token: 0x04000744 RID: 1860
		private readonly int _PropertyOffset;

		// Token: 0x04000745 RID: 1861
		internal readonly int RowSize;

		// Token: 0x04000746 RID: 1862
		internal readonly MemoryBlock Block;
	}
}
