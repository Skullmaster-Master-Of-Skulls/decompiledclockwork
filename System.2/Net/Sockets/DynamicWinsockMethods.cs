using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x02000390 RID: 912
	internal sealed class DynamicWinsockMethods
	{
		// Token: 0x06002241 RID: 8769 RVA: 0x000A3D80 File Offset: 0x000A1F80
		public static DynamicWinsockMethods GetMethods(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			List<DynamicWinsockMethods> obj = DynamicWinsockMethods.s_MethodTable;
			DynamicWinsockMethods result;
			lock (obj)
			{
				DynamicWinsockMethods dynamicWinsockMethods;
				for (int i = 0; i < DynamicWinsockMethods.s_MethodTable.Count; i++)
				{
					dynamicWinsockMethods = DynamicWinsockMethods.s_MethodTable[i];
					if (dynamicWinsockMethods.addressFamily == addressFamily && dynamicWinsockMethods.socketType == socketType && dynamicWinsockMethods.protocolType == protocolType)
					{
						return dynamicWinsockMethods;
					}
				}
				dynamicWinsockMethods = new DynamicWinsockMethods(addressFamily, socketType, protocolType);
				DynamicWinsockMethods.s_MethodTable.Add(dynamicWinsockMethods);
				result = dynamicWinsockMethods;
			}
			return result;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000A3E14 File Offset: 0x000A2014
		private DynamicWinsockMethods(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			this.addressFamily = addressFamily;
			this.socketType = socketType;
			this.protocolType = protocolType;
			this.lockObject = new object();
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000A3E3C File Offset: 0x000A203C
		public T GetDelegate<T>(SafeCloseSocket socketHandle) where T : class
		{
			if (typeof(T) == typeof(AcceptExDelegate))
			{
				this.EnsureAcceptEx(socketHandle);
				return (T)((object)this.acceptEx);
			}
			if (typeof(T) == typeof(GetAcceptExSockaddrsDelegate))
			{
				this.EnsureGetAcceptExSockaddrs(socketHandle);
				return (T)((object)this.getAcceptExSockaddrs);
			}
			if (typeof(T) == typeof(ConnectExDelegate))
			{
				this.EnsureConnectEx(socketHandle);
				return (T)((object)this.connectEx);
			}
			if (typeof(T) == typeof(DisconnectExDelegate))
			{
				this.EnsureDisconnectEx(socketHandle);
				return (T)((object)this.disconnectEx);
			}
			if (typeof(T) == typeof(DisconnectExDelegate_Blocking))
			{
				this.EnsureDisconnectEx(socketHandle);
				return (T)((object)this.disconnectEx_Blocking);
			}
			if (typeof(T) == typeof(WSARecvMsgDelegate))
			{
				this.EnsureWSARecvMsg(socketHandle);
				return (T)((object)this.recvMsg);
			}
			if (typeof(T) == typeof(WSARecvMsgDelegate_Blocking))
			{
				this.EnsureWSARecvMsg(socketHandle);
				return (T)((object)this.recvMsg_Blocking);
			}
			if (typeof(T) == typeof(TransmitPacketsDelegate))
			{
				this.EnsureTransmitPackets(socketHandle);
				return (T)((object)this.transmitPackets);
			}
			return default(T);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000A3FC4 File Offset: 0x000A21C4
		private IntPtr LoadDynamicFunctionPointer(SafeCloseSocket socketHandle, ref Guid guid)
		{
			IntPtr zero = IntPtr.Zero;
			int num;
			SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl(socketHandle, -939524090, ref guid, sizeof(Guid), out zero, sizeof(IntPtr), out num, IntPtr.Zero, IntPtr.Zero);
			if (socketError != SocketError.Success)
			{
				throw new SocketException();
			}
			return zero;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000A4008 File Offset: 0x000A2208
		private void EnsureAcceptEx(SafeCloseSocket socketHandle)
		{
			if (this.acceptEx == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.acceptEx == null)
					{
						Guid guid = new Guid("{0xb5367df1,0xcbac,0x11cf,{0x95, 0xca, 0x00, 0x80, 0x5f, 0x48, 0xa1, 0x92}}");
						IntPtr ptr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.acceptEx = (AcceptExDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(AcceptExDelegate));
					}
				}
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000A4084 File Offset: 0x000A2284
		private void EnsureGetAcceptExSockaddrs(SafeCloseSocket socketHandle)
		{
			if (this.getAcceptExSockaddrs == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.getAcceptExSockaddrs == null)
					{
						Guid guid = new Guid("{0xb5367df2,0xcbac,0x11cf,{0x95, 0xca, 0x00, 0x80, 0x5f, 0x48, 0xa1, 0x92}}");
						IntPtr ptr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.getAcceptExSockaddrs = (GetAcceptExSockaddrsDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(GetAcceptExSockaddrsDelegate));
					}
				}
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000A4100 File Offset: 0x000A2300
		private void EnsureConnectEx(SafeCloseSocket socketHandle)
		{
			if (this.connectEx == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.connectEx == null)
					{
						Guid guid = new Guid("{0x25a207b9,0x0ddf3,0x4660,{0x8e,0xe9,0x76,0xe5,0x8c,0x74,0x06,0x3e}}");
						IntPtr ptr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.connectEx = (ConnectExDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(ConnectExDelegate));
					}
				}
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000A417C File Offset: 0x000A237C
		private void EnsureDisconnectEx(SafeCloseSocket socketHandle)
		{
			if (this.disconnectEx == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.disconnectEx == null)
					{
						Guid guid = new Guid("{0x7fda2e11,0x8630,0x436f,{0xa0, 0x31, 0xf5, 0x36, 0xa6, 0xee, 0xc1, 0x57}}");
						IntPtr ptr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.disconnectEx = (DisconnectExDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(DisconnectExDelegate));
						this.disconnectEx_Blocking = (DisconnectExDelegate_Blocking)Marshal.GetDelegateForFunctionPointer(ptr, typeof(DisconnectExDelegate_Blocking));
					}
				}
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000A4214 File Offset: 0x000A2414
		private void EnsureWSARecvMsg(SafeCloseSocket socketHandle)
		{
			if (this.recvMsg == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.recvMsg == null)
					{
						Guid guid = new Guid("{0xf689d7c8,0x6f1f,0x436b,{0x8a,0x53,0xe5,0x4f,0xe3,0x51,0xc3,0x22}}");
						IntPtr ptr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.recvMsg = (WSARecvMsgDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(WSARecvMsgDelegate));
						this.recvMsg_Blocking = (WSARecvMsgDelegate_Blocking)Marshal.GetDelegateForFunctionPointer(ptr, typeof(WSARecvMsgDelegate_Blocking));
					}
				}
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000A42AC File Offset: 0x000A24AC
		private void EnsureTransmitPackets(SafeCloseSocket socketHandle)
		{
			if (this.transmitPackets == null)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.transmitPackets == null)
					{
						Guid guid = new Guid("{0xd9689da0,0x1f90,0x11d3,{0x99,0x71,0x00,0xc0,0x4f,0x68,0xc8,0x76}}");
						IntPtr ptr = this.LoadDynamicFunctionPointer(socketHandle, ref guid);
						this.transmitPackets = (TransmitPacketsDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TransmitPacketsDelegate));
					}
				}
			}
		}

		// Token: 0x04001F70 RID: 8048
		private static List<DynamicWinsockMethods> s_MethodTable = new List<DynamicWinsockMethods>();

		// Token: 0x04001F71 RID: 8049
		private AddressFamily addressFamily;

		// Token: 0x04001F72 RID: 8050
		private SocketType socketType;

		// Token: 0x04001F73 RID: 8051
		private ProtocolType protocolType;

		// Token: 0x04001F74 RID: 8052
		private object lockObject;

		// Token: 0x04001F75 RID: 8053
		private AcceptExDelegate acceptEx;

		// Token: 0x04001F76 RID: 8054
		private GetAcceptExSockaddrsDelegate getAcceptExSockaddrs;

		// Token: 0x04001F77 RID: 8055
		private ConnectExDelegate connectEx;

		// Token: 0x04001F78 RID: 8056
		private TransmitPacketsDelegate transmitPackets;

		// Token: 0x04001F79 RID: 8057
		private DisconnectExDelegate disconnectEx;

		// Token: 0x04001F7A RID: 8058
		private DisconnectExDelegate_Blocking disconnectEx_Blocking;

		// Token: 0x04001F7B RID: 8059
		private WSARecvMsgDelegate recvMsg;

		// Token: 0x04001F7C RID: 8060
		private WSARecvMsgDelegate_Blocking recvMsg_Blocking;
	}
}
