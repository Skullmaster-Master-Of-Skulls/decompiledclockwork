using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E7 RID: 231
	internal struct MethodTableReader
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x00018434 File Offset: 0x00016634
		internal MethodTableReader(int numberOfRows, int paramRefSize, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsParamRefSizeSmall = (paramRefSize == 2);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._RvaOffset = 0;
			this._ImplFlagsOffset = this._RvaOffset + 4;
			this._FlagsOffset = this._ImplFlagsOffset + 2;
			this._NameOffset = this._FlagsOffset + 2;
			this._SignatureOffset = this._NameOffset + stringHeapRefSize;
			this._ParamListOffset = this._SignatureOffset + blobHeapRefSize;
			this.RowSize = this._ParamListOffset + paramRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000184DC File Offset: 0x000166DC
		internal int GetParamStart(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._ParamListOffset, this._IsParamRefSizeSmall);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00018510 File Offset: 0x00016710
		internal BlobHandle GetSignature(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._SignatureOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00018550 File Offset: 0x00016750
		internal int GetRva(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._RvaOffset);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00018584 File Offset: 0x00016784
		internal StringHandle GetName(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000185C4 File Offset: 0x000167C4
		internal MethodAttributes GetFlags(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (MethodAttributes)this.Block.PeekUInt16(num + this._FlagsOffset);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000185F8 File Offset: 0x000167F8
		internal MethodImplAttributes GetImplFlags(MethodDefinitionHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return (MethodImplAttributes)this.Block.PeekUInt16(num + this._ImplFlagsOffset);
		}

		// Token: 0x040006CC RID: 1740
		internal readonly int NumberOfRows;

		// Token: 0x040006CD RID: 1741
		private readonly bool _IsParamRefSizeSmall;

		// Token: 0x040006CE RID: 1742
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x040006CF RID: 1743
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x040006D0 RID: 1744
		private readonly int _RvaOffset;

		// Token: 0x040006D1 RID: 1745
		private readonly int _ImplFlagsOffset;

		// Token: 0x040006D2 RID: 1746
		private readonly int _FlagsOffset;

		// Token: 0x040006D3 RID: 1747
		private readonly int _NameOffset;

		// Token: 0x040006D4 RID: 1748
		private readonly int _SignatureOffset;

		// Token: 0x040006D5 RID: 1749
		private readonly int _ParamListOffset;

		// Token: 0x040006D6 RID: 1750
		internal readonly int RowSize;

		// Token: 0x040006D7 RID: 1751
		internal readonly MemoryBlock Block;
	}
}
