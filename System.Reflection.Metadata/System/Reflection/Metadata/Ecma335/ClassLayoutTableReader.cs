using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F0 RID: 240
	internal struct ClassLayoutTableReader
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0001913C File Offset: 0x0001733C
		internal ClassLayoutTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = (typeDefTableRowRefSize == 2);
			this._PackagingSizeOffset = 0;
			this._ClassSizeOffset = this._PackagingSizeOffset + 2;
			this._ParentOffset = this._ClassSizeOffset + 4;
			this.RowSize = this._ParentOffset + typeDefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.ClassLayout);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000191B4 File Offset: 0x000173B4
		internal TypeDefinitionHandle GetParent(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000191EC File Offset: 0x000173EC
		internal ushort GetPackingSize(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._PackagingSizeOffset);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00019218 File Offset: 0x00017418
		internal uint GetClassSize(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._ClassSizeOffset);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00019243 File Offset: 0x00017443
		internal int FindRow(TypeDefinitionHandle typeDef)
		{
			return 1 + this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._ParentOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00019271 File Offset: 0x00017471
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x04000715 RID: 1813
		internal int NumberOfRows;

		// Token: 0x04000716 RID: 1814
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x04000717 RID: 1815
		private readonly int _PackagingSizeOffset;

		// Token: 0x04000718 RID: 1816
		private readonly int _ClassSizeOffset;

		// Token: 0x04000719 RID: 1817
		private readonly int _ParentOffset;

		// Token: 0x0400071A RID: 1818
		internal readonly int RowSize;

		// Token: 0x0400071B RID: 1819
		internal MemoryBlock Block;
	}
}
