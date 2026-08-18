using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000101 RID: 257
	internal struct AssemblyTableReader
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0001A248 File Offset: 0x00018448
		internal AssemblyTableReader(int numberOfRows, int stringHeapRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = ((numberOfRows > 1) ? 1 : numberOfRows);
			this._IsStringHeapRefSizeSmall = (stringHeapRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._HashAlgIdOffset = 0;
			this._MajorVersionOffset = this._HashAlgIdOffset + 4;
			this._MinorVersionOffset = this._MajorVersionOffset + 2;
			this._BuildNumberOffset = this._MinorVersionOffset + 2;
			this._RevisionNumberOffset = this._BuildNumberOffset + 2;
			this._FlagsOffset = this._RevisionNumberOffset + 2;
			this._PublicKeyOffset = this._FlagsOffset + 4;
			this._NameOffset = this._PublicKeyOffset + blobHeapRefSize;
			this._CultureOffset = this._NameOffset + stringHeapRefSize;
			this.RowSize = this._CultureOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001A314 File Offset: 0x00018514
		internal AssemblyHashAlgorithm GetHashAlgorithm()
		{
			return (AssemblyHashAlgorithm)this.Block.PeekUInt32(this._HashAlgIdOffset);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001A338 File Offset: 0x00018538
		internal Version GetVersion()
		{
			return new Version((int)this.Block.PeekUInt16(this._MajorVersionOffset), (int)this.Block.PeekUInt16(this._MinorVersionOffset), (int)this.Block.PeekUInt16(this._BuildNumberOffset), (int)this.Block.PeekUInt16(this._RevisionNumberOffset));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001A39C File Offset: 0x0001859C
		internal AssemblyFlags GetFlags()
		{
			return (AssemblyFlags)this.Block.PeekUInt32(this._FlagsOffset);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001A3C0 File Offset: 0x000185C0
		internal BlobHandle GetPublicKey()
		{
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(this._PublicKeyOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001A3EC File Offset: 0x000185EC
		internal StringHandle GetName()
		{
			return StringHandle.FromOffset(this.Block.PeekHeapReference(this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001A418 File Offset: 0x00018618
		internal StringHandle GetCulture()
		{
			return StringHandle.FromOffset(this.Block.PeekHeapReference(this._CultureOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x04000782 RID: 1922
		internal readonly int NumberOfRows;

		// Token: 0x04000783 RID: 1923
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000784 RID: 1924
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000785 RID: 1925
		private readonly int _HashAlgIdOffset;

		// Token: 0x04000786 RID: 1926
		private readonly int _MajorVersionOffset;

		// Token: 0x04000787 RID: 1927
		private readonly int _MinorVersionOffset;

		// Token: 0x04000788 RID: 1928
		private readonly int _BuildNumberOffset;

		// Token: 0x04000789 RID: 1929
		private readonly int _RevisionNumberOffset;

		// Token: 0x0400078A RID: 1930
		private readonly int _FlagsOffset;

		// Token: 0x0400078B RID: 1931
		private readonly int _PublicKeyOffset;

		// Token: 0x0400078C RID: 1932
		private readonly int _NameOffset;

		// Token: 0x0400078D RID: 1933
		private readonly int _CultureOffset;

		// Token: 0x0400078E RID: 1934
		internal readonly int RowSize;

		// Token: 0x0400078F RID: 1935
		internal readonly MemoryBlock Block;
	}
}
