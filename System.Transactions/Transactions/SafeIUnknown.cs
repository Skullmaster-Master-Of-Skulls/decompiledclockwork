using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Transactions
{
	// Token: 0x02000075 RID: 117
	internal sealed class SafeIUnknown : SafeHandle
	{
		// Token: 0x06000345 RID: 837 RVA: 0x00036FD4 File Offset: 0x000363D4
		internal SafeIUnknown() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00036FF4 File Offset: 0x000363F4
		internal SafeIUnknown(IntPtr unknown) : base(IntPtr.Zero, true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.handle = unknown;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00037044 File Offset: 0x00036444
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00037074 File Offset: 0x00036474
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				Marshal.Release(handle);
			}
			return true;
		}
	}
}
