using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020002A4 RID: 676
	internal class ServerVarCharBuffer
	{
		// Token: 0x06002323 RID: 8995 RVA: 0x000972F0 File Offset: 0x000962F0
		internal ServerVarCharBuffer()
		{
			this._charBuffer = (char[])ServerVarCharBuffer.s_CharBufferAllocator.GetBuffer();
			this._recyclable = true;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00097314 File Offset: 0x00096314
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

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x00097362 File Offset: 0x00096362
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

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x0009739D File Offset: 0x0009639D
		internal int Length
		{
			get
			{
				return this._charBuffer.Length;
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000973A7 File Offset: 0x000963A7
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

		// Token: 0x04001BA2 RID: 7074
		private const int BUFFER_SIZE = 1024;

		// Token: 0x04001BA3 RID: 7075
		private const int MAX_FREE_BUFFERS = 64;

		// Token: 0x04001BA4 RID: 7076
		private static CharBufferAllocator s_CharBufferAllocator = new CharBufferAllocator(1024, 64);

		// Token: 0x04001BA5 RID: 7077
		private bool _recyclable;

		// Token: 0x04001BA6 RID: 7078
		private char[] _charBuffer;

		// Token: 0x04001BA7 RID: 7079
		private bool _pinned;

		// Token: 0x04001BA8 RID: 7080
		private GCHandle _pinnedCharBufferHandle;

		// Token: 0x04001BA9 RID: 7081
		private IntPtr _pinnedAddr;
	}
}
