using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000120 RID: 288
	internal struct ImportScopeTableReader
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		internal ImportScopeTableReader(int numberOfRows, int importScopeRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isImportScopeRefSizeSmall = (importScopeRefSize == 2);
			this._isBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._importsOffset = 0 + importScopeRefSize;
			this.RowSize = this._importsOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001C32C File Offset: 0x0001A52C
		internal ImportScopeHandle GetParent(ImportScopeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ImportScopeHandle.FromRowId(this.Block.PeekReference(num + 0, this._isImportScopeRefSizeSmall));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001C368 File Offset: 0x0001A568
		internal BlobHandle GetImports(ImportScopeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._importsOffset, this._isBlobHeapRefSizeSmall));
		}

		// Token: 0x04000881 RID: 2177
		internal readonly int NumberOfRows;

		// Token: 0x04000882 RID: 2178
		private readonly bool _isImportScopeRefSizeSmall;

		// Token: 0x04000883 RID: 2179
		private readonly bool _isBlobHeapRefSizeSmall;

		// Token: 0x04000884 RID: 2180
		private const int ParentOffset = 0;

		// Token: 0x04000885 RID: 2181
		private readonly int _importsOffset;

		// Token: 0x04000886 RID: 2182
		internal readonly int RowSize;

		// Token: 0x04000887 RID: 2183
		internal readonly MemoryBlock Block;
	}
}
