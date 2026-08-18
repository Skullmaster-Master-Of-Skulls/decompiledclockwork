using System;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x020001D7 RID: 471
	internal struct AddressInfo
	{
		// Token: 0x040014EB RID: 5355
		internal AddressInfoHints ai_flags;

		// Token: 0x040014EC RID: 5356
		internal AddressFamily ai_family;

		// Token: 0x040014ED RID: 5357
		internal SocketType ai_socktype;

		// Token: 0x040014EE RID: 5358
		internal ProtocolFamily ai_protocol;

		// Token: 0x040014EF RID: 5359
		internal int ai_addrlen;

		// Token: 0x040014F0 RID: 5360
		internal unsafe sbyte* ai_canonname;

		// Token: 0x040014F1 RID: 5361
		internal unsafe byte* ai_addr;

		// Token: 0x040014F2 RID: 5362
		internal unsafe AddressInfo* ai_next;
	}
}
