using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FA RID: 250
	internal struct MethodImplTableReader
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x00019B84 File Offset: 0x00017D84
		internal MethodImplTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, int methodDefOrRefRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = (typeDefTableRowRefSize == 2);
			this._IsMethodDefOrRefRefSizeSmall = (methodDefOrRefRefSize == 2);
			this._ClassOffset = 0;
			this._MethodBodyOffset = this._ClassOffset + typeDefTableRowRefSize;
			this._MethodDeclarationOffset = this._MethodBodyOffset + methodDefOrRefRefSize;
			this.RowSize = this._MethodDeclarationOffset + methodDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.MethodImpl);
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00019C0C File Offset: 0x00017E0C
		internal TypeDefinitionHandle GetClass(MethodImplementationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00019C4C File Offset: 0x00017E4C
		internal EntityHandle GetMethodBody(MethodImplementationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MethodDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MethodBodyOffset, this._IsMethodDefOrRefRefSizeSmall));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00019C8C File Offset: 0x00017E8C
		internal EntityHandle GetMethodDeclaration(MethodImplementationHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MethodDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MethodDeclarationOffset, this._IsMethodDefOrRefRefSizeSmall));
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00019CCC File Offset: 0x00017ECC
		internal void GetMethodImplRange(TypeDefinitionHandle typeDef, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ClassOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00019D20 File Offset: 0x00017F20
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ClassOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x04000757 RID: 1879
		internal readonly int NumberOfRows;

		// Token: 0x04000758 RID: 1880
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x04000759 RID: 1881
		private readonly bool _IsMethodDefOrRefRefSizeSmall;

		// Token: 0x0400075A RID: 1882
		private readonly int _ClassOffset;

		// Token: 0x0400075B RID: 1883
		private readonly int _MethodBodyOffset;

		// Token: 0x0400075C RID: 1884
		private readonly int _MethodDeclarationOffset;

		// Token: 0x0400075D RID: 1885
		internal readonly int RowSize;

		// Token: 0x0400075E RID: 1886
		internal readonly MemoryBlock Block;
	}
}
