using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000200 RID: 512
	[SuppressUnmanagedCodeSecurity]
	internal class SafeCloseSocket : SafeHandleMinusOneIsInvalid
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x00065D72 File Offset: 0x00063F72
		protected SafeCloseSocket() : base(true)
		{
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00065D7B File Offset: 0x00063F7B
		public override bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return base.IsClosed || base.IsInvalid;
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00065D8D File Offset: 0x00063F8D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void SetInnerSocket(SafeCloseSocket.InnerSafeCloseSocket socket)
		{
			this.m_InnerSocket = socket;
			base.SetHandle(socket.DangerousGetHandle());
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00065DA4 File Offset: 0x00063FA4
		private static SafeCloseSocket CreateSocket(SafeCloseSocket.InnerSafeCloseSocket socket)
		{
			SafeCloseSocket safeCloseSocket = new SafeCloseSocket();
			SafeCloseSocket.CreateSocket(socket, safeCloseSocket);
			return safeCloseSocket;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00065DC0 File Offset: 0x00063FC0
		protected static void CreateSocket(SafeCloseSocket.InnerSafeCloseSocket socket, SafeCloseSocket target)
		{
			if (socket != null && socket.IsInvalid)
			{
				target.SetHandleAsInvalid();
				return;
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				socket.DangerousAddRef(ref flag);
			}
			catch
			{
				if (flag)
				{
					socket.DangerousRelease();
					flag = false;
				}
			}
			finally
			{
				if (flag)
				{
					target.SetInnerSocket(socket);
					socket.Close();
				}
				else
				{
					target.SetHandleAsInvalid();
				}
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00065E34 File Offset: 0x00064034
		internal unsafe static SafeCloseSocket CreateWSASocket(byte* pinnedBuffer)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(pinnedBuffer));
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00065E41 File Offset: 0x00064041
		internal static SafeCloseSocket CreateWSASocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(addressFamily, socketType, protocolType));
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00065E50 File Offset: 0x00064050
		internal static SafeCloseSocket Accept(SafeCloseSocket socketHandle, byte[] socketAddress, ref int socketAddressSize)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.Accept(socketHandle, socketAddress, ref socketAddressSize));
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00065E60 File Offset: 0x00064060
		protected override bool ReleaseHandle()
		{
			this.m_Released = true;
			SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = (this.m_InnerSocket == null) ? null : Interlocked.Exchange<SafeCloseSocket.InnerSafeCloseSocket>(ref this.m_InnerSocket, null);
			if (innerSafeCloseSocket != null)
			{
				innerSafeCloseSocket.DangerousRelease();
			}
			return true;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00065E98 File Offset: 0x00064098
		internal void CloseAsIs()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = (this.m_InnerSocket == null) ? null : Interlocked.Exchange<SafeCloseSocket.InnerSafeCloseSocket>(ref this.m_InnerSocket, null);
				base.Close();
				if (innerSafeCloseSocket != null)
				{
					while (!this.m_Released)
					{
						Thread.SpinWait(1);
					}
					innerSafeCloseSocket.BlockingRelease();
				}
			}
		}

		// Token: 0x0400155B RID: 5467
		private SafeCloseSocket.InnerSafeCloseSocket m_InnerSocket;

		// Token: 0x0400155C RID: 5468
		private volatile bool m_Released;

		// Token: 0x02000757 RID: 1879
		internal class InnerSafeCloseSocket : SafeHandleMinusOneIsInvalid
		{
			// Token: 0x0600420C RID: 16908 RVA: 0x00112712 File Offset: 0x00110912
			protected InnerSafeCloseSocket() : base(true)
			{
			}

			// Token: 0x17000F1A RID: 3866
			// (get) Token: 0x0600420D RID: 16909 RVA: 0x0011271B File Offset: 0x0011091B
			public override bool IsInvalid
			{
				[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
				get
				{
					return base.IsClosed || base.IsInvalid;
				}
			}

			// Token: 0x0600420E RID: 16910 RVA: 0x00112730 File Offset: 0x00110930
			protected override bool ReleaseHandle()
			{
				SocketError socketError;
				if (this.m_Blockable)
				{
					socketError = UnsafeNclNativeMethods.SafeNetHandles.closesocket(this.handle);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
					if (socketError != SocketError.WouldBlock)
					{
						return socketError == SocketError.Success;
					}
					int num = 0;
					socketError = UnsafeNclNativeMethods.SafeNetHandles.ioctlsocket(this.handle, -2147195266, ref num);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
					if (socketError == SocketError.InvalidArgument)
					{
						socketError = UnsafeNclNativeMethods.SafeNetHandles.WSAEventSelect(this.handle, IntPtr.Zero, AsyncEventBits.FdNone);
						socketError = UnsafeNclNativeMethods.SafeNetHandles.ioctlsocket(this.handle, -2147195266, ref num);
					}
					if (socketError == SocketError.Success)
					{
						socketError = UnsafeNclNativeMethods.SafeNetHandles.closesocket(this.handle);
						if (socketError == SocketError.SocketError)
						{
							socketError = (SocketError)Marshal.GetLastWin32Error();
						}
						if (socketError != SocketError.WouldBlock)
						{
							return socketError == SocketError.Success;
						}
					}
				}
				Linger linger;
				linger.OnOff = 1;
				linger.Time = 0;
				socketError = UnsafeNclNativeMethods.SafeNetHandles.setsockopt(this.handle, SocketOptionLevel.Socket, SocketOptionName.Linger, ref linger, 4);
				if (socketError == SocketError.SocketError)
				{
					socketError = (SocketError)Marshal.GetLastWin32Error();
				}
				if (socketError != SocketError.Success && socketError != SocketError.InvalidArgument && socketError != SocketError.ProtocolOption)
				{
					return false;
				}
				socketError = UnsafeNclNativeMethods.SafeNetHandles.closesocket(this.handle);
				return socketError == SocketError.Success;
			}

			// Token: 0x0600420F RID: 16911 RVA: 0x0011283F File Offset: 0x00110A3F
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void BlockingRelease()
			{
				this.m_Blockable = true;
				base.DangerousRelease();
			}

			// Token: 0x06004210 RID: 16912 RVA: 0x00112850 File Offset: 0x00110A50
			internal unsafe static SafeCloseSocket.InnerSafeCloseSocket CreateWSASocket(byte* pinnedBuffer)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.OSSOCK.WSASocket(AddressFamily.Unknown, SocketType.Unknown, ProtocolType.Unknown, pinnedBuffer, 0U, SocketConstructorFlags.WSA_FLAG_OVERLAPPED);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x06004211 RID: 16913 RVA: 0x00112878 File Offset: 0x00110A78
			internal static SafeCloseSocket.InnerSafeCloseSocket CreateWSASocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.OSSOCK.WSASocket(addressFamily, socketType, protocolType, IntPtr.Zero, 0U, SocketConstructorFlags.WSA_FLAG_OVERLAPPED);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x06004212 RID: 16914 RVA: 0x001128A4 File Offset: 0x00110AA4
			internal static SafeCloseSocket.InnerSafeCloseSocket Accept(SafeCloseSocket socketHandle, byte[] socketAddress, ref int socketAddressSize)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.SafeNetHandles.accept(socketHandle.DangerousGetHandle(), socketAddress, ref socketAddressSize);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x04003221 RID: 12833
			private static readonly byte[] tempBuffer = new byte[1];

			// Token: 0x04003222 RID: 12834
			private bool m_Blockable;
		}
	}
}
