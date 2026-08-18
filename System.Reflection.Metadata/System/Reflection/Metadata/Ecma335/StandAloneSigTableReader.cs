using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F2 RID: 242
	internal struct StandAloneSigTableReader
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x000193D1 File Offset: 0x000175D1
		internal StandAloneSigTableReader(int numberOfRows, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._SignatureOffset = 0;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00019410 File Offset: 0x00017610
		internal BlobHandle GetSignature(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x04000722 RID: 1826
		internal readonly int NumberOfRows;

		// Token: 0x04000723 RID: 1827
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000724 RID: 1828
		private readonly int _SignatureOffset;

		// Token: 0x04000725 RID: 1829
		internal readonly int RowSize;

		// Token: 0x04000726 RID: 1830
		internal readonly MemoryBlock Block;
	}
}
