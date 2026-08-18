using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FC RID: 252
	internal struct TypeSpecTableReader
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x00019DCB File Offset: 0x00017FCB
		internal TypeSpecTableReader(int numberOfRows, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._SignatureOffset = 0;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00019E0C File Offset: 0x0001800C
		internal BlobHandle GetSignature(TypeSpecificationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x04000764 RID: 1892
		internal readonly int NumberOfRows;

		// Token: 0x04000765 RID: 1893
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000766 RID: 1894
		private readonly int _SignatureOffset;

		// Token: 0x04000767 RID: 1895
		internal readonly int RowSize;

		// Token: 0x04000768 RID: 1896
		internal readonly MemoryBlock Block;
	}
}
