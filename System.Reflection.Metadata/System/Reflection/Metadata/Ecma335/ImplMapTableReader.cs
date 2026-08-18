using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000FD RID: 253
	internal struct ImplMapTableReader
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x00019E4C File Offset: 0x0001804C
		internal ImplMapTableReader(int numberOfRows, bool declaredSorted, int moduleRefTableRowRefSize, int memberForwardedRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsModuleRefTableRowRefSizeSmall = (moduleRefTableRowRefSize == 2);
			this._IsMemberForwardRowRefSizeSmall = (memberForwardedRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._FlagsOffset = 0;
			this._MemberForwardedOffset = this._FlagsOffset + 2;
			this._ImportNameOffset = this._MemberForwardedOffset + memberForwardedRefSize;
			this._ImportScopeOffset = this._ImportNameOffset + stringHeapRefSize;
			this.RowSize = this._ImportScopeOffset + moduleRefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.ImplMap);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00019EEC File Offset: 0x000180EC
		internal MethodImport GetImport(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			MethodImportAttributes attributes = (MethodImportAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
			StringHandle name = StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._ImportNameOffset, this._IsStringHeapRefSizeSmall));
			ModuleReferenceHandle module = ModuleReferenceHandle.FromRowId(this.Block.PeekReference(num + this._ImportScopeOffset, this._IsModuleRefTableRowRefSizeSmall));
			return new MethodImport(attributes, name, module);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00019F68 File Offset: 0x00018168
		internal EntityHandle GetMemberForwarded(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MemberForwardedTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._MemberForwardedOffset, this._IsMemberForwardRowRefSizeSmall));
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00019FA4 File Offset: 0x000181A4
		internal int FindImplForMethod(MethodDefinitionHandle methodDef)
		{
			uint searchCodedTag = MemberForwardedTag.ConvertMethodDefToTag(methodDef);
			return this.BinarySearchTag(searchCodedTag);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00019FC0 File Offset: 0x000181C0
		private int BinarySearchTag(uint searchCodedTag)
		{
			return this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._MemberForwardedOffset, searchCodedTag, this._IsMemberForwardRowRefSizeSmall) + 1;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00019FF8 File Offset: 0x000181F8
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._MemberForwardedOffset, this._IsMemberForwardRowRefSizeSmall);
		}

		// Token: 0x04000769 RID: 1897
		internal readonly int NumberOfRows;

		// Token: 0x0400076A RID: 1898
		private readonly bool _IsModuleRefTableRowRefSizeSmall;

		// Token: 0x0400076B RID: 1899
		private readonly bool _IsMemberForwardRowRefSizeSmall;

		// Token: 0x0400076C RID: 1900
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400076D RID: 1901
		private readonly int _FlagsOffset;

		// Token: 0x0400076E RID: 1902
		private readonly int _MemberForwardedOffset;

		// Token: 0x0400076F RID: 1903
		private readonly int _ImportNameOffset;

		// Token: 0x04000770 RID: 1904
		private readonly int _ImportScopeOffset;

		// Token: 0x04000771 RID: 1905
		internal readonly int RowSize;

		// Token: 0x04000772 RID: 1906
		internal readonly MemoryBlock Block;
	}
}
