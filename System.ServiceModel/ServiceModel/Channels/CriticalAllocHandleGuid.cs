using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A55 RID: 2645
	internal class CriticalAllocHandleGuid : CriticalAllocHandle
	{
		// Token: 0x06006861 RID: 26721 RVA: 0x00185454 File Offset: 0x00183654
		public static CriticalAllocHandle FromGuid(Guid input)
		{
			int num = Marshal.SizeOf(typeof(Guid));
			CriticalAllocHandle criticalAllocHandle = CriticalAllocHandle.FromSize(num);
			Marshal.Copy(input.ToByteArray(), 0, criticalAllocHandle, num);
			return criticalAllocHandle;
		}
	}
}
