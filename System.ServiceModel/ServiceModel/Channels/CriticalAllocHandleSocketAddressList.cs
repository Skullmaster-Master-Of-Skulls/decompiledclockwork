using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A51 RID: 2641
	internal class CriticalAllocHandleSocketAddressList : CriticalAllocHandle
	{
		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x001850DE File Offset: 0x001832DE
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x06006851 RID: 26705 RVA: 0x001850E6 File Offset: 0x001832E6
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x001850F0 File Offset: 0x001832F0
		public static CriticalAllocHandleSocketAddressList FromAddressList(ICollection<IPAddress> addresses)
		{
			if (addresses == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addresses");
			}
			int num = addresses.Count;
			CriticalAllocHandleSocketAddress[] array = new CriticalAllocHandleSocketAddress[50];
			SocketAddressList socketAddressList = new SocketAddressList(new SocketAddress[50], num);
			int num2 = 0;
			foreach (IPAddress input in addresses)
			{
				if (num2 == 50)
				{
					break;
				}
				array[num2] = CriticalAllocHandleSocketAddress.FromIPAddress(input);
				socketAddressList.Addresses[num2].InitializeFromCriticalAllocHandleSocketAddress(array[num2]);
				num2++;
			}
			int num3 = Marshal.SizeOf(socketAddressList);
			CriticalAllocHandleSocketAddressList criticalAllocHandleSocketAddressList = CriticalAllocHandleSocketAddressList.FromSize(num3);
			criticalAllocHandleSocketAddressList.count = num;
			criticalAllocHandleSocketAddressList.socketHandles = array;
			Marshal.StructureToPtr(socketAddressList, criticalAllocHandleSocketAddressList, false);
			return criticalAllocHandleSocketAddressList;
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x001851D0 File Offset: 0x001833D0
		public static CriticalAllocHandleSocketAddressList FromAddressCount(int count)
		{
			SocketAddressList socketAddressList = new SocketAddressList(new SocketAddress[50], 0);
			int num = Marshal.SizeOf(socketAddressList);
			CriticalAllocHandleSocketAddressList criticalAllocHandleSocketAddressList = CriticalAllocHandleSocketAddressList.FromSize(num);
			criticalAllocHandleSocketAddressList.count = count;
			Marshal.StructureToPtr(socketAddressList, criticalAllocHandleSocketAddressList, false);
			return criticalAllocHandleSocketAddressList;
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x0018521C File Offset: 0x0018341C
		private new static CriticalAllocHandleSocketAddressList FromSize(int size)
		{
			CriticalAllocHandleSocketAddressList criticalAllocHandleSocketAddressList = new CriticalAllocHandleSocketAddressList();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				criticalAllocHandleSocketAddressList.SetHandle(Marshal.AllocHGlobal(size));
				criticalAllocHandleSocketAddressList.size = size;
			}
			return criticalAllocHandleSocketAddressList;
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x0018525C File Offset: 0x0018345C
		public ReadOnlyCollection<IPAddress> ToAddresses()
		{
			SocketAddressList socketAddressList = (SocketAddressList)Marshal.PtrToStructure(this, typeof(SocketAddressList));
			IPAddress[] array = new IPAddress[socketAddressList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				if (socketAddressList.Addresses[i].SockAddrLength != Marshal.SizeOf(typeof(sockaddr_in6)))
				{
					throw Fx.AssertAndThrow("sockAddressLength in SOCKET_ADDRESS expected to be valid");
				}
				array[i] = ((sockaddr_in6)Marshal.PtrToStructure(socketAddressList.Addresses[i].SockAddr, typeof(sockaddr_in6))).ToIPAddress();
			}
			return Array.AsReadOnly<IPAddress>(array);
		}

		// Token: 0x04003BD2 RID: 15314
		private int count;

		// Token: 0x04003BD3 RID: 15315
		private int size;

		// Token: 0x04003BD4 RID: 15316
		private CriticalAllocHandleSocketAddress[] socketHandles;
	}
}
