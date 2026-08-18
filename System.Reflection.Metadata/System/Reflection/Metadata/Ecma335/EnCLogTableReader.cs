using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FF RID: 255
	internal struct EnCLogTableReader
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x0001A12C File Offset: 0x0001832C
		internal EnCLogTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset, MetadataStreamKind metadataStreamKind)
		{
			this.NumberOfRows = ((metadataStreamKind == MetadataStreamKind.Compressed) ? 0 : numberOfRows);
			this._TokenOffset = 0;
			this._FuncCodeOffset = this._TokenOffset + 4;
			this.RowSize = this._FuncCodeOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001A184 File Offset: 0x00018384
		internal uint GetToken(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._TokenOffset);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001A1B4 File Offset: 0x000183B4
		internal EditAndContinueOperation GetFuncCode(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (EditAndContinueOperation)this.Block.PeekUInt32(num + this._FuncCodeOffset);
		}

		// Token: 0x04000779 RID: 1913
		internal readonly int NumberOfRows;

		// Token: 0x0400077A RID: 1914
		private readonly int _TokenOffset;

		// Token: 0x0400077B RID: 1915
		private readonly int _FuncCodeOffset;

		// Token: 0x0400077C RID: 1916
		internal readonly int RowSize;

		// Token: 0x0400077D RID: 1917
		internal readonly MemoryBlock Block;
	}
}
