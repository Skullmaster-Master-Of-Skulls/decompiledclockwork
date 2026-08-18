using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005F4 RID: 1524
	internal struct IpAddrString
	{
		// Token: 0x06002FD7 RID: 12247 RVA: 0x000CF374 File Offset: 0x000CE374
		internal IPAddressCollection ToIPAddressCollection()
		{
			IpAddrString ipAddrString = this;
			IPAddressCollection ipaddressCollection = new IPAddressCollection();
			if (ipAddrString.IpAddress.Length != 0)
			{
				ipaddressCollection.InternalAdd(IPAddress.Parse(ipAddrString.IpAddress));
			}
			while (ipAddrString.Next != IntPtr.Zero)
			{
				ipAddrString = (IpAddrString)Marshal.PtrToStructure(ipAddrString.Next, typeof(IpAddrString));
				if (ipAddrString.IpAddress.Length != 0)
				{
					ipaddressCollection.InternalAdd(IPAddress.Parse(ipAddrString.IpAddress));
				}
			}
			return ipaddressCollection;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000CF400 File Offset: 0x000CE400
		internal ArrayList ToIPExtendedAddressArrayList()
		{
			IpAddrString ipAddrString = this;
			ArrayList arrayList = new ArrayList();
			if (ipAddrString.IpAddress.Length != 0)
			{
				arrayList.Add(new IPExtendedAddress(IPAddress.Parse(ipAddrString.IpAddress), IPAddress.Parse(ipAddrString.IpMask)));
			}
			while (ipAddrString.Next != IntPtr.Zero)
			{
				ipAddrString = (IpAddrString)Marshal.PtrToStructure(ipAddrString.Next, typeof(IpAddrString));
				if (ipAddrString.IpAddress.Length != 0)
				{
					arrayList.Add(new IPExtendedAddress(IPAddress.Parse(ipAddrString.IpAddress), IPAddress.Parse(ipAddrString.IpMask)));
				}
			}
			return arrayList;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000CF4BC File Offset: 0x000CE4BC
		internal GatewayIPAddressInformationCollection ToIPGatewayAddressCollection()
		{
			IpAddrString ipAddrString = this;
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			if (ipAddrString.IpAddress.Length != 0)
			{
				gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(IPAddress.Parse(ipAddrString.IpAddress)));
			}
			while (ipAddrString.Next != IntPtr.Zero)
			{
				ipAddrString = (IpAddrString)Marshal.PtrToStructure(ipAddrString.Next, typeof(IpAddrString));
				if (ipAddrString.IpAddress.Length != 0)
				{
					gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(IPAddress.Parse(ipAddrString.IpAddress)));
				}
			}
			return gatewayIPAddressInformationCollection;
		}

		// Token: 0x04002CF4 RID: 11508
		internal IntPtr Next;

		// Token: 0x04002CF5 RID: 11509
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string IpAddress;

		// Token: 0x04002CF6 RID: 11510
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string IpMask;

		// Token: 0x04002CF7 RID: 11511
		internal uint Context;
	}
}
