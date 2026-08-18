using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E9 RID: 233
	internal struct ParamTableReader
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x000186A8 File Offset: 0x000168A8
		internal ParamTableReader(int numberOfRows, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._FlagsOffset = 0;
			this._SequenceOffset = this._FlagsOffset + 2;
			this._NameOffset = this._SequenceOffset + 2;
			this.RowSize = this._NameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00018710 File Offset: 0x00016910
		internal ParameterAttributes GetFlags(ParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (ParameterAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00018744 File Offset: 0x00016944
		internal ushort GetSequence(ParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._SequenceOffset);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00018778 File Offset: 0x00016978
		internal StringHandle GetName(ParameterHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x040006DD RID: 1757
		internal readonly int NumberOfRows;

		// Token: 0x040006DE RID: 1758
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040006DF RID: 1759
		private readonly int _FlagsOffset;

		// Token: 0x040006E0 RID: 1760
		private readonly int _SequenceOffset;

		// Token: 0x040006E1 RID: 1761
		private readonly int _NameOffset;

		// Token: 0x040006E2 RID: 1762
		internal readonly int RowSize;

		// Token: 0x040006E3 RID: 1763
		internal readonly MemoryBlock Block;
	}
}
