using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A4F RID: 2639
	internal struct SocketAddressList
	{
		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06006846 RID: 26694 RVA: 0x00184E72 File Offset: 0x00183072
		public SocketAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06006847 RID: 26695 RVA: 0x00184E7A File Offset: 0x0018307A
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x00184E82 File Offset: 0x00183082
		public SocketAddressList(SocketAddress[] addresses, int count)
		{
			this.addresses = addresses;
			this.count = count;
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x00184E94 File Offset: 0x00183094
		public static ReadOnlyCollection<IPAddress> SortAddresses(Socket socket, IPAddress listenAddress, ReadOnlyCollection<IPAddress> addresses)
		{
			ReadOnlyCollection<IPAddress> result = null;
			if (socket == null || addresses.Count <= 1)
			{
				result = addresses;
			}
			else
			{
				CriticalAllocHandleSocketAddressList criticalAllocHandleSocketAddressList = null;
				CriticalAllocHandleSocketAddressList criticalAllocHandleSocketAddressList2 = null;
				try
				{
					criticalAllocHandleSocketAddressList = CriticalAllocHandleSocketAddressList.FromAddressList(addresses);
					criticalAllocHandleSocketAddressList2 = CriticalAllocHandleSocketAddressList.FromAddressCount(0);
					int num2;
					int num = PeerWinsock.WSAIoctl(socket.Handle, -939524071, criticalAllocHandleSocketAddressList, criticalAllocHandleSocketAddressList.Size, criticalAllocHandleSocketAddressList2, criticalAllocHandleSocketAddressList2.Size, out num2, IntPtr.Zero, IntPtr.Zero);
					if (num == -1)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SocketException(lastWin32Error));
					}
					result = criticalAllocHandleSocketAddressList2.ToAddresses();
				}
				finally
				{
					if (criticalAllocHandleSocketAddressList != null)
					{
						criticalAllocHandleSocketAddressList.Dispose();
					}
					if (criticalAllocHandleSocketAddressList2 != null)
					{
						criticalAllocHandleSocketAddressList2.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x04003BC7 RID: 15303
		private int count;

		// Token: 0x04003BC8 RID: 15304
		internal const int maxAddresses = 50;

		// Token: 0x04003BC9 RID: 15305
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private SocketAddress[] addresses;
	}
}
