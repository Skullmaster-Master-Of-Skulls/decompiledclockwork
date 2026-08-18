using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200010D RID: 269
	internal struct GenericParamConstraintTableReader
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x0001B15C File Offset: 0x0001935C
		internal GenericParamConstraintTableReader(int numberOfRows, bool declaredSorted, int genericParamTableRowRefSize, int typeDefOrRefRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsGenericParamTableRowRefSizeSmall = (genericParamTableRowRefSize == 2);
			this._IsTypeDefOrRefRefSizeSmall = (typeDefOrRefRefSize == 2);
			this._OwnerOffset = 0;
			this._ConstraintOffset = this._OwnerOffset + genericParamTableRowRefSize;
			this.RowSize = this._ConstraintOffset + typeDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.GenericParamConstraint);
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001B1D4 File Offset: 0x000193D4
		internal GenericParameterConstraintHandleCollection FindConstraintsForGenericParam(GenericParameterHandle genericParameter)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._OwnerOffset, (uint)genericParameter.RowId, this._IsGenericParamTableRowRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				return default(GenericParameterConstraintHandleCollection);
			}
			return new GenericParameterConstraintHandleCollection(num + 1, (ushort)(num2 - num + 1));
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001B230 File Offset: 0x00019430
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._OwnerOffset, this._IsGenericParamTableRowRefSizeSmall);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001B260 File Offset: 0x00019460
		internal EntityHandle GetConstraint(GenericParameterConstraintHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ConstraintOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001B2A0 File Offset: 0x000194A0
		internal GenericParameterHandle GetOwner(GenericParameterConstraintHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return GenericParameterHandle.FromRowId(this.Block.PeekReference(num + this._OwnerOffset, this._IsGenericParamTableRowRefSizeSmall));
		}

		// Token: 0x040007E8 RID: 2024
		internal readonly int NumberOfRows;

		// Token: 0x040007E9 RID: 2025
		private readonly bool _IsGenericParamTableRowRefSizeSmall;

		// Token: 0x040007EA RID: 2026
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x040007EB RID: 2027
		private readonly int _OwnerOffset;

		// Token: 0x040007EC RID: 2028
		private readonly int _ConstraintOffset;

		// Token: 0x040007ED RID: 2029
		internal readonly int RowSize;

		// Token: 0x040007EE RID: 2030
		internal readonly MemoryBlock Block;
	}
}
