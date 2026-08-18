using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000103 RID: 259
	internal struct AssemblyOSTableReader
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0001A478 File Offset: 0x00018678
		internal AssemblyOSTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._OSPlatformIdOffset = 0;
			this._OSMajorVersionIdOffset = this._OSPlatformIdOffset + 4;
			this._OSMinorVersionIdOffset = this._OSMajorVersionIdOffset + 4;
			this.RowSize = this._OSMinorVersionIdOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x04000794 RID: 1940
		internal readonly int NumberOfRows;

		// Token: 0x04000795 RID: 1941
		private readonly int _OSPlatformIdOffset;

		// Token: 0x04000796 RID: 1942
		private readonly int _OSMajorVersionIdOffset;

		// Token: 0x04000797 RID: 1943
		private readonly int _OSMinorVersionIdOffset;

		// Token: 0x04000798 RID: 1944
		internal readonly int RowSize;

		// Token: 0x04000799 RID: 1945
		internal readonly MemoryBlock Block;
	}
}
