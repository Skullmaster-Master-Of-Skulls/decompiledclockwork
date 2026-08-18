using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000ED RID: 237
	internal struct CustomAttributeTableReader
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x00018BD0 File Offset: 0x00016DD0
		internal CustomAttributeTableReader(int numberOfRows, bool declaredSorted, int hasCustomAttributeRefSize, int customAttributeTypeRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasCustomAttributeRefSizeSmall = (hasCustomAttributeRefSize == 2);
			this._IsCustomAttributeTypeRefSizeSmall = (customAttributeTypeRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._ParentOffset = 0;
			this._TypeOffset = this._ParentOffset + hasCustomAttributeRefSize;
			this._ValueOffset = this._TypeOffset + customAttributeTypeRefSize;
			this.RowSize = this._ValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			this.PtrTable = null;
			if (!declaredSorted && !this.CheckSorted())
			{
				this.PtrTable = this.Block.BuildPtrTable(numberOfRows, this.RowSize, this._ParentOffset, this._IsHasCustomAttributeRefSizeSmall);
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00018C84 File Offset: 0x00016E84
		internal EntityHandle GetParent(CustomAttributeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return HasCustomAttributeTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasCustomAttributeRefSizeSmall));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00018CC4 File Offset: 0x00016EC4
		internal EntityHandle GetConstructor(CustomAttributeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return CustomAttributeTypeTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._TypeOffset, this._IsCustomAttributeTypeRefSizeSmall));
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00018D04 File Offset: 0x00016F04
		internal BlobHandle GetValue(CustomAttributeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._ValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00018D44 File Offset: 0x00016F44
		private uint GetParentTag(int index)
		{
			return this.Block.PeekTaggedReference(index * this.RowSize + this._ParentOffset, this._IsHasCustomAttributeRefSizeSmall);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00018D74 File Offset: 0x00016F74
		internal void GetAttributeRange(EntityHandle parentHandle, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			if (this.PtrTable != null)
			{
				this.Block.BinarySearchReferenceRange(this.PtrTable, this.RowSize, this._ParentOffset, HasCustomAttributeTag.ConvertToTag(parentHandle), this._IsHasCustomAttributeRefSizeSmall, out num, out num2);
			}
			else
			{
				this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ParentOffset, HasCustomAttributeTag.ConvertToTag(parentHandle), this._IsHasCustomAttributeRefSizeSmall, out num, out num2);
			}
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00018E00 File Offset: 0x00017000
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasCustomAttributeRefSizeSmall);
		}

		// Token: 0x040006FC RID: 1788
		internal readonly int NumberOfRows;

		// Token: 0x040006FD RID: 1789
		private readonly bool _IsHasCustomAttributeRefSizeSmall;

		// Token: 0x040006FE RID: 1790
		private readonly bool _IsCustomAttributeTypeRefSizeSmall;

		// Token: 0x040006FF RID: 1791
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000700 RID: 1792
		private readonly int _ParentOffset;

		// Token: 0x04000701 RID: 1793
		private readonly int _TypeOffset;

		// Token: 0x04000702 RID: 1794
		private readonly int _ValueOffset;

		// Token: 0x04000703 RID: 1795
		internal readonly int RowSize;

		// Token: 0x04000704 RID: 1796
		internal readonly MemoryBlock Block;

		// Token: 0x04000705 RID: 1797
		internal readonly int[] PtrTable;
	}
}
