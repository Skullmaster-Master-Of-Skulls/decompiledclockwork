using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F9 RID: 249
	internal struct MethodSemanticsTableReader
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0001999C File Offset: 0x00017B9C
		internal MethodSemanticsTableReader(int numberOfRows, bool declaredSorted, int methodTableRowRefSize, int hasSemanticRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMethodTableRowRefSizeSmall = (methodTableRowRefSize == 2);
			this._IsHasSemanticRefSizeSmall = (hasSemanticRefSize == 2);
			this._SemanticsFlagOffset = 0;
			this._MethodOffset = this._SemanticsFlagOffset + 2;
			this._AssociationOffset = this._MethodOffset + methodTableRowRefSize;
			this.RowSize = this._AssociationOffset + hasSemanticRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.MethodSemantics);
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00019A20 File Offset: 0x00017C20
		internal MethodDefinitionHandle GetMethod(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._MethodOffset, this._IsMethodTableRowRefSizeSmall));
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00019A5C File Offset: 0x00017C5C
		internal MethodSemanticsAttributes GetSemantics(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (MethodSemanticsAttributes)this.Block.PeekUInt16(num + this._SemanticsFlagOffset);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00019A8C File Offset: 0x00017C8C
		internal EntityHandle GetAssociation(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return HasSemanticsTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._AssociationOffset, this._IsHasSemanticRefSizeSmall));
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00019AC8 File Offset: 0x00017CC8
		internal int FindSemanticMethodsForEvent(EventDefinitionHandle eventDef, out ushort methodCount)
		{
			methodCount = 0;
			uint searchCodedTag = HasSemanticsTag.ConvertEventHandleToTag(eventDef);
			return this.BinarySearchTag(searchCodedTag, ref methodCount);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00019AE8 File Offset: 0x00017CE8
		internal int FindSemanticMethodsForProperty(PropertyDefinitionHandle propertyDef, out ushort methodCount)
		{
			methodCount = 0;
			uint searchCodedTag = HasSemanticsTag.ConvertPropertyHandleToTag(propertyDef);
			return this.BinarySearchTag(searchCodedTag, ref methodCount);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00019B08 File Offset: 0x00017D08
		private int BinarySearchTag(uint searchCodedTag, ref ushort methodCount)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._AssociationOffset, searchCodedTag, this._IsHasSemanticRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				methodCount = 0;
				return 0;
			}
			methodCount = (ushort)(num2 - num + 1);
			return num + 1;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00019B54 File Offset: 0x00017D54
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._AssociationOffset, this._IsHasSemanticRefSizeSmall);
		}

		// Token: 0x0400074F RID: 1871
		internal readonly int NumberOfRows;

		// Token: 0x04000750 RID: 1872
		private readonly bool _IsMethodTableRowRefSizeSmall;

		// Token: 0x04000751 RID: 1873
		private readonly bool _IsHasSemanticRefSizeSmall;

		// Token: 0x04000752 RID: 1874
		private readonly int _SemanticsFlagOffset;

		// Token: 0x04000753 RID: 1875
		private readonly int _MethodOffset;

		// Token: 0x04000754 RID: 1876
		private readonly int _AssociationOffset;

		// Token: 0x04000755 RID: 1877
		internal readonly int RowSize;

		// Token: 0x04000756 RID: 1878
		internal readonly MemoryBlock Block;
	}
}
