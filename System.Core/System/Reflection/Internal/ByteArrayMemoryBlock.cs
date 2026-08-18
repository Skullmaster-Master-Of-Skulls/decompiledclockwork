using System;
using System.Security;

namespace System.Reflection.Internal
{
	// Token: 0x0200007A RID: 122
	internal sealed class ByteArrayMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x0600030B RID: 779 RVA: 0x00007C7A File Offset: 0x00005E7A
		internal ByteArrayMemoryBlock(ByteArrayMemoryProvider provider, int start, int size)
		{
			this._provider = provider;
			this._size = size;
			this._start = start;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00007C97 File Offset: 0x00005E97
		public override void Dispose()
		{
			this._provider = null;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public unsafe override byte* Pointer
		{
			[SecuritySafeCritical]
			get
			{
				return this._provider.Pointer + this._start;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04000477 RID: 1143
		private ByteArrayMemoryProvider _provider;

		// Token: 0x04000478 RID: 1144
		private readonly int _start;

		// Token: 0x04000479 RID: 1145
		private readonly int _size;
	}
}
