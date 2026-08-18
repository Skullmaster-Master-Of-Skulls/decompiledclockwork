using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BB RID: 699
	internal struct IpSocketAddress
	{
		// Token: 0x060019C0 RID: 6592 RVA: 0x0007E384 File Offset: 0x0007C584
		internal IPAddress MarshalIPAddress()
		{
			AddressFamily family = (this.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;
			SocketAddress socketAddress = new SocketAddress(family, this.addressLength);
			Marshal.Copy(this.address, socketAddress.m_Buffer, 0, this.addressLength);
			return socketAddress.GetIPAddress();
		}

		// Token: 0x04001950 RID: 6480
		internal IntPtr address;

		// Token: 0x04001951 RID: 6481
		internal int addressLength;
	}
}
