using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BC RID: 700
	internal struct IpAdapterAddress
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x0007E3CC File Offset: 0x0007C5CC
		internal static IPAddressCollection MarshalIpAddressCollection(IntPtr ptr)
		{
			IPAddressCollection ipaddressCollection = new IPAddressCollection();
			while (ptr != IntPtr.Zero)
			{
				IpAdapterAddress ipAdapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterAddress));
				IPAddress ipaddress = ipAdapterAddress.address.MarshalIPAddress();
				ipaddressCollection.InternalAdd(ipaddress);
				ptr = ipAdapterAddress.next;
			}
			return ipaddressCollection;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0007E424 File Offset: 0x0007C624
		internal static IPAddressInformationCollection MarshalIpAddressInformationCollection(IntPtr ptr)
		{
			IPAddressInformationCollection ipaddressInformationCollection = new IPAddressInformationCollection();
			while (ptr != IntPtr.Zero)
			{
				IpAdapterAddress ipAdapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterAddress));
				IPAddress ipaddress = ipAdapterAddress.address.MarshalIPAddress();
				ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(ipaddress, ipAdapterAddress.flags));
				ptr = ipAdapterAddress.next;
			}
			return ipaddressInformationCollection;
		}

		// Token: 0x04001952 RID: 6482
		internal uint length;

		// Token: 0x04001953 RID: 6483
		internal AdapterAddressFlags flags;

		// Token: 0x04001954 RID: 6484
		internal IntPtr next;

		// Token: 0x04001955 RID: 6485
		internal IpSocketAddress address;
	}
}
