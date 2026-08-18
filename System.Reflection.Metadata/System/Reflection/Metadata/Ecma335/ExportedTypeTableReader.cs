using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000108 RID: 264
	internal struct ExportedTypeTableReader
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x0001A930 File Offset: 0x00018B30
		internal ExportedTypeTableReader(int numberOfRows, int implementationRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsImplementationRefSizeSmall = (implementationRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._FlagsOffset = 0;
			this._TypeDefIdOffset = this._FlagsOffset + 4;
			this._TypeNameOffset = this._TypeDefIdOffset + 4;
			this._TypeNamespaceOffset = this._TypeNameOffset + stringHeapRefSize;
			this._ImplementationOffset = this._TypeNamespaceOffset + stringHeapRefSize;
			this.RowSize = this._ImplementationOffset + implementationRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001A9BC File Offset: 0x00018BBC
		internal StringHandle GetTypeName(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._TypeNameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		internal StringHandle GetTypeNamespaceString(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._TypeNamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001AA34 File Offset: 0x00018C34
		internal NamespaceDefinitionHandle GetTypeNamespace(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return NamespaceDefinitionHandle.FromFullNameOffset(this.Block.PeekHeapReference(num + this._TypeNamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001AA70 File Offset: 0x00018C70
		internal EntityHandle GetImplementation(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return ImplementationTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ImplementationOffset, this._IsImplementationRefSizeSmall));
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001AAAC File Offset: 0x00018CAC
		internal TypeAttributes GetFlags(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (TypeAttributes)this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001AADC File Offset: 0x00018CDC
		internal int GetTypeDefId(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._TypeDefIdOffset);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001AB0C File Offset: 0x00018D0C
		internal int GetNamespace(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._TypeNamespaceOffset, this._IsStringHeapRefSizeSmall);
		}

		// Token: 0x040007BF RID: 1983
		internal readonly int NumberOfRows;

		// Token: 0x040007C0 RID: 1984
		private readonly bool _IsImplementationRefSizeSmall;

		// Token: 0x040007C1 RID: 1985
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040007C2 RID: 1986
		private readonly int _FlagsOffset;

		// Token: 0x040007C3 RID: 1987
		private readonly int _TypeDefIdOffset;

		// Token: 0x040007C4 RID: 1988
		private readonly int _TypeNameOffset;

		// Token: 0x040007C5 RID: 1989
		private readonly int _TypeNamespaceOffset;

		// Token: 0x040007C6 RID: 1990
		private readonly int _ImplementationOffset;

		// Token: 0x040007C7 RID: 1991
		internal readonly int RowSize;

		// Token: 0x040007C8 RID: 1992
		internal readonly MemoryBlock Block;
	}
}
