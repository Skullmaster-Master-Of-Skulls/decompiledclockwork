using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F1 RID: 241
	internal struct FieldLayoutTableReader
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x00019290 File Offset: 0x00017490
		internal FieldLayoutTableReader(int numberOfRows, bool declaredSorted, int fieldTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldTableRowRefSizeSmall = (fieldTableRowRefSize == 2);
			this._OffsetOffset = 0;
			this._FieldOffset = this._OffsetOffset + 4;
			this.RowSize = this._FieldOffset + fieldTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.FieldLayout);
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000192FC File Offset: 0x000174FC
		internal int FindFieldLayoutRowId(FieldDefinitionHandle handle)
		{
			return this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._FieldOffset, (uint)handle.RowId, this._IsFieldTableRowRefSizeSmall) + 1;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00019338 File Offset: 0x00017538
		internal uint GetOffset(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._OffsetOffset);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00019368 File Offset: 0x00017568
		internal FieldDefinitionHandle GetField(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return FieldDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._FieldOffset, this._IsFieldTableRowRefSizeSmall));
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000193A4 File Offset: 0x000175A4
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._FieldOffset, this._IsFieldTableRowRefSizeSmall);
		}

		// Token: 0x0400071C RID: 1820
		internal readonly int NumberOfRows;

		// Token: 0x0400071D RID: 1821
		private readonly bool _IsFieldTableRowRefSizeSmall;

		// Token: 0x0400071E RID: 1822
		private readonly int _OffsetOffset;

		// Token: 0x0400071F RID: 1823
		private readonly int _FieldOffset;

		// Token: 0x04000720 RID: 1824
		internal readonly int RowSize;

		// Token: 0x04000721 RID: 1825
		internal readonly MemoryBlock Block;
	}
}
