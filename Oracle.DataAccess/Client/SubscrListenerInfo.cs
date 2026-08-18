using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200007E RID: 126
	[StructLayout(LayoutKind.Sequential)]
	internal class SubscrListenerInfo
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0003E80D File Offset: 0x0003D80D
		internal SubscrListenerInfo()
		{
			this.port = OraTrace.m_DBNotificationPort;
			this.bListenerStart = false;
		}

		// Token: 0x040003A6 RID: 934
		internal int port;

		// Token: 0x040003A7 RID: 935
		internal bool bListenerStart;
	}
}
