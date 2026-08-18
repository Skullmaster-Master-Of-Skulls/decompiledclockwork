using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E5 RID: 229
	internal struct FieldTableReader
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00018264 File Offset: 0x00016464
		internal FieldTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
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

		// Token: 0x060008AC RID: 2220 RVA: 0x000182D4 File Offset: 0x000164D4
		internal StringHandle GetName(FieldDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00018314 File Offset: 0x00016514
		internal FieldAttributes GetFlags(FieldDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (FieldAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00018348 File Offset: 0x00016548
		internal BlobHandle GetSignature(FieldDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x040006BF RID: 1727
		internal readonly int NumberOfRows;

		// Token: 0x040006C0 RID: 1728
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040006C1 RID: 1729
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040006C2 RID: 1730
		private readonly int _FlagsOffset;

		// Token: 0x040006C3 RID: 1731
		private readonly int _NameOffset;

		// Token: 0x040006C4 RID: 1732
		private readonly int _SignatureOffset;

		// Token: 0x040006C5 RID: 1733
		internal readonly int RowSize;

		// Token: 0x040006C6 RID: 1734
		internal readonly MemoryBlock Block;
	}
}
