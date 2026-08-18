using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F3 RID: 243
	internal struct EventMapTableReader
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0001944C File Offset: 0x0001764C
		internal EventMapTableReader(int numberOfRows, int typeDefTableRowRefSize, int eventRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = (typeDefTableRowRefSize == 2);
			this._IsEventRefSizeSmall = (eventRefSize == 2);
			this._ParentOffset = 0;
			this._EventListOffset = this._ParentOffset + typeDefTableRowRefSize;
			this.RowSize = this._EventListOffset + eventRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000194B0 File Offset: 0x000176B0
		internal int FindEventMapRowIdFor(TypeDefinitionHandle typeDef)
		{
			return this.Block.LinearSearchReference(this.RowSize, this._ParentOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall) + 1;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000194E8 File Offset: 0x000176E8
		internal TypeDefinitionHandle GetParentType(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00019524 File Offset: 0x00017724
		internal int GetEventListStartFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._EventListOffset, this._IsEventRefSizeSmall);
		}

		// Token: 0x04000727 RID: 1831
		internal readonly int NumberOfRows;

		// Token: 0x04000728 RID: 1832
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x04000729 RID: 1833
		private readonly bool _IsEventRefSizeSmall;

		// Token: 0x0400072A RID: 1834
		private readonly int _ParentOffset;

		// Token: 0x0400072B RID: 1835
		private readonly int _EventListOffset;

		// Token: 0x0400072C RID: 1836
		internal readonly int RowSize;

		// Token: 0x0400072D RID: 1837
		internal readonly MemoryBlock Block;
	}
}
