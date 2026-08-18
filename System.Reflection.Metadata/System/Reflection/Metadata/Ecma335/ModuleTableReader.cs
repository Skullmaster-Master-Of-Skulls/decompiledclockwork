using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E1 RID: 225
	internal struct ModuleTableReader
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x00017BE0 File Offset: 0x00015DE0
		internal ModuleTableReader(int numberOfRows, int stringHeapRefSize, int guidHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._IsGUIDHeapRefSizeSmall = (guidHeapRefSize == 2);
			this._GenerationOffset = 0;
			this._NameOffset = this._GenerationOffset + 2;
			this._MVIdOffset = this._NameOffset + stringHeapRefSize;
			this._EnCIdOffset = this._MVIdOffset + guidHeapRefSize;
			this._EnCBaseIdOffset = this._EnCIdOffset + guidHeapRefSize;
			this.RowSize = this._EnCBaseIdOffset + guidHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00017C6C File Offset: 0x00015E6C
		internal ushort GetGeneration()
		{
			return this.Block.PeekUInt16(this._GenerationOffset);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00017C90 File Offset: 0x00015E90
		internal StringHandle GetName()
		{
			return StringHandle.FromOffset(this.Block.PeekHeapReference(this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00017CBC File Offset: 0x00015EBC
		internal GuidHandle GetMvid()
		{
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(this._MVIdOffset, this._IsGUIDHeapRefSizeSmall));
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00017CE8 File Offset: 0x00015EE8
		internal GuidHandle GetEncId()
		{
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(this._EnCIdOffset, this._IsGUIDHeapRefSizeSmall));
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00017D14 File Offset: 0x00015F14
		internal GuidHandle GetEncBaseId()
		{
			return GuidHandle.FromIndex(this.Block.PeekHeapReference(this._EnCBaseIdOffset, this._IsGUIDHeapRefSizeSmall));
		}

		// Token: 0x0400069B RID: 1691
		internal readonly int NumberOfRows;

		// Token: 0x0400069C RID: 1692
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400069D RID: 1693
		private readonly bool _IsGUIDHeapRefSizeSmall;

		// Token: 0x0400069E RID: 1694
		private readonly int _GenerationOffset;

		// Token: 0x0400069F RID: 1695
		private readonly int _NameOffset;

		// Token: 0x040006A0 RID: 1696
		private readonly int _MVIdOffset;

		// Token: 0x040006A1 RID: 1697
		private readonly int _EnCIdOffset;

		// Token: 0x040006A2 RID: 1698
		private readonly int _EnCBaseIdOffset;

		// Token: 0x040006A3 RID: 1699
		internal readonly int RowSize;

		// Token: 0x040006A4 RID: 1700
		internal readonly MemoryBlock Block;
	}
}
