using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C4 RID: 2244
	[SuppressUnmanagedCodeSecurity]
	internal sealed class MsmqQueueHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060055B5 RID: 21941 RVA: 0x00139968 File Offset: 0x00137B68
		internal MsmqQueueHandle() : base(true)
		{
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x00139971 File Offset: 0x00137B71
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.MQCloseQueue(this.handle) >= 0;
		}
	}
}
