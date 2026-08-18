using System;
using System.Security;

namespace System.Reflection.Internal
{
	// Token: 0x0200007C RID: 124
	internal sealed class ExternalMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x06000316 RID: 790 RVA: 0x00007D7D File Offset: 0x00005F7D
		[SecurityCritical]
		public unsafe ExternalMemoryBlock(object memoryOwner, byte* buffer, int size)
		{
			this._memoryOwner = memoryOwner;
			this._buffer = buffer;
			this._size = size;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00007D9A File Offset: 0x00005F9A
		[SecuritySafeCritical]
		public override void Dispose()
		{
			this._buffer = null;
			this._size = 0;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00007DAB File Offset: 0x00005FAB
		public unsafe override byte* Pointer
		{
			[SecurityCritical]
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00007DB3 File Offset: 0x00005FB3
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0400047C RID: 1148
		private readonly object _memoryOwner;

		// Token: 0x0400047D RID: 1149
		[SecurityCritical]
		private unsafe byte* _buffer;

		// Token: 0x0400047E RID: 1150
		private int _size;
	}
}
