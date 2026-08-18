using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000F5 RID: 245
	internal struct EventTableReader
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x000195D4 File Offset: 0x000177D4
		internal EventTableReader(int numberOfRows, int typeDefOrRefRefSize, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefOrRefRefSizeSmall = (typeDefOrRefRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._FlagsOffset = 0;
			this._NameOffset = this._FlagsOffset + 2;
			this._EventTypeOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._EventTypeOffset + typeDefOrRefRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00019644 File Offset: 0x00017844
		internal EventAttributes GetFlags(EventDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (EventAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00019678 File Offset: 0x00017878
		internal StringHandle GetName(EventDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000196B4 File Offset: 0x000178B4
		internal EntityHandle GetEventType(EventDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return TypeDefOrRefTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._EventTypeOffset, this._IsTypeDefOrRefRefSizeSmall));
		}

		// Token: 0x04000733 RID: 1843
		internal int NumberOfRows;

		// Token: 0x04000734 RID: 1844
		private readonly bool _IsTypeDefOrRefRefSizeSmall;

		// Token: 0x04000735 RID: 1845
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000736 RID: 1846
		private readonly int _FlagsOffset;

		// Token: 0x04000737 RID: 1847
		private readonly int _NameOffset;

		// Token: 0x04000738 RID: 1848
		private readonly int _EventTypeOffset;

		// Token: 0x04000739 RID: 1849
		internal readonly int RowSize;

		// Token: 0x0400073A RID: 1850
		internal MemoryBlock Block;
	}
}
