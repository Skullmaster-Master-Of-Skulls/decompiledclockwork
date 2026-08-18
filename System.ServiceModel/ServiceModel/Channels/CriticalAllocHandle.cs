using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A53 RID: 2643
	internal class CriticalAllocHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600685B RID: 26715 RVA: 0x001853B0 File Offset: 0x001835B0
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static implicit operator IntPtr(CriticalAllocHandle safeHandle)
		{
			if (safeHandle == null)
			{
				return (IntPtr)null;
			}
			return safeHandle.handle;
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x001853C3 File Offset: 0x001835C3
		protected override bool ReleaseHandle()
		{
			Marshal.FreeHGlobal(this.handle);
			return true;
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x001853D4 File Offset: 0x001835D4
		public static CriticalAllocHandle FromSize(int size)
		{
			CriticalAllocHandle criticalAllocHandle = new CriticalAllocHandle();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				criticalAllocHandle.SetHandle(Marshal.AllocHGlobal(size));
			}
			return criticalAllocHandle;
		}
	}
}
