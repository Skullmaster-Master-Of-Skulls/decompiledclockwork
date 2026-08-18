using System;

namespace System.Net
{
	// Token: 0x020001A1 RID: 417
	internal struct TunnelStateObject
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x0005396F File Offset: 0x00051B6F
		internal TunnelStateObject(HttpWebRequest r, Connection c)
		{
			this.Connection = c;
			this.OriginalRequest = r;
		}

		// Token: 0x04001336 RID: 4918
		internal Connection Connection;

		// Token: 0x04001337 RID: 4919
		internal HttpWebRequest OriginalRequest;
	}
}
