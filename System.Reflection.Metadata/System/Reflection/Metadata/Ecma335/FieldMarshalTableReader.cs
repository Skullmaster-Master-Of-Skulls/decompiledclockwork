using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EE RID: 238
	internal struct FieldMarshalTableReader
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x00018E30 File Offset: 0x00017030
		internal FieldMarshalTableReader(int numberOfRows, bool declaredSorted, int hasFieldMarshalRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasFieldMarshalRefSizeSmall = (hasFieldMarshalRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._ParentOffset = 0;
			this._NativeTypeOffset = this._ParentOffset + hasFieldMarshalRefSize;
			this.RowSize = this._NativeTypeOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.FieldMarshal);
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00018EA8 File Offset: 0x000170A8
		internal EntityHandle GetParent(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return HasFieldMarshalTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasFieldMarshalRefSizeSmall));
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00018EE4 File Offset: 0x000170E4
		internal BlobHandle GetNativeType(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._NativeTypeOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00018F20 File Offset: 0x00017120
		internal int FindFieldMarshalRowId(EntityHandle handle)
		{
			return this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._ParentOffset, HasFieldMarshalTag.ConvertToTag(handle), this._IsHasFieldMarshalRefSizeSmall) + 1;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00018F5C File Offset: 0x0001715C
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasFieldMarshalRefSizeSmall);
		}

		// Token: 0x04000706 RID: 1798
		internal readonly int NumberOfRows;

		// Token: 0x04000707 RID: 1799
		private readonly bool _IsHasFieldMarshalRefSizeSmall;

		// Token: 0x04000708 RID: 1800
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000709 RID: 1801
		private readonly int _ParentOffset;

		// Token: 0x0400070A RID: 1802
		private readonly int _NativeTypeOffset;

		// Token: 0x0400070B RID: 1803
		internal readonly int RowSize;

		// Token: 0x0400070C RID: 1804
		internal readonly MemoryBlock Block;
	}
}
