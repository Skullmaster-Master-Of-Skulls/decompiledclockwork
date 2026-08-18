using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000109 RID: 265
	internal struct ManifestResourceTableReader
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x0001AB40 File Offset: 0x00018D40
		internal ManifestResourceTableReader(int numberOfRows, int implementationRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsImplementationRefSizeSmall = (implementationRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._OffsetOffset = 0;
			this._FlagsOffset = this._OffsetOffset + 4;
			this._NameOffset = this._FlagsOffset + 4;
			this._ImplementationOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._ImplementationOffset + implementationRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		internal StringHandle GetName(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001AC00 File Offset: 0x00018E00
		internal EntityHandle GetImplementation(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ImplementationTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ImplementationOffset, this._IsImplementationRefSizeSmall));
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001AC40 File Offset: 0x00018E40
		internal uint GetOffset(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._OffsetOffset);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001AC74 File Offset: 0x00018E74
		internal ManifestResourceAttributes GetFlags(ManifestResourceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (ManifestResourceAttributes)this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x040007C9 RID: 1993
		internal readonly int NumberOfRows;

		// Token: 0x040007CA RID: 1994
		private readonly bool _IsImplementationRefSizeSmall;

		// Token: 0x040007CB RID: 1995
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040007CC RID: 1996
		private readonly int _OffsetOffset;

		// Token: 0x040007CD RID: 1997
		private readonly int _FlagsOffset;

		// Token: 0x040007CE RID: 1998
		private readonly int _NameOffset;

		// Token: 0x040007CF RID: 1999
		private readonly int _ImplementationOffset;

		// Token: 0x040007D0 RID: 2000
		internal readonly int RowSize;

		// Token: 0x040007D1 RID: 2001
		internal readonly MemoryBlock Block;
	}
}
