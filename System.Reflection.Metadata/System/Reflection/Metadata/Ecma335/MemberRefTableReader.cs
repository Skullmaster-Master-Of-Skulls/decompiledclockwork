using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EB RID: 235
	internal struct MemberRefTableReader
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x000188F4 File Offset: 0x00016AF4
		internal MemberRefTableReader(int numberOfRows, int memberRefParentRefSize, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMemberRefParentRefSizeSmall = (memberRefParentRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._ClassOffset = 0;
			this._NameOffset = this._ClassOffset + memberRefParentRefSize;
			this._SignatureOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._SignatureOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00018970 File Offset: 0x00016B70
		internal BlobHandle GetSignature(MemberReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000189AC File Offset: 0x00016BAC
		internal StringHandle GetName(MemberReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000189E8 File Offset: 0x00016BE8
		internal EntityHandle GetClass(MemberReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return MemberRefParentTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ClassOffset, this._IsMemberRefParentRefSizeSmall));
		}

		// Token: 0x040006EB RID: 1771
		internal int NumberOfRows;

		// Token: 0x040006EC RID: 1772
		private readonly bool _IsMemberRefParentRefSizeSmall;

		// Token: 0x040006ED RID: 1773
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040006EE RID: 1774
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040006EF RID: 1775
		private readonly int _ClassOffset;

		// Token: 0x040006F0 RID: 1776
		private readonly int _NameOffset;

		// Token: 0x040006F1 RID: 1777
		private readonly int _SignatureOffset;

		// Token: 0x040006F2 RID: 1778
		internal readonly int RowSize;

		// Token: 0x040006F3 RID: 1779
		internal MemoryBlock Block;
	}
}
