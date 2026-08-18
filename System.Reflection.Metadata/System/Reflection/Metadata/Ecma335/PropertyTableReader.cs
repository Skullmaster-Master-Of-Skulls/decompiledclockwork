using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F8 RID: 248
	internal struct PropertyTableReader
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00019878 File Offset: 0x00017A78
		internal PropertyTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 2;
			this._SignatureOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000198E8 File Offset: 0x00017AE8
		internal PropertyAttributes GetFlags(PropertyDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (PropertyAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001991C File Offset: 0x00017B1C
		internal StringHandle GetName(PropertyDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001995C File Offset: 0x00017B5C
		internal BlobHandle GetSignature(PropertyDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x04000747 RID: 1863
		internal readonly int NumberOfRows;

		// Token: 0x04000748 RID: 1864
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000749 RID: 1865
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x0400074A RID: 1866
		private readonly int _FlagsOffset;

		// Token: 0x0400074B RID: 1867
		private readonly int _NameOffset;

		// Token: 0x0400074C RID: 1868
		private readonly int _SignatureOffset;

		// Token: 0x0400074D RID: 1869
		internal readonly int RowSize;

		// Token: 0x0400074E RID: 1870
		internal readonly MemoryBlock Block;
	}
}
