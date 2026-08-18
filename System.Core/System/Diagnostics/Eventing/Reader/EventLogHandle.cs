using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002BB RID: 699
	[SecuritySafeCritical]
	internal sealed class EventLogHandle : SafeHandle
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x0005CA9E File Offset: 0x0005AC9E
		private EventLogHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0005CAAC File Offset: 0x0005ACAC
		internal EventLogHandle(IntPtr handle, bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0005CAC1 File Offset: 0x0005ACC1
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0005CADD File Offset: 0x0005ACDD
		protected override bool ReleaseHandle()
		{
			NativeWrapper.EvtClose(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0005CAF6 File Offset: 0x0005ACF6
		public static EventLogHandle Zero
		{
			get
			{
				return new EventLogHandle();
			}
		}
	}
}
