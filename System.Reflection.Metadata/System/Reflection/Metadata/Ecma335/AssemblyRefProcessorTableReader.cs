using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000105 RID: 261
	internal struct AssemblyRefProcessorTableReader
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x0001A740 File Offset: 0x00018940
		internal AssemblyRefProcessorTableReader(int numberOfRows, int assemblyRefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsAssemblyRefTableRowSizeSmall = (assemblyRefTableRowRefSize == 2);
			this._ProcessorOffset = 0;
			this._AssemblyRefOffset = this._ProcessorOffset + 4;
			this.RowSize = this._AssemblyRefOffset + assemblyRefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x040007A9 RID: 1961
		internal readonly int NumberOfRows;

		// Token: 0x040007AA RID: 1962
		private readonly bool _IsAssemblyRefTableRowSizeSmall;

		// Token: 0x040007AB RID: 1963
		private readonly int _ProcessorOffset;

		// Token: 0x040007AC RID: 1964
		private readonly int _AssemblyRefOffset;

		// Token: 0x040007AD RID: 1965
		internal readonly int RowSize;

		// Token: 0x040007AE RID: 1966
		internal readonly MemoryBlock Block;
	}
}
