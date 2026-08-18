using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020004FB RID: 1275
	internal struct IPMulticastRequest
	{
		// Token: 0x0400271B RID: 10011
		internal int MulticastAddress;

		// Token: 0x0400271C RID: 10012
		internal int InterfaceAddress;

		// Token: 0x0400271D RID: 10013
		internal static readonly int Size = Marshal.SizeOf(typeof(IPMulticastRequest));
	}
}
