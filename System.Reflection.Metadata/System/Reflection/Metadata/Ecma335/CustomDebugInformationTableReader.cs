using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000121 RID: 289
	internal struct CustomDebugInformationTableReader
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		internal CustomDebugInformationTableReader(int numberOfRows, bool declaredSorted, int hasCustomDebugInformationRefSize, int guidHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isHasCustomDebugInformationRefSizeSmall = (hasCustomDebugInformationRefSize == 2);
			this._isGuidHeapRefSizeSmall = (guidHeapRefSize == 2);
			this._isBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._kindOffset = 0 + hasCustomDebugInformationRefSize;
			this._valueOffset = this._kindOffset + guidHeapRefSize;
			this.RowSize = this._valueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (numberOfRows > 0 && !declaredSorted)
			{
				Throw.TableNotSorted(TableIndex.CustomDebugInformation);
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001C428 File Offset: 0x0001A628
		internal EntityHandle GetParent(CustomDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return HasCustomDebugInformationTag.ConvertToHandle(this.Block.PeekTaggedReference(num + 0, this._isHasCustomDebugInformationRefSizeSmall));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001C464 File Offset: 0x0001A664
		internal GuidHandle GetKind(CustomDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(num + this._kindOffset, this._isGuidHeapRefSizeSmall));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		internal BlobHandle GetValue(CustomDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._valueOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		internal void GetRange(EntityHandle parentHandle, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, 0, HasCustomDebugInformationTag.ConvertToTag(parentHandle), this._isHasCustomDebugInformationRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001C534 File Offset: 0x0001A734
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, 0, this._isHasCustomDebugInformationRefSizeSmall);
		}

		// Token: 0x04000888 RID: 2184
		internal readonly int NumberOfRows;

		// Token: 0x04000889 RID: 2185
		private readonly bool _isHasCustomDebugInformationRefSizeSmall;

		// Token: 0x0400088A RID: 2186
		private readonly bool _isGuidHeapRefSizeSmall;

		// Token: 0x0400088B RID: 2187
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x0400088C RID: 2188
		private const int ParentOffset = 0;

		// Token: 0x0400088D RID: 2189
		private readonly int _kindOffset;

		// Token: 0x0400088E RID: 2190
		private readonly int _valueOffset;

		// Token: 0x0400088F RID: 2191
		internal readonly int RowSize;

		// Token: 0x04000890 RID: 2192
		internal readonly MemoryBlock Block;
	}
}
