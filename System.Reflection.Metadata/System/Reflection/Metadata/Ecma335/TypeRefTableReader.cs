using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E2 RID: 226
	internal struct TypeRefTableReader
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x00017D40 File Offset: 0x00015F40
		internal TypeRefTableReader(int numberOfRows, int resolutionScopeRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsResolutionScopeRefSizeSmall = (resolutionScopeRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._ResolutionScopeOffset = 0;
			this._NameOffset = this._ResolutionScopeOffset + resolutionScopeRefSize;
			this._NamespaceOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._NamespaceOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00017DB0 File Offset: 0x00015FB0
		internal EntityHandle GetResolutionScope(TypeReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ResolutionScopeTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ResolutionScopeOffset, this._IsResolutionScopeRefSizeSmall));
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00017DF0 File Offset: 0x00015FF0
		internal StringHandle GetName(TypeReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00017E30 File Offset: 0x00016030
		internal StringHandle GetNamespace(TypeReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NamespaceOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x040006A5 RID: 1701
		internal readonly int NumberOfRows;

		// Token: 0x040006A6 RID: 1702
		private readonly bool _IsResolutionScopeRefSizeSmall;

		// Token: 0x040006A7 RID: 1703
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040006A8 RID: 1704
		private readonly int _ResolutionScopeOffset;

		// Token: 0x040006A9 RID: 1705
		private readonly int _NameOffset;

		// Token: 0x040006AA RID: 1706
		private readonly int _NamespaceOffset;

		// Token: 0x040006AB RID: 1707
		internal readonly int RowSize;

		// Token: 0x040006AC RID: 1708
		internal readonly MemoryBlock Block;
	}
}
