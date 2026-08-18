using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001FF RID: 511
	internal class SafeNativeOverlapped : SafeHandle
	{
		// Token: 0x06001345 RID: 4933 RVA: 0x00065C90 File Offset: 0x00063E90
		internal SafeNativeOverlapped() : this(IntPtr.Zero)
		{
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00065C9D File Offset: 0x00063E9D
		internal unsafe SafeNativeOverlapped(NativeOverlapped* handle) : this((IntPtr)((void*)handle))
		{
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00065CAB File Offset: 0x00063EAB
		internal SafeNativeOverlapped(IntPtr handle) : base(IntPtr.Zero, true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00065CC0 File Offset: 0x00063EC0
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00065CD4 File Offset: 0x00063ED4
		public unsafe void ReinitializeNativeOverlapped()
		{
			IntPtr handle = this.handle;
			if (handle != IntPtr.Zero)
			{
				((NativeOverlapped*)((void*)handle))->InternalHigh = IntPtr.Zero;
				((NativeOverlapped*)((void*)handle))->InternalLow = IntPtr.Zero;
				((NativeOverlapped*)((void*)handle))->EventHandle = IntPtr.Zero;
			}
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00065D28 File Offset: 0x00063F28
		protected unsafe override bool ReleaseHandle()
		{
			IntPtr intPtr = Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			if (intPtr != IntPtr.Zero && !NclUtilities.HasShutdownStarted)
			{
				Overlapped.Free((NativeOverlapped*)((void*)intPtr));
			}
			return true;
		}

		// Token: 0x0400155A RID: 5466
		internal static readonly SafeNativeOverlapped Zero = new SafeNativeOverlapped();
	}
}
