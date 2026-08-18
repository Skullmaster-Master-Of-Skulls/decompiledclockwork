using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005CD RID: 1485
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeServiceHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060039BA RID: 14778 RVA: 0x000DEBFA File Offset: 0x000DCDFA
		internal SafeServiceHandle() : base(true)
		{
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000DEC03 File Offset: 0x000DCE03
		protected override bool ReleaseHandle()
		{
			return ListenerUnsafeNativeMethods.CloseServiceHandle(this.handle);
		}
	}
}
