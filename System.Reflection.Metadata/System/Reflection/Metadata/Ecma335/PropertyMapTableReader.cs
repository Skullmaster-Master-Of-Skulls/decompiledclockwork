using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F6 RID: 246
	internal struct PropertyMapTableReader
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x000196F0 File Offset: 0x000178F0
		internal PropertyMapTableReader(int numberOfRows, int typeDefTableRowRefSize, int propertyRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = (typeDefTableRowRefSize == 2);
			this._IsPropertyRefSizeSmall = (propertyRefSize == 2);
			this._ParentOffset = 0;
			this._PropertyListOffset = this._ParentOffset + typeDefTableRowRefSize;
			this.RowSize = this._PropertyListOffset + propertyRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00019754 File Offset: 0x00017954
		internal int FindPropertyMapRowIdFor(TypeDefinitionHandle typeDef)
		{
			return this.Block.LinearSearchReference(this.RowSize, this._ParentOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall) + 1;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001978C File Offset: 0x0001798C
		internal TypeDefinitionHandle GetParentType(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000197C8 File Offset: 0x000179C8
		internal int GetPropertyListStartFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._PropertyListOffset, this._IsPropertyRefSizeSmall);
		}

		// Token: 0x0400073B RID: 1851
		internal readonly int NumberOfRows;

		// Token: 0x0400073C RID: 1852
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x0400073D RID: 1853
		private readonly bool _IsPropertyRefSizeSmall;

		// Token: 0x0400073E RID: 1854
		private readonly int _ParentOffset;

		// Token: 0x0400073F RID: 1855
		private readonly int _PropertyListOffset;

		// Token: 0x04000740 RID: 1856
		internal readonly int RowSize;

		// Token: 0x04000741 RID: 1857
		internal readonly MemoryBlock Block;
	}
}
