using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000107 RID: 263
	internal struct FileTableReader
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x0001A80C File Offset: 0x00018A0C
		internal FileTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 4;
			this._HashValueOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._HashValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001A87C File Offset: 0x00018A7C
		internal BlobHandle GetHashValue(AssemblyFileHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._HashValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001A8BC File Offset: 0x00018ABC
		internal uint GetFlags(AssemblyFileHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001A8F0 File Offset: 0x00018AF0
		internal StringHandle GetName(AssemblyFileHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x040007B7 RID: 1975
		internal readonly int NumberOfRows;

		// Token: 0x040007B8 RID: 1976
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040007B9 RID: 1977
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040007BA RID: 1978
		private readonly int _FlagsOffset;

		// Token: 0x040007BB RID: 1979
		private readonly int _NameOffset;

		// Token: 0x040007BC RID: 1980
		private readonly int _HashValueOffset;

		// Token: 0x040007BD RID: 1981
		internal readonly int RowSize;

		// Token: 0x040007BE RID: 1982
		internal readonly MemoryBlock Block;
	}
}
