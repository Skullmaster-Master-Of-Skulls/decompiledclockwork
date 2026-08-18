using System;
using System.ComponentModel;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C0 RID: 2240
	[SuppressUnmanagedCodeSecurity]
	internal class PipeHandle : SafeHandleMinusOneIsInvalid
	{
		// Token: 0x060055A9 RID: 21929 RVA: 0x00139898 File Offset: 0x00137A98
		internal PipeHandle() : base(true)
		{
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x001398A1 File Offset: 0x00137AA1
		internal PipeHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x001398B4 File Offset: 0x00137AB4
		internal int GetClientPid()
		{
			int result;
			if (!UnsafeNativeMethods.GetNamedPipeClientProcessId(this, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception());
			}
			return result;
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x001398DE File Offset: 0x00137ADE
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.CloseHandle(this.handle) != 0;
		}
	}
}
