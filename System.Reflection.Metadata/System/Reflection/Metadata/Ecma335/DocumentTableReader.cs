using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011A RID: 282
	internal struct DocumentTableReader
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x0001BB40 File Offset: 0x00019D40
		internal DocumentTableReader(int numberOfRows, int guidHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isGuidHeapRefSizeSmall = (guidHeapRefSize == 2);
			this._isBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._hashAlgorithmOffset = 0 + blobHeapRefSize;
			this._hashOffset = this._hashAlgorithmOffset + guidHeapRefSize;
			this._languageOffset = this._hashOffset + blobHeapRefSize;
			this.RowSize = this._languageOffset + guidHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		internal DocumentNameBlobHandle GetName(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return DocumentNameBlobHandle.FromOffset(this.Block.PeekHeapReference(num + 0, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		internal GuidHandle GetHashAlgorithm(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(num + this._hashAlgorithmOffset, this._isGuidHeapRefSizeSmall));
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001BC30 File Offset: 0x00019E30
		internal BlobHandle GetHash(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._hashOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001BC70 File Offset: 0x00019E70
		internal GuidHandle GetLanguage(DocumentHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(num + this._languageOffset, this._isGuidHeapRefSizeSmall));
		}

		// Token: 0x04000850 RID: 2128
		internal readonly int NumberOfRows;

		// Token: 0x04000851 RID: 2129
		private readonly bool _isGuidHeapRefSizeSmall;

		// Token: 0x04000852 RID: 2130
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x04000853 RID: 2131
		private const int NameOffset = 0;

		// Token: 0x04000854 RID: 2132
		private readonly int _hashAlgorithmOffset;

		// Token: 0x04000855 RID: 2133
		private readonly int _hashOffset;

		// Token: 0x04000856 RID: 2134
		private readonly int _languageOffset;

		// Token: 0x04000857 RID: 2135
		internal readonly int RowSize;

		// Token: 0x04000858 RID: 2136
		internal readonly MemoryBlock Block;
	}
}
