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
	// Token: 0x02000528 RID: 1320
	[SuppressUnmanagedCodeSecurity]
	internal class SafeCloseSocket : SafeHandleMinusOneIsInvalid
	{
		// Token: 0x0600287B RID: 10363 RVA: 0x000A7876 File Offset: 0x000A6876
		protected SafeCloseSocket() : base(true)
		{
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x000A787F File Offset: 0x000A687F
		public override bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return base.IsClosed || base.IsInvalid;
			}
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000A7891 File Offset: 0x000A6891
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void SetInnerSocket(SafeCloseSocket.InnerSafeCloseSocket socket)
		{
			this.m_InnerSocket = socket;
			base.SetHandle(socket.DangerousGetHandle());
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000A78A8 File Offset: 0x000A68A8
		private static SafeCloseSocket CreateSocket(SafeCloseSocket.InnerSafeCloseSocket socket)
		{
			SafeCloseSocket safeCloseSocket = new SafeCloseSocket();
			SafeCloseSocket.CreateSocket(socket, safeCloseSocket);
			return safeCloseSocket;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000A78C4 File Offset: 0x000A68C4
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

		// Token: 0x06002880 RID: 10368 RVA: 0x000A793C File Offset: 0x000A693C
		internal unsafe static SafeCloseSocket CreateWSASocket(byte* pinnedBuffer)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(pinnedBuffer));
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000A7949 File Offset: 0x000A6949
		internal static SafeCloseSocket CreateWSASocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(addressFamily, socketType, protocolType));
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000A7958 File Offset: 0x000A6958
		internal static SafeCloseSocket Accept(SafeCloseSocket socketHandle, byte[] socketAddress, ref int socketAddressSize)
		{
			return SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.Accept(socketHandle, socketAddress, ref socketAddressSize));
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000A7968 File Offset: 0x000A6968
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

		// Token: 0x06002884 RID: 10372 RVA: 0x000A79A0 File Offset: 0x000A69A0
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

		// Token: 0x04002791 RID: 10129
		private SafeCloseSocket.InnerSafeCloseSocket m_InnerSocket;

		// Token: 0x04002792 RID: 10130
		private volatile bool m_Released;

		// Token: 0x02000529 RID: 1321
		internal class InnerSafeCloseSocket : SafeHandleMinusOneIsInvalid
		{
			// Token: 0x06002885 RID: 10373 RVA: 0x000A7A00 File Offset: 0x000A6A00
			protected InnerSafeCloseSocket() : base(true)
			{
			}

			// Token: 0x17000849 RID: 2121
			// (get) Token: 0x06002886 RID: 10374 RVA: 0x000A7A09 File Offset: 0x000A6A09
			public override bool IsInvalid
			{
				[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
				get
				{
					return base.IsClosed || base.IsInvalid;
				}
			}

			// Token: 0x06002887 RID: 10375 RVA: 0x000A7A1C File Offset: 0x000A6A1C
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

			// Token: 0x06002888 RID: 10376 RVA: 0x000A7B21 File Offset: 0x000A6B21
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void BlockingRelease()
			{
				this.m_Blockable = true;
				base.DangerousRelease();
			}

			// Token: 0x06002889 RID: 10377 RVA: 0x000A7B30 File Offset: 0x000A6B30
			internal unsafe static SafeCloseSocket.InnerSafeCloseSocket CreateWSASocket(byte* pinnedBuffer)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.OSSOCK.WSASocket(AddressFamily.Unknown, SocketType.Unknown, ProtocolType.Unknown, pinnedBuffer, 0U, SocketConstructorFlags.WSA_FLAG_OVERLAPPED);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x0600288A RID: 10378 RVA: 0x000A7B58 File Offset: 0x000A6B58
			internal static SafeCloseSocket.InnerSafeCloseSocket CreateWSASocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.OSSOCK.WSASocket(addressFamily, socketType, protocolType, IntPtr.Zero, 0U, SocketConstructorFlags.WSA_FLAG_OVERLAPPED);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x0600288B RID: 10379 RVA: 0x000A7B84 File Offset: 0x000A6B84
			internal static SafeCloseSocket.InnerSafeCloseSocket Accept(SafeCloseSocket socketHandle, byte[] socketAddress, ref int socketAddressSize)
			{
				SafeCloseSocket.InnerSafeCloseSocket innerSafeCloseSocket = UnsafeNclNativeMethods.SafeNetHandles.accept(socketHandle.DangerousGetHandle(), socketAddress, ref socketAddressSize);
				if (innerSafeCloseSocket.IsInvalid)
				{
					innerSafeCloseSocket.SetHandleAsInvalid();
				}
				return innerSafeCloseSocket;
			}

			// Token: 0x04002793 RID: 10131
			private static readonly byte[] tempBuffer = new byte[1];

			// Token: 0x04002794 RID: 10132
			private bool m_Blockable;
		}
	}
}
