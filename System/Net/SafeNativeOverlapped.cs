using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000527 RID: 1319
	internal class SafeNativeOverlapped : SafeHandle
	{
		// Token: 0x06002875 RID: 10357 RVA: 0x000A77E7 File Offset: 0x000A67E7
		internal SafeNativeOverlapped() : this(IntPtr.Zero)
		{
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000A77F4 File Offset: 0x000A67F4
		internal unsafe SafeNativeOverlapped(NativeOverlapped* handle) : this((IntPtr)((void*)handle))
		{
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000A7802 File Offset: 0x000A6802
		internal SafeNativeOverlapped(IntPtr handle) : base(IntPtr.Zero, true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x000A7817 File Offset: 0x000A6817
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000A782C File Offset: 0x000A682C
		protected unsafe override bool ReleaseHandle()
		{
			IntPtr intPtr = Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			if (intPtr != IntPtr.Zero && !NclUtilities.HasShutdownStarted)
			{
				Overlapped.Free((NativeOverlapped*)((void*)intPtr));
			}
			return true;
		}

		// Token: 0x04002790 RID: 10128
		internal static readonly SafeNativeOverlapped Zero = new SafeNativeOverlapped();
	}
}
