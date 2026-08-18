using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000102 RID: 258
	internal struct AssemblyProcessorTableReader
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x0001A444 File Offset: 0x00018644
		internal AssemblyProcessorTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._ProcessorOffset = 0;
			this.RowSize = this._ProcessorOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x04000790 RID: 1936
		internal readonly int NumberOfRows;

		// Token: 0x04000791 RID: 1937
		private readonly int _ProcessorOffset;

		// Token: 0x04000792 RID: 1938
		internal readonly int RowSize;

		// Token: 0x04000793 RID: 1939
		internal readonly MemoryBlock Block;
	}
}
