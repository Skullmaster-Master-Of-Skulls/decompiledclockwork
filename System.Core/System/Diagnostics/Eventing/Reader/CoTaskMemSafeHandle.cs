using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002CD RID: 717
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class CoTaskMemSafeHandle : SafeHandle
	{
		// Token: 0x06001A2B RID: 6699 RVA: 0x000605EC File Offset: 0x0005E7EC
		internal CoTaskMemSafeHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000605FA File Offset: 0x0005E7FA
		internal void SetMemory(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00060603 File Offset: 0x0005E803
		internal IntPtr GetMemory()
		{
			return this.handle;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x0006060B File Offset: 0x0005E80B
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00060627 File Offset: 0x0005E827
		protected override bool ReleaseHandle()
		{
			Marshal.FreeCoTaskMem(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00060640 File Offset: 0x0005E840
		public static CoTaskMemSafeHandle Zero
		{
			get
			{
				return new CoTaskMemSafeHandle();
			}
		}
	}
}
