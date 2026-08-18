using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011C RID: 284
	internal struct LocalScopeTableReader
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x0001BD84 File Offset: 0x00019F84
		internal LocalScopeTableReader(int numberOfRows, bool declaredSorted, int methodRefSize, int importScopeRefSize, int localVariableRefSize, int localConstantRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._isMethodRefSmall = (methodRefSize == 2);
			this._isImportScopeRefSmall = (importScopeRefSize == 2);
			this._isLocalVariableRefSmall = (localVariableRefSize == 2);
			this._isLocalConstantRefSmall = (localConstantRefSize == 2);
			this._importScopeOffset = 0 + methodRefSize;
			this._variableListOffset = this._importScopeOffset + importScopeRefSize;
			this._constantListOffset = this._variableListOffset + localVariableRefSize;
			this._startOffsetOffset = this._constantListOffset + localConstantRefSize;
			this._lengthOffset = this._startOffsetOffset + 4;
			this.RowSize = this._lengthOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (numberOfRows > 0 && !declaredSorted)
			{
				Throw.TableNotSorted(TableIndex.LocalScope);
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001BE3C File Offset: 0x0001A03C
		internal MethodDefinitionHandle GetMethod(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num + 0, this._isMethodRefSmall));
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001BE70 File Offset: 0x0001A070
		internal ImportScopeHandle GetImportScope(LocalScopeHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return ImportScopeHandle.FromRowId(this.Block.PeekReference(num + this._importScopeOffset, this._isImportScopeRefSmall));
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		internal int GetVariableStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._variableListOffset, this._isLocalVariableRefSmall);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001BEE4 File Offset: 0x0001A0E4
		internal int GetConstantStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._constantListOffset, this._isLocalConstantRefSmall);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001BF18 File Offset: 0x0001A118
		internal int GetStartOffset(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._startOffsetOffset);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001BF48 File Offset: 0x0001A148
		internal int GetLength(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._lengthOffset);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001BF78 File Offset: 0x0001A178
		internal int GetEndOffset(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			long num2 = (long)((ulong)(this.Block.PeekUInt32(num + this._startOffsetOffset) + this.Block.PeekUInt32(num + this._lengthOffset)));
			if ((long)((int)num2) != num2)
			{
				MemoryBlock.ThrowValueOverflow();
			}
			return (int)num2;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001BFCC File Offset: 0x0001A1CC
		internal void GetLocalScopeRange(int methodDefRid, out int firstScopeRowId, out int lastScopeRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, 0, (uint)methodDefRid, this._isMethodRefSmall, out num, out num2);
			if (num == -1)
			{
				firstScopeRowId = 1;
				lastScopeRowId = 0;
				return;
			}
			firstScopeRowId = num + 1;
			lastScopeRowId = num2 + 1;
		}

		// Token: 0x04000860 RID: 2144
		internal readonly int NumberOfRows;

		// Token: 0x04000861 RID: 2145
		private readonly bool _isMethodRefSmall;

		// Token: 0x04000862 RID: 2146
		private readonly bool _isImportScopeRefSmall;

		// Token: 0x04000863 RID: 2147
		private readonly bool _isLocalConstantRefSmall;

		// Token: 0x04000864 RID: 2148
		private readonly bool _isLocalVariableRefSmall;

		// Token: 0x04000865 RID: 2149
		private const int MethodOffset = 0;

		// Token: 0x04000866 RID: 2150
		private readonly int _importScopeOffset;

		// Token: 0x04000867 RID: 2151
		private readonly int _variableListOffset;

		// Token: 0x04000868 RID: 2152
		private readonly int _constantListOffset;

		// Token: 0x04000869 RID: 2153
		private readonly int _startOffsetOffset;

		// Token: 0x0400086A RID: 2154
		private readonly int _lengthOffset;

		// Token: 0x0400086B RID: 2155
		internal readonly int RowSize;

		// Token: 0x0400086C RID: 2156
		internal readonly MemoryBlock Block;
	}
}
