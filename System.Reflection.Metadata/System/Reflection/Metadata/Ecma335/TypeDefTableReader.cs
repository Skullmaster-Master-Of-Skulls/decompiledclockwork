using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E3 RID: 227
	internal struct TypeDefTableReader
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x00017E70 File Offset: 0x00016070
		internal TypeDefTableReader(int numberOfRows, int fieldRefSize, int methodRefSize, int typeDefOrRefRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldRefSizeSmall = (fieldRefSize == 2);
			this._IsMethodRefSizeSmall = (methodRefSize == 2);
			this._IsTypeDefOrRefRefSizeSmall = (typeDefOrRefRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 4;
			this._NamespaceOffset = this._NameOffset + stringHeapRefSize;
			this._ExtendsOffset = this._NamespaceOffset + stringHeapRefSize;
			this._FieldListOffset = this._ExtendsOffset + typeDefOrRefRefSize;
			this._MethodListOffset = this._FieldListOffset + fieldRefSize;
			this.RowSize = this._MethodListOffset + methodRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00017F24 File Offset: 0x00016124
		internal TypeAttributes GetFlags(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (TypeAttributes)this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00017F58 File Offset: 0x00016158
		internal NamespaceDefinitionHandle GetNamespaceDefinition(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return NamespaceDefinitionHandle.FromFullNameOffset(this.Block.PeekHeapReference(num + this._NamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00017F94 File Offset: 0x00016194
		internal StringHandle GetNamespace(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00017FD0 File Offset: 0x000161D0
		internal StringHandle GetName(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001800C File Offset: 0x0001620C
		internal EntityHandle GetExtends(TypeDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ExtendsOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00018048 File Offset: 0x00016248
		internal int GetFieldStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._FieldListOffset, this._IsFieldRefSizeSmall);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001807C File Offset: 0x0001627C
		internal int GetMethodStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._MethodListOffset, this._IsMethodRefSizeSmall);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000180B0 File Offset: 0x000162B0
		internal TypeDefinitionHandle FindTypeContainingMethod(int methodDefOrPtrRowId, int numberOfMethods)
		{
			int numberOfRows = this.NumberOfRows;
			int i = this.Block.BinarySearchForSlot(numberOfRows, this.RowSize, this._MethodListOffset, (uint)methodDefOrPtrRowId, this._IsMethodRefSizeSmall) + 1;
			if (i == 0)
			{
				return default(TypeDefinitionHandle);
			}
			if (i <= numberOfRows)
			{
				if (this.GetMethodStart(i) == methodDefOrPtrRowId)
				{
					while (i < numberOfRows)
					{
						int num = i + 1;
						if (this.GetMethodStart(num) != methodDefOrPtrRowId)
						{
							break;
						}
						i = num;
					}
				}
				return TypeDefinitionHandle.FromRowId(i);
			}
			if (methodDefOrPtrRowId <= numberOfMethods)
			{
				return TypeDefinitionHandle.FromRowId(numberOfRows);
			}
			return default(TypeDefinitionHandle);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00018134 File Offset: 0x00016334
		internal TypeDefinitionHandle FindTypeContainingField(int fieldDefOrPtrRowId, int numberOfFields)
		{
			int numberOfRows = this.NumberOfRows;
			int i = this.Block.BinarySearchForSlot(numberOfRows, this.RowSize, this._FieldListOffset, (uint)fieldDefOrPtrRowId, this._IsFieldRefSizeSmall) + 1;
			if (i == 0)
			{
				return default(TypeDefinitionHandle);
			}
			if (i <= numberOfRows)
			{
				if (this.GetFieldStart(i) == fieldDefOrPtrRowId)
				{
					while (i < numberOfRows)
					{
						int num = i + 1;
						if (this.GetFieldStart(num) != fieldDefOrPtrRowId)
						{
							break;
						}
						i = num;
					}
				}
				return TypeDefinitionHandle.FromRowId(i);
			}
			if (fieldDefOrPtrRowId <= numberOfFields)
			{
				return TypeDefinitionHandle.FromRowId(numberOfRows);
			}
			return default(TypeDefinitionHandle);
		}

		// Token: 0x040006AD RID: 1709
		internal readonly int NumberOfRows;

		// Token: 0x040006AE RID: 1710
		private readonly bool _IsFieldRefSizeSmall;

		// Token: 0x040006AF RID: 1711
		private readonly bool _IsMethodRefSizeSmall;

		// Token: 0x040006B0 RID: 1712
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x040006B1 RID: 1713
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040006B2 RID: 1714
		private readonly int _FlagsOffset;

		// Token: 0x040006B3 RID: 1715
		private readonly int _NameOffset;

		// Token: 0x040006B4 RID: 1716
		private readonly int _NamespaceOffset;

		// Token: 0x040006B5 RID: 1717
		private readonly int _ExtendsOffset;

		// Token: 0x040006B6 RID: 1718
		private readonly int _FieldListOffset;

		// Token: 0x040006B7 RID: 1719
		private readonly int _MethodListOffset;

		// Token: 0x040006B8 RID: 1720
		internal readonly int RowSize;

		// Token: 0x040006B9 RID: 1721
		internal MemoryBlock Block;
	}
}
