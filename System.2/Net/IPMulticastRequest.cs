using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D2 RID: 466
	internal struct IPMulticastRequest
	{
		// Token: 0x040014D9 RID: 5337
		internal int MulticastAddress;

		// Token: 0x040014DA RID: 5338
		internal int InterfaceAddress;

		// Token: 0x040014DB RID: 5339
		internal static readonly int Size = Marshal.SizeOf(typeof(IPMulticastRequest));
	}
}
