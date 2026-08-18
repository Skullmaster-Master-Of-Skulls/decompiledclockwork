using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FE RID: 254
	internal struct FieldRVATableReader
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x0001A028 File Offset: 0x00018228
		internal FieldRVATableReader(int numberOfRows, bool declaredSorted, int fieldTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldTableRowRefSizeSmall = (fieldTableRowRefSize == 2);
			this._RvaOffset = 0;
			this._FieldOffset = this._RvaOffset + 4;
			this.RowSize = this._FieldOffset + fieldTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.FieldRva);
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001A094 File Offset: 0x00018294
		internal int GetRva(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._RvaOffset);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001A0C4 File Offset: 0x000182C4
		internal int FindFieldRvaRowId(int fieldDefRowId)
		{
			return this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._FieldOffset, (uint)fieldDefRowId, this._IsFieldTableRowRefSizeSmall) + 1;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001A0FC File Offset: 0x000182FC
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._FieldOffset, this._IsFieldTableRowRefSizeSmall);
		}

		// Token: 0x04000773 RID: 1907
		internal readonly int NumberOfRows;

		// Token: 0x04000774 RID: 1908
		private readonly bool _IsFieldTableRowRefSizeSmall;

		// Token: 0x04000775 RID: 1909
		private readonly int _RvaOffset;

		// Token: 0x04000776 RID: 1910
		private readonly int _FieldOffset;

		// Token: 0x04000777 RID: 1911
		internal readonly int RowSize;

		// Token: 0x04000778 RID: 1912
		internal readonly MemoryBlock Block;
	}
}
