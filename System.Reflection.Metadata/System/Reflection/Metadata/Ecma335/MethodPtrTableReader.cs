using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E6 RID: 230
	internal struct MethodPtrTableReader
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x00018387 File Offset: 0x00016587
		internal MethodPtrTableReader(int numberOfRows, int methodTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMethodTableRowRefSizeSmall = (methodTableRowRefSize == 2);
			this._MethodOffset = 0;
			this.RowSize = this._MethodOffset + methodTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000183C8 File Offset: 0x000165C8
		internal MethodDefinitionHandle GetMethodFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._MethodOffset, this._IsMethodTableRowRefSizeSmall));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00018404 File Offset: 0x00016604
		internal int GetRowIdForMethodDefRow(int methodDefRowId)
		{
			return this.Block.LinearSearchReference(this.RowSize, this._MethodOffset, (uint)methodDefRowId, this._IsMethodTableRowRefSizeSmall) + 1;
		}

		// Token: 0x040006C7 RID: 1735
		internal readonly int NumberOfRows;

		// Token: 0x040006C8 RID: 1736
		private readonly bool _IsMethodTableRowRefSizeSmall;

		// Token: 0x040006C9 RID: 1737
		private readonly int _MethodOffset;

		// Token: 0x040006CA RID: 1738
		internal readonly int RowSize;

		// Token: 0x040006CB RID: 1739
		internal readonly MemoryBlock Block;
	}
}
