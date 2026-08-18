using System;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x02000500 RID: 1280
	internal struct AddressInfo
	{
		// Token: 0x0400272D RID: 10029
		internal AddressInfoHints ai_flags;

		// Token: 0x0400272E RID: 10030
		internal AddressFamily ai_family;

		// Token: 0x0400272F RID: 10031
		internal SocketType ai_socktype;

		// Token: 0x04002730 RID: 10032
		internal ProtocolFamily ai_protocol;

		// Token: 0x04002731 RID: 10033
		internal int ai_addrlen;

		// Token: 0x04002732 RID: 10034
		internal unsafe sbyte* ai_canonname;

		// Token: 0x04002733 RID: 10035
		internal unsafe byte* ai_addr;

		// Token: 0x04002734 RID: 10036
		internal unsafe AddressInfo* ai_next;
	}
}
