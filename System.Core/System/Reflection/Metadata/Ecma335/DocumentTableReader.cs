using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000077 RID: 119
	internal struct DocumentTableReader
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00007AAC File Offset: 0x00005CAC
		internal DocumentTableReader(int numberOfRows, int guidHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isGuidHeapRefSizeSmall = (guidHeapRefSize == 2);
			this._isBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._hashAlgorithmOffset = blobHeapRefSize;
			this._hashOffset = this._hashAlgorithmOffset + guidHeapRefSize;
			this._languageOffset = this._hashOffset + blobHeapRefSize;
			this.RowSize = this._languageOffset + guidHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00007B1C File Offset: 0x00005D1C
		internal DocumentNameBlobHandle GetName(DocumentHandle handle)
		{
			int offset = (handle.RowId - 1) * this.RowSize;
			return DocumentNameBlobHandle.FromOffset(this.Block.PeekHeapReference(offset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00007B54 File Offset: 0x00005D54
		internal BlobHandle GetHash(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._hashOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000467 RID: 1127
		internal readonly int NumberOfRows;

		// Token: 0x04000468 RID: 1128
		private readonly bool _isGuidHeapRefSizeSmall;

		// Token: 0x04000469 RID: 1129
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x0400046A RID: 1130
		private const int NameOffset = 0;

		// Token: 0x0400046B RID: 1131
		private readonly int _hashAlgorithmOffset;

		// Token: 0x0400046C RID: 1132
		private readonly int _hashOffset;

		// Token: 0x0400046D RID: 1133
		private readonly int _languageOffset;

		// Token: 0x0400046E RID: 1134
		internal readonly int RowSize;

		// Token: 0x0400046F RID: 1135
		internal readonly MemoryBlock Block;
	}
}
