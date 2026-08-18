using System;

namespace System.Net
{
	// Token: 0x020004C6 RID: 1222
	internal struct TunnelStateObject
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x0009640C File Offset: 0x0009540C
		internal TunnelStateObject(HttpWebRequest r, Connection c)
		{
			this.Connection = c;
			this.OriginalRequest = r;
		}

		// Token: 0x0400257D RID: 9597
		internal Connection Connection;

		// Token: 0x0400257E RID: 9598
		internal HttpWebRequest OriginalRequest;
	}
}
