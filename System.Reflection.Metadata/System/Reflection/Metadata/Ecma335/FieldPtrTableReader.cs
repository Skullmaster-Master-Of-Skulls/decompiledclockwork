using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E4 RID: 228
	internal struct FieldPtrTableReader
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x000181B6 File Offset: 0x000163B6
		internal FieldPtrTableReader(int numberOfRows, int fieldTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldTableRowRefSizeSmall = (fieldTableRowRefSize == 2);
			this._FieldOffset = 0;
			this.RowSize = this._FieldOffset + fieldTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000181F8 File Offset: 0x000163F8
		internal FieldDefinitionHandle GetFieldFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return FieldDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._FieldOffset, this._IsFieldTableRowRefSizeSmall));
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00018234 File Offset: 0x00016434
		internal int GetRowIdForFieldDefRow(int fieldDefRowId)
		{
			return this.Block.LinearSearchReference(this.RowSize, this._FieldOffset, (uint)fieldDefRowId, this._IsFieldTableRowRefSizeSmall) + 1;
		}

		// Token: 0x040006BA RID: 1722
		internal readonly int NumberOfRows;

		// Token: 0x040006BB RID: 1723
		private readonly bool _IsFieldTableRowRefSizeSmall;

		// Token: 0x040006BC RID: 1724
		private readonly int _FieldOffset;

		// Token: 0x040006BD RID: 1725
		internal readonly int RowSize;

		// Token: 0x040006BE RID: 1726
		internal readonly MemoryBlock Block;
	}
}
