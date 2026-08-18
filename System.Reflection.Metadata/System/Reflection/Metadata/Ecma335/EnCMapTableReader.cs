using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000100 RID: 256
	internal struct EnCMapTableReader
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x0001A1E2 File Offset: 0x000183E2
		internal EnCMapTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._TokenOffset = 0;
			this.RowSize = this._TokenOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001A218 File Offset: 0x00018418
		internal uint GetToken(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._TokenOffset);
		}

		// Token: 0x0400077E RID: 1918
		internal readonly int NumberOfRows;

		// Token: 0x0400077F RID: 1919
		private readonly int _TokenOffset;

		// Token: 0x04000780 RID: 1920
		internal readonly int RowSize;

		// Token: 0x04000781 RID: 1921
		internal readonly MemoryBlock Block;
	}
}
