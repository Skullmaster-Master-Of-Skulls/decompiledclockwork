using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000078 RID: 120
	internal struct MethodDebugInformationTableReader
	{
		// Token: 0x06000303 RID: 771 RVA: 0x00007B94 File Offset: 0x00005D94
		internal MethodDebugInformationTableReader(int numberOfRows, int documentRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isDocumentRefSmall = (documentRefSize == 2);
			this._isBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._sequencePointsOffset = documentRefSize;
			this.RowSize = this._sequencePointsOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00007BE8 File Offset: 0x00005DE8
		internal DocumentHandle GetDocument(MethodDebugInformationHandle handle)
		{
			int offset = (handle.RowId - 1) * this.RowSize;
			return DocumentHandle.FromRowId(this.Block.PeekReference(offset, this._isDocumentRefSmall));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00007C20 File Offset: 0x00005E20
		internal BlobHandle GetSequencePoints(MethodDebugInformationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._sequencePointsOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000470 RID: 1136
		internal readonly int NumberOfRows;

		// Token: 0x04000471 RID: 1137
		private readonly bool _isDocumentRefSmall;

		// Token: 0x04000472 RID: 1138
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x04000473 RID: 1139
		private const int DocumentOffset = 0;

		// Token: 0x04000474 RID: 1140
		private readonly int _sequencePointsOffset;

		// Token: 0x04000475 RID: 1141
		internal readonly int RowSize;

		// Token: 0x04000476 RID: 1142
		internal readonly MemoryBlock Block;
	}
}
