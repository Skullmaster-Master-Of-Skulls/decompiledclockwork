using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011F RID: 287
	internal struct StateMachineMethodTableReader
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		internal StateMachineMethodTableReader(int numberOfRows, bool declaredSorted, int methodRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isMethodRefSizeSmall = (methodRefSize == 2);
			this._kickoffMethodOffset = methodRefSize;
			this.RowSize = this._kickoffMethodOffset + methodRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (numberOfRows > 0 && !declaredSorted)
			{
				Throw.TableNotSorted(TableIndex.StateMachineMethod);
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001C250 File Offset: 0x0001A450
		internal MethodDefinitionHandle FindKickoffMethod(int moveNextMethodRowId)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, 0, (uint)moveNextMethodRowId, this._isMethodRefSizeSmall);
			if (num < 0)
			{
				return default(MethodDefinitionHandle);
			}
			return this.GetKickoffMethod(num + 1);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001C298 File Offset: 0x0001A498
		private MethodDefinitionHandle GetKickoffMethod(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._kickoffMethodOffset, this._isMethodRefSizeSmall));
		}

		// Token: 0x0400087B RID: 2171
		internal readonly int NumberOfRows;

		// Token: 0x0400087C RID: 2172
		private readonly bool _isMethodRefSizeSmall;

		// Token: 0x0400087D RID: 2173
		private const int MoveNextMethodOffset = 0;

		// Token: 0x0400087E RID: 2174
		private readonly int _kickoffMethodOffset;

		// Token: 0x0400087F RID: 2175
		internal readonly int RowSize;

		// Token: 0x04000880 RID: 2176
		internal readonly MemoryBlock Block;
	}
}
