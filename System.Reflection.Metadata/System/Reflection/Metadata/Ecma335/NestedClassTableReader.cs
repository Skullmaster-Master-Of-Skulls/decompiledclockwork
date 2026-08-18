using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200010A RID: 266
	internal struct NestedClassTableReader
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		internal NestedClassTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = (typeDefTableRowRefSize == 2);
			this._NestedClassOffset = 0;
			this._EnclosingClassOffset = this._NestedClassOffset + typeDefTableRowRefSize;
			this.RowSize = this._EnclosingClassOffset + typeDefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.NestedClass);
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001AD14 File Offset: 0x00018F14
		internal TypeDefinitionHandle GetNestedClass(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._NestedClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001AD50 File Offset: 0x00018F50
		internal TypeDefinitionHandle GetEnclosingClass(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._EnclosingClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001AD8C File Offset: 0x00018F8C
		internal TypeDefinitionHandle FindEnclosingType(TypeDefinitionHandle nestedTypeDef)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._NestedClassOffset, (uint)nestedTypeDef.RowId, this._IsTypeDefTableRowRefSizeSmall);
			if (num == -1)
			{
				return default(TypeDefinitionHandle);
			}
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num * this.RowSize + this._EnclosingClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001AE00 File Offset: 0x00019000
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._NestedClassOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x040007D2 RID: 2002
		internal readonly int NumberOfRows;

		// Token: 0x040007D3 RID: 2003
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x040007D4 RID: 2004
		private readonly int _NestedClassOffset;

		// Token: 0x040007D5 RID: 2005
		private readonly int _EnclosingClassOffset;

		// Token: 0x040007D6 RID: 2006
		internal readonly int RowSize;

		// Token: 0x040007D7 RID: 2007
		internal readonly MemoryBlock Block;
	}
}
