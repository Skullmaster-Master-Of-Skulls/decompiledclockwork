using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A54 RID: 2644
	internal class CriticalAllocHandleBlob : CriticalAllocHandle
	{
		// Token: 0x0600685F RID: 26719 RVA: 0x00185414 File Offset: 0x00183614
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public static CriticalAllocHandle FromBlob<T>(T id)
		{
			int size = Marshal.SizeOf(typeof(T));
			CriticalAllocHandle criticalAllocHandle = CriticalAllocHandle.FromSize(size);
			Marshal.StructureToPtr(id, criticalAllocHandle, false);
			return criticalAllocHandle;
		}
	}
}
