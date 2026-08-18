using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FB RID: 251
	internal struct ModuleRefTableReader
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00019D4D File Offset: 0x00017F4D
		internal ModuleRefTableReader(int numberOfRows, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._NameOffset = 0;
			this.RowSize = this._NameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00019D8C File Offset: 0x00017F8C
		internal StringHandle GetName(ModuleReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0400075F RID: 1887
		internal readonly int NumberOfRows;

		// Token: 0x04000760 RID: 1888
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000761 RID: 1889
		private readonly int _NameOffset;

		// Token: 0x04000762 RID: 1890
		internal readonly int RowSize;

		// Token: 0x04000763 RID: 1891
		internal readonly MemoryBlock Block;
	}
}
