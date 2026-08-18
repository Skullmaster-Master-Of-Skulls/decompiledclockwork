using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007C7 RID: 1991
	internal class ServerVarCharBuffer
	{
		// Token: 0x06005F4D RID: 24397 RVA: 0x00148FCC File Offset: 0x001471CC
		internal ServerVarCharBuffer()
		{
			this._charBuffer = (char[])ServerVarCharBuffer.s_CharBufferAllocator.GetBuffer();
			this._recyclable = true;
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x00148FF0 File Offset: 0x001471F0
		internal void Dispose()
		{
			if (this._pinned)
			{
				this._pinnedCharBufferHandle.Free();
				this._pinned = false;
			}
			if (this._recyclable && this._charBuffer != null)
			{
				ServerVarCharBuffer.s_CharBufferAllocator.ReuseBuffer(this._charBuffer);
			}
			this._charBuffer = null;
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x06005F4F RID: 24399 RVA: 0x0014903E File Offset: 0x0014723E
		internal IntPtr PinnedAddress
		{
			get
			{
				if (!this._pinned)
				{
					this._pinnedCharBufferHandle = GCHandle.Alloc(this._charBuffer, GCHandleType.Pinned);
					this._pinnedAddr = Marshal.UnsafeAddrOfPinnedArrayElement(this._charBuffer, 0);
					this._pinned = true;
				}
				return this._pinnedAddr;
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x06005F50 RID: 24400 RVA: 0x00149079 File Offset: 0x00147279
		internal int Length
		{
			get
			{
				return this._charBuffer.Length;
			}
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x00149083 File Offset: 0x00147283
		internal void Resize(int newSize)
		{
			if (this._pinned)
			{
				this._pinnedCharBufferHandle.Free();
				this._pinned = false;
			}
			this._charBuffer = new char[newSize];
			this._recyclable = false;
		}

		// Token: 0x040031BA RID: 12730
		private const int BUFFER_SIZE = 1024;

		// Token: 0x040031BB RID: 12731
		private const int MAX_FREE_BUFFERS = 64;

		// Token: 0x040031BC RID: 12732
		private static CharBufferAllocator s_CharBufferAllocator = new CharBufferAllocator(1024, 64);

		// Token: 0x040031BD RID: 12733
		private bool _recyclable;

		// Token: 0x040031BE RID: 12734
		private char[] _charBuffer;

		// Token: 0x040031BF RID: 12735
		private bool _pinned;

		// Token: 0x040031C0 RID: 12736
		private GCHandle _pinnedCharBufferHandle;

		// Token: 0x040031C1 RID: 12737
		private IntPtr _pinnedAddr;
	}
}
