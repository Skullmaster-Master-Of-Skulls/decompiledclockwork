using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000219 RID: 537
	internal class RoutingInfo
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000EC524 File Offset: 0x000EB924
		// (set) Token: 0x060021F5 RID: 8693 RVA: 0x000EC538 File Offset: 0x000EB938
		internal byte Protocol { get; private set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000EC54C File Offset: 0x000EB94C
		// (set) Token: 0x060021F7 RID: 8695 RVA: 0x000EC560 File Offset: 0x000EB960
		internal ushort Port { get; private set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x000EC574 File Offset: 0x000EB974
		// (set) Token: 0x060021F9 RID: 8697 RVA: 0x000EC588 File Offset: 0x000EB988
		internal string ServerName { get; private set; }

		// Token: 0x060021FA RID: 8698 RVA: 0x000EC59C File Offset: 0x000EB99C
		internal RoutingInfo(byte protocol, ushort port, string servername)
		{
			this.Protocol = protocol;
			this.Port = port;
			this.ServerName = servername;
		}
	}
}
