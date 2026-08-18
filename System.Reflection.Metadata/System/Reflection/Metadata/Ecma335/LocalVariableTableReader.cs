using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011D RID: 285
	internal struct LocalVariableTableReader
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0001C014 File Offset: 0x0001A214
		internal LocalVariableTableReader(int numberOfRows, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._attributesOffset = 0;
			this._indexOffset = this._attributesOffset + 2;
			this._nameOffset = this._indexOffset + 2;
			this.RowSize = this._nameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001C07C File Offset: 0x0001A27C
		internal LocalVariableAttributes GetAttributes(LocalVariableHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (LocalVariableAttributes)this.Block.PeekUInt16(num + this._attributesOffset);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
		internal ushort GetIndex(LocalVariableHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._indexOffset);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		internal StringHandle GetName(LocalVariableHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._nameOffset, this._isStringHeapRefSizeSmall));
		}

		// Token: 0x0400086D RID: 2157
		internal readonly int NumberOfRows;

		// Token: 0x0400086E RID: 2158
		private readonly bool _isStringHeapRefSizeSmall;

		// Token: 0x0400086F RID: 2159
		private readonly int _attributesOffset;

		// Token: 0x04000870 RID: 2160
		private readonly int _indexOffset;

		// Token: 0x04000871 RID: 2161
		private readonly int _nameOffset;

		// Token: 0x04000872 RID: 2162
		internal readonly int RowSize;

		// Token: 0x04000873 RID: 2163
		internal readonly MemoryBlock Block;
	}
}
