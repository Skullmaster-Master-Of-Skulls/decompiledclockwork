using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A52 RID: 2642
	internal class CriticalAllocHandleSocketAddress : CriticalAllocHandle
	{
		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06006857 RID: 26711 RVA: 0x0018530C File Offset: 0x0018350C
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x00185314 File Offset: 0x00183514
		public static CriticalAllocHandleSocketAddress FromIPAddress(IPAddress input)
		{
			if (input == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("input");
			}
			int num = Marshal.SizeOf(typeof(sockaddr_in6));
			CriticalAllocHandleSocketAddress criticalAllocHandleSocketAddress = CriticalAllocHandleSocketAddress.FromSize(num);
			sockaddr_in6 sockaddr_in = new sockaddr_in6(input);
			Marshal.StructureToPtr(sockaddr_in, criticalAllocHandleSocketAddress, false);
			return criticalAllocHandleSocketAddress;
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x00185368 File Offset: 0x00183568
		public new static CriticalAllocHandleSocketAddress FromSize(int size)
		{
			CriticalAllocHandleSocketAddress criticalAllocHandleSocketAddress = new CriticalAllocHandleSocketAddress();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				criticalAllocHandleSocketAddress.SetHandle(Marshal.AllocHGlobal(size));
				criticalAllocHandleSocketAddress.size = size;
			}
			return criticalAllocHandleSocketAddress;
		}

		// Token: 0x04003BD5 RID: 15317
		private int size;
	}
}
