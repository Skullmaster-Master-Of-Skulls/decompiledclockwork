using System;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000E4 RID: 228
	internal sealed class PerfInstanceDataHandle : SafeHandle
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x000287BC File Offset: 0x000269BC
		internal PerfInstanceDataHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000287CA File Offset: 0x000269CA
		internal IntPtr UnsafeHandle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x000287D2 File Offset: 0x000269D2
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000287E4 File Offset: 0x000269E4
		protected override bool ReleaseHandle()
		{
			UnsafeNativeMethods.PerfCloseAppCounters(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
