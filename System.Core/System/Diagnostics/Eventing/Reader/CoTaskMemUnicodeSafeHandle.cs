using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002CC RID: 716
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class CoTaskMemUnicodeSafeHandle : SafeHandle
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x0006057C File Offset: 0x0005E77C
		internal CoTaskMemUnicodeSafeHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0006058A File Offset: 0x0005E78A
		internal CoTaskMemUnicodeSafeHandle(IntPtr handle, bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0006059F File Offset: 0x0005E79F
		internal void SetMemory(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000605A8 File Offset: 0x0005E7A8
		internal IntPtr GetMemory()
		{
			return this.handle;
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x000605B0 File Offset: 0x0005E7B0
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000605CC File Offset: 0x0005E7CC
		protected override bool ReleaseHandle()
		{
			Marshal.ZeroFreeCoTaskMemUnicode(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x000605E5 File Offset: 0x0005E7E5
		public static CoTaskMemUnicodeSafeHandle Zero
		{
			get
			{
				return new CoTaskMemUnicodeSafeHandle();
			}
		}
	}
}
