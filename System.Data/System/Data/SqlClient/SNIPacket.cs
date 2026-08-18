using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000331 RID: 817
	internal sealed class SNIPacket : SafeHandle
	{
		// Token: 0x06002A87 RID: 10887 RVA: 0x002BEF98 File Offset: 0x002BE398
		internal SNIPacket(SafeHandle sniHandle) : base(IntPtr.Zero, true)
		{
			SNINativeMethodWrapper.SNIPacketAllocate(sniHandle, SNINativeMethodWrapper.IOType.WRITE, ref this.handle);
			if (IntPtr.Zero == this.handle)
			{
				throw SQL.SNIPacketAllocationFailure();
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x002BEFD8 File Offset: 0x002BE3D8
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x002BEFF8 File Offset: 0x002BE3F8
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SNINativeMethodWrapper.SNIPacketRelease(handle);
			}
			return true;
		}
	}
}
