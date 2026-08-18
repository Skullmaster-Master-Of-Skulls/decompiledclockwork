using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011E RID: 286
	internal struct LocalConstantTableReader
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0001C124 File Offset: 0x0001A324
		internal LocalConstantTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._isBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._signatureOffset = 0 + stringHeapRefSize;
			this.RowSize = this._signatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001C17C File Offset: 0x0001A37C
		internal StringHandle GetName(LocalConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + 0, this._isStringHeapRefSizeSmall));
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		internal BlobHandle GetSignature(LocalConstantHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._signatureOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000874 RID: 2164
		internal readonly int NumberOfRows;

		// Token: 0x04000875 RID: 2165
		private readonly bool _isStringHeapRefSizeSmall;

		// Token: 0x04000876 RID: 2166
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x04000877 RID: 2167
		private const int NameOffset = 0;

		// Token: 0x04000878 RID: 2168
		private readonly int _signatureOffset;

		// Token: 0x04000879 RID: 2169
		internal readonly int RowSize;

		// Token: 0x0400087A RID: 2170
		internal readonly MemoryBlock Block;
	}
}
