using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EA RID: 234
	internal struct InterfaceImplTableReader
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x000187B8 File Offset: 0x000169B8
		internal InterfaceImplTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, int typeDefOrRefRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = (typeDefTableRowRefSize == 2);
			this._IsTypeDefOrRefRefSizeSmall = (typeDefOrRefRefSize == 2);
			this._ClassOffset = 0;
			this._InterfaceOffset = this._ClassOffset + typeDefTableRowRefSize;
			this.RowSize = this._InterfaceOffset + typeDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.InterfaceImpl);
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00018830 File Offset: 0x00016A30
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ClassOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00018860 File Offset: 0x00016A60
		internal void GetInterfaceImplRange(TypeDefinitionHandle typeDef, out int firstImplRowId, out int lastImplRowId)
		{
			int rowId = typeDef.RowId;
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ClassOffset, (uint)rowId, this._IsTypeDefTableRowRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000188B8 File Offset: 0x00016AB8
		internal EntityHandle GetInterface(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._InterfaceOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x040006E4 RID: 1764
		internal readonly int NumberOfRows;

		// Token: 0x040006E5 RID: 1765
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x040006E6 RID: 1766
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x040006E7 RID: 1767
		private readonly int _ClassOffset;

		// Token: 0x040006E8 RID: 1768
		private readonly int _InterfaceOffset;

		// Token: 0x040006E9 RID: 1769
		internal readonly int RowSize;

		// Token: 0x040006EA RID: 1770
		internal readonly MemoryBlock Block;
	}
}
