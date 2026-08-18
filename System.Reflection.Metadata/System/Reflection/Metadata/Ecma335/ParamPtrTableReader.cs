using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E8 RID: 232
	internal struct ParamPtrTableReader
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x0001862C File Offset: 0x0001682C
		internal ParamPtrTableReader(int numberOfRows, int paramTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsParamTableRowRefSizeSmall = (paramTableRowRefSize == 2);
			this._ParamOffset = 0;
			this.RowSize = this._ParamOffset + paramTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001866C File Offset: 0x0001686C
		internal ParameterHandle GetParamFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return ParameterHandle.FromRowId(this.Block.PeekReference(num + this._ParamOffset, this._IsParamTableRowRefSizeSmall));
		}

		// Token: 0x040006D8 RID: 1752
		internal readonly int NumberOfRows;

		// Token: 0x040006D9 RID: 1753
		private readonly bool _IsParamTableRowRefSizeSmall;

		// Token: 0x040006DA RID: 1754
		private readonly int _ParamOffset;

		// Token: 0x040006DB RID: 1755
		internal readonly int RowSize;

		// Token: 0x040006DC RID: 1756
		internal readonly MemoryBlock Block;
	}
}
