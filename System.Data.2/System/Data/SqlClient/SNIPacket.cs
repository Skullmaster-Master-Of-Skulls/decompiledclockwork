using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200022A RID: 554
	internal sealed class SNIPacket : SafeHandle
	{
		// Token: 0x06002245 RID: 8773 RVA: 0x000ED520 File Offset: 0x000EC920
		internal SNIPacket(SafeHandle sniHandle) : base(IntPtr.Zero, true)
		{
			SNINativeMethodWrapper.SNIPacketAllocate(sniHandle, SNINativeMethodWrapper.IOType.WRITE, ref this.handle);
			if (IntPtr.Zero == this.handle)
			{
				throw SQL.SNIPacketAllocationFailure();
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x000ED560 File Offset: 0x000EC960
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000ED580 File Offset: 0x000EC980
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
