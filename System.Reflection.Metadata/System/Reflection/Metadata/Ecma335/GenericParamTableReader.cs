using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200010B RID: 267
	internal struct GenericParamTableReader
	{
		// Token: 0x06000949 RID: 2377 RVA: 0x0001AE30 File Offset: 0x00019030
		internal GenericParamTableReader(int numberOfRows, bool declaredSorted, int typeOrMethodDefRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeOrMethodDefRefSizeSmall = (typeOrMethodDefRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._NumberOffset = 0;
			this._FlagsOffset = this._NumberOffset + 2;
			this._OwnerOffset = this._FlagsOffset + 2;
			this._NameOffset = this._OwnerOffset + typeOrMethodDefRefSize;
			this.RowSize = this._NameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.GenericParam);
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001AEC4 File Offset: 0x000190C4
		internal ushort GetNumber(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._NumberOffset);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001AEF8 File Offset: 0x000190F8
		internal GenericParameterAttributes GetFlags(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (GenericParameterAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001AF2C File Offset: 0x0001912C
		internal StringHandle GetName(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001AF6C File Offset: 0x0001916C
		internal EntityHandle GetOwner(GenericParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeOrMethodDefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._OwnerOffset, this._IsTypeOrMethodDefRefSizeSmall));
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001AFAC File Offset: 0x000191AC
		internal GenericParameterHandleCollection FindGenericParametersForType(TypeDefinitionHandle typeDef)
		{
			ushort count = 0;
			uint searchCodedTag = TypeOrMethodDefTag.ConvertTypeDefRowIdToTag(typeDef);
			return new GenericParameterHandleCollection(this.BinarySearchTag(searchCodedTag, ref count), count);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001AFD4 File Offset: 0x000191D4
		internal GenericParameterHandleCollection FindGenericParametersForMethod(MethodDefinitionHandle methodDef)
		{
			ushort count = 0;
			uint searchCodedTag = TypeOrMethodDefTag.ConvertMethodDefToTag(methodDef);
			return new GenericParameterHandleCollection(this.BinarySearchTag(searchCodedTag, ref count), count);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001AFFC File Offset: 0x000191FC
		private int BinarySearchTag(uint searchCodedTag, ref ushort genericParamCount)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._OwnerOffset, searchCodedTag, this._IsTypeOrMethodDefRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				genericParamCount = 0;
				return 0;
			}
			genericParamCount = (ushort)(num2 - num + 1);
			return num + 1;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001B048 File Offset: 0x00019248
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._OwnerOffset, this._IsTypeOrMethodDefRefSizeSmall);
		}

		// Token: 0x040007D8 RID: 2008
		internal readonly int NumberOfRows;

		// Token: 0x040007D9 RID: 2009
		private readonly bool _IsTypeOrMethodDefRefSizeSmall;

		// Token: 0x040007DA RID: 2010
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040007DB RID: 2011
		private readonly int _NumberOffset;

		// Token: 0x040007DC RID: 2012
		private readonly int _FlagsOffset;

		// Token: 0x040007DD RID: 2013
		private readonly int _OwnerOffset;

		// Token: 0x040007DE RID: 2014
		private readonly int _NameOffset;

		// Token: 0x040007DF RID: 2015
		internal readonly int RowSize;

		// Token: 0x040007E0 RID: 2016
		internal readonly MemoryBlock Block;
	}
}
