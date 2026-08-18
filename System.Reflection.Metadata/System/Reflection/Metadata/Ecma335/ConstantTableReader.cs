using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EC RID: 236
	internal struct ConstantTableReader
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x00018A24 File Offset: 0x00016C24
		internal ConstantTableReader(int numberOfRows, bool declaredSorted, int hasConstantRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasConstantRefSizeSmall = (hasConstantRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._TypeOffset = 0;
			this._ParentOffset = this._TypeOffset + 1 + 1;
			this._ValueOffset = this._ParentOffset + hasConstantRefSize;
			this.RowSize = this._ValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.Constant);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00018AAC File Offset: 0x00016CAC
		internal ConstantTypeCode GetType(ConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (ConstantTypeCode)this.Block.PeekByte(num + this._TypeOffset);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00018AE0 File Offset: 0x00016CE0
		internal BlobHandle GetValue(ConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._ValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00018B20 File Offset: 0x00016D20
		internal EntityHandle GetParent(ConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return HasConstantTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasConstantRefSizeSmall));
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00018B60 File Offset: 0x00016D60
		internal ConstantHandle FindConstant(EntityHandle parentHandle)
		{
			return ConstantHandle.FromRowId(this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._ParentOffset, HasConstantTag.ConvertToTag(parentHandle), this._IsHasConstantRefSizeSmall) + 1);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00018BA0 File Offset: 0x00016DA0
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasConstantRefSizeSmall);
		}

		// Token: 0x040006F4 RID: 1780
		internal readonly int NumberOfRows;

		// Token: 0x040006F5 RID: 1781
		private readonly bool _IsHasConstantRefSizeSmall;

		// Token: 0x040006F6 RID: 1782
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040006F7 RID: 1783
		private readonly int _TypeOffset;

		// Token: 0x040006F8 RID: 1784
		private readonly int _ParentOffset;

		// Token: 0x040006F9 RID: 1785
		private readonly int _ValueOffset;

		// Token: 0x040006FA RID: 1786
		internal readonly int RowSize;

		// Token: 0x040006FB RID: 1787
		internal readonly MemoryBlock Block;
	}
}
