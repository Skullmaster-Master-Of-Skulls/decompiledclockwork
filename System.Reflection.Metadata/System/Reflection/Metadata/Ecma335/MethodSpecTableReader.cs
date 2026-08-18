using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200010C RID: 268
	internal struct MethodSpecTableReader
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x0001B078 File Offset: 0x00019278
		internal MethodSpecTableReader(int numberOfRows, int methodDefOrRefRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMethodDefOrRefRefSizeSmall = (methodDefOrRefRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._MethodOffset = 0;
			this._InstantiationOffset = this._MethodOffset + methodDefOrRefRefSize;
			this.RowSize = this._InstantiationOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001B0DC File Offset: 0x000192DC
		internal EntityHandle GetMethod(MethodSpecificationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MethodDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MethodOffset, this._IsMethodDefOrRefRefSizeSmall));
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001B11C File Offset: 0x0001931C
		internal BlobHandle GetInstantiation(MethodSpecificationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._InstantiationOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x040007E1 RID: 2017
		internal readonly int NumberOfRows;

		// Token: 0x040007E2 RID: 2018
		private readonly bool _IsMethodDefOrRefRefSizeSmall;

		// Token: 0x040007E3 RID: 2019
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007E4 RID: 2020
		private readonly int _MethodOffset;

		// Token: 0x040007E5 RID: 2021
		private readonly int _InstantiationOffset;

		// Token: 0x040007E6 RID: 2022
		internal readonly int RowSize;

		// Token: 0x040007E7 RID: 2023
		internal readonly MemoryBlock Block;
	}
}
