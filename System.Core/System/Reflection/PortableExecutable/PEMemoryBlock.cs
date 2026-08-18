using System;
using System.Reflection.Internal;
using System.Reflection.Metadata;
using System.Security;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200004C RID: 76
	internal struct PEMemoryBlock
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00004E45 File Offset: 0x00003045
		internal PEMemoryBlock(AbstractMemoryBlock block, int offset = 0)
		{
			this._block = block;
			this._offset = offset;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00004E55 File Offset: 0x00003055
		public unsafe byte* Pointer
		{
			[SecurityCritical]
			get
			{
				if (this._block == null)
				{
					return null;
				}
				return this._block.Pointer + this._offset;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00004E74 File Offset: 0x00003074
		public int Length
		{
			get
			{
				AbstractMemoryBlock block = this._block;
				return (((block != null) ? new int?(block.Size) : null) - this._offset).GetValueOrDefault();
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00004ED2 File Offset: 0x000030D2
		[SecuritySafeCritical]
		public BlobReader GetReader()
		{
			return new BlobReader(this.Pointer, this.Length);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00004EE5 File Offset: 0x000030E5
		[SecuritySafeCritical]
		public BlobReader GetReader(int start, int length)
		{
			BlobUtilities.ValidateRange(this.Length, start, length, "length");
			return new BlobReader(this.Pointer + start, length);
		}

		// Token: 0x040002DB RID: 731
		private readonly AbstractMemoryBlock _block;

		// Token: 0x040002DC RID: 732
		private readonly int _offset;
	}
}
