using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000106 RID: 262
	internal struct AssemblyRefOSTableReader
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x0001A798 File Offset: 0x00018998
		internal AssemblyRefOSTableReader(int numberOfRows, int assemblyRefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsAssemblyRefTableRowRefSizeSmall = (assemblyRefTableRowRefSize == 2);
			this._OSPlatformIdOffset = 0;
			this._OSMajorVersionIdOffset = this._OSPlatformIdOffset + 4;
			this._OSMinorVersionIdOffset = this._OSMajorVersionIdOffset + 4;
			this._AssemblyRefOffset = this._OSMinorVersionIdOffset + 4;
			this.RowSize = this._AssemblyRefOffset + assemblyRefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x040007AF RID: 1967
		internal readonly int NumberOfRows;

		// Token: 0x040007B0 RID: 1968
		private readonly bool _IsAssemblyRefTableRowRefSizeSmall;

		// Token: 0x040007B1 RID: 1969
		private readonly int _OSPlatformIdOffset;

		// Token: 0x040007B2 RID: 1970
		private readonly int _OSMajorVersionIdOffset;

		// Token: 0x040007B3 RID: 1971
		private readonly int _OSMinorVersionIdOffset;

		// Token: 0x040007B4 RID: 1972
		private readonly int _AssemblyRefOffset;

		// Token: 0x040007B5 RID: 1973
		internal readonly int RowSize;

		// Token: 0x040007B6 RID: 1974
		internal readonly MemoryBlock Block;
	}
}
