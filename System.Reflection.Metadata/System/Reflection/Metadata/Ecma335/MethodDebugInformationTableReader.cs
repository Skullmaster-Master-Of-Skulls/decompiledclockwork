using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011B RID: 283
	internal struct MethodDebugInformationTableReader
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x0001BCB0 File Offset: 0x00019EB0
		internal MethodDebugInformationTableReader(int numberOfRows, int documentRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isDocumentRefSmall = (documentRefSize == 2);
			this._isBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._sequencePointsOffset = 0 + documentRefSize;
			this.RowSize = this._sequencePointsOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001BD08 File Offset: 0x00019F08
		internal DocumentHandle GetDocument(MethodDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return DocumentHandle.FromRowId(this.Block.PeekReference(num + 0, this._isDocumentRefSmall));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001BD44 File Offset: 0x00019F44
		internal BlobHandle GetSequencePoints(MethodDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._sequencePointsOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000859 RID: 2137
		internal readonly int NumberOfRows;

		// Token: 0x0400085A RID: 2138
		private readonly bool _isDocumentRefSmall;

		// Token: 0x0400085B RID: 2139
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x0400085C RID: 2140
		private const int DocumentOffset = 0;

		// Token: 0x0400085D RID: 2141
		private readonly int _sequencePointsOffset;

		// Token: 0x0400085E RID: 2142
		internal readonly int RowSize;

		// Token: 0x0400085F RID: 2143
		internal readonly MemoryBlock Block;
	}
}
