using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000104 RID: 260
	internal struct AssemblyRefTableReader
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0001A4D4 File Offset: 0x000186D4
		internal AssemblyRefTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset, MetadataKind metadataKind)
		{
			this.NumberOfNonVirtualRows = numberOfRows;
			this.NumberOfVirtualRows = ((metadataKind == MetadataKind.Ecma335) ? 0 : 6);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._MajorVersionOffset = 0;
			this._MinorVersionOffset = this._MajorVersionOffset + 2;
			this._BuildNumberOffset = this._MinorVersionOffset + 2;
			this._RevisionNumberOffset = this._BuildNumberOffset + 2;
			this._FlagsOffset = this._RevisionNumberOffset + 2;
			this._PublicKeyOrTokenOffset = this._FlagsOffset + 4;
			this._NameOffset = this._PublicKeyOrTokenOffset + blobHeapRefSize;
			this._CultureOffset = this._NameOffset + stringHeapRefSize;
			this._HashValueOffset = this._CultureOffset + stringHeapRefSize;
			this.RowSize = this._HashValueOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001A5A8 File Offset: 0x000187A8
		internal Version GetVersion(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return new Version((int)this.Block.PeekUInt16(num + this._MajorVersionOffset), (int)this.Block.PeekUInt16(num + this._MinorVersionOffset), (int)this.Block.PeekUInt16(num + this._BuildNumberOffset), (int)this.Block.PeekUInt16(num + this._RevisionNumberOffset));
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001A620 File Offset: 0x00018820
		internal AssemblyFlags GetFlags(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (AssemblyFlags)this.Block.PeekUInt32(num + this._FlagsOffset);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001A650 File Offset: 0x00018850
		internal BlobHandle GetPublicKeyOrToken(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._PublicKeyOrTokenOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001A68C File Offset: 0x0001888C
		internal StringHandle GetName(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001A6C8 File Offset: 0x000188C8
		internal StringHandle GetCulture(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._CultureOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001A704 File Offset: 0x00018904
		internal BlobHandle GetHashValue(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._HashValueOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x0400079A RID: 1946
		internal readonly int NumberOfNonVirtualRows;

		// Token: 0x0400079B RID: 1947
		internal readonly int NumberOfVirtualRows;

		// Token: 0x0400079C RID: 1948
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x0400079D RID: 1949
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x0400079E RID: 1950
		private readonly int _MajorVersionOffset;

		// Token: 0x0400079F RID: 1951
		private readonly int _MinorVersionOffset;

		// Token: 0x040007A0 RID: 1952
		private readonly int _BuildNumberOffset;

		// Token: 0x040007A1 RID: 1953
		private readonly int _RevisionNumberOffset;

		// Token: 0x040007A2 RID: 1954
		private readonly int _FlagsOffset;

		// Token: 0x040007A3 RID: 1955
		private readonly int _PublicKeyOrTokenOffset;

		// Token: 0x040007A4 RID: 1956
		private readonly int _NameOffset;

		// Token: 0x040007A5 RID: 1957
		private readonly int _CultureOffset;

		// Token: 0x040007A6 RID: 1958
		private readonly int _HashValueOffset;

		// Token: 0x040007A7 RID: 1959
		internal readonly int RowSize;

		// Token: 0x040007A8 RID: 1960
		internal readonly MemoryBlock Block;
	}
}
