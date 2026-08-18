using System;
using System.Collections.Immutable;
using System.Reflection.Internal;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000028 RID: 40
	public struct PEMemoryBlock
	{
		// Token: 0x0600023E RID: 574 RVA: 0x00006A31 File Offset: 0x00004C31
		internal PEMemoryBlock(AbstractMemoryBlock block, int offset = 0)
		{
			this._block = block;
			this._offset = offset;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00006A41 File Offset: 0x00004C41
		public unsafe byte* Pointer
		{
			get
			{
				if (this._block == null)
				{
					return null;
				}
				return this._block.Pointer + this._offset;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00006A60 File Offset: 0x00004C60
		public int Length
		{
			get
			{
				if (this._block == null)
				{
					return 0;
				}
				return this._block.Size - this._offset;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006A7E File Offset: 0x00004C7E
		public ImmutableArray<byte> GetContent()
		{
			if (this._block == null)
			{
				return ImmutableArray<byte>.Empty;
			}
			return this._block.GetContent(this._offset);
		}

		// Token: 0x04000172 RID: 370
		private readonly AbstractMemoryBlock _block;

		// Token: 0x04000173 RID: 371
		private readonly int _offset;
	}
}
