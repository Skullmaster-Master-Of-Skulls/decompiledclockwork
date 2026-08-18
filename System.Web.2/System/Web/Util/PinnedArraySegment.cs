using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x020001D5 RID: 469
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal sealed class PinnedArraySegment<T> : IDisposable
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x00049A78 File Offset: 0x00047C78
		internal PinnedArraySegment(ArraySegment<T> segment)
		{
			segment = new ArraySegment<T>(segment.Array, segment.Offset, segment.Count);
			this._gcHandle = GCHandle.Alloc(segment.Array, GCHandleType.Pinned);
			this._pointer = Marshal.UnsafeAddrOfPinnedArrayElement(segment.Array, segment.Offset);
			this._count = segment.Count;
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00049AE0 File Offset: 0x00047CE0
		public int Count
		{
			get
			{
				this.ThrowIfDisposed();
				return this._count;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x00049AEE File Offset: 0x00047CEE
		public IntPtr Pointer
		{
			get
			{
				this.ThrowIfDisposed();
				return this._pointer;
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00049AFC File Offset: 0x00047CFC
		public void Dispose()
		{
			if (this._pointer != IntPtr.Zero)
			{
				this._pointer = IntPtr.Zero;
				this._gcHandle.Free();
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00049B26 File Offset: 0x00047D26
		private void ThrowIfDisposed()
		{
			if (this._pointer == IntPtr.Zero)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04001715 RID: 5909
		private int _count;

		// Token: 0x04001716 RID: 5910
		private GCHandle _gcHandle;

		// Token: 0x04001717 RID: 5911
		private IntPtr _pointer;
	}
}
