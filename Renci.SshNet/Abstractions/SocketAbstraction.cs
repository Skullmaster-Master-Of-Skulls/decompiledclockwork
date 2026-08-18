using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Abstractions
{
	// Token: 0x02000119 RID: 281
	internal static class SocketAbstraction
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x000271F5 File Offset: 0x000253F5
		public static bool CanRead(Socket socket)
		{
			return socket.Connected && socket.Poll(-1, SelectMode.SelectRead) && socket.Available > 0;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00027216 File Offset: 0x00025416
		public static bool CanWrite(Socket socket)
		{
			return socket.Connected && socket.Poll(-1, SelectMode.SelectWrite);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002722C File Offset: 0x0002542C
		public static Socket Connect(IPEndPoint remoteEndpoint, TimeSpan connectTimeout)
		{
			Socket socket = new Socket(remoteEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
			{
				NoDelay = true
			};
			ManualResetEvent manualResetEvent = new ManualResetEvent(false);
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs
			{
				UserToken = manualResetEvent,
				RemoteEndPoint = remoteEndpoint
			};
			socketAsyncEventArgs.Completed += SocketAbstraction.ConnectCompleted;
			if (socket.ConnectAsync(socketAsyncEventArgs) && !manualResetEvent.WaitOne(connectTimeout))
			{
				throw new SshOperationTimeoutException(string.Format(CultureInfo.InvariantCulture, "Connection failed to establish within {0:F0} milliseconds.", new object[]
				{
					connectTimeout.TotalMilliseconds
				}));
			}
			if (socketAsyncEventArgs.SocketError != SocketError.Success)
			{
				throw new SocketException((int)socketAsyncEventArgs.SocketError);
			}
			return socket;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000272CC File Offset: 0x000254CC
		public static void ClearReadBuffer(Socket socket)
		{
			try
			{
				byte[] array = new byte[256];
				int num;
				do
				{
					num = SocketAbstraction.ReadPartial(socket, array, 0, array.Length, TimeSpan.FromSeconds(2.0));
				}
				while (num > 0);
			}
			catch
			{
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00027318 File Offset: 0x00025518
		public static int ReadPartial(Socket socket, byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			socket.ReceiveTimeout = (int)timeout.TotalMilliseconds;
			int result;
			try
			{
				result = socket.Receive(buffer, offset, size, SocketFlags.None);
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.TimedOut)
				{
					throw new SshOperationTimeoutException(string.Format(CultureInfo.InvariantCulture, "Socket read operation has timed out after {0:F0} milliseconds.", new object[]
					{
						timeout.TotalMilliseconds
					}));
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002738C File Offset: 0x0002558C
		public static void ReadContinuous(Socket socket, byte[] buffer, int offset, int size, Action<byte[], int, int> processReceivedBytesAction)
		{
			socket.ReceiveTimeout = 0;
			while (socket.Connected)
			{
				try
				{
					int num = socket.Receive(buffer, offset, size, SocketFlags.None);
					if (num == 0)
					{
						break;
					}
					processReceivedBytesAction(buffer, offset, num);
				}
				catch (SocketException ex)
				{
					if (!SocketAbstraction.IsErrorResumable(ex.SocketErrorCode))
					{
						SocketError socketErrorCode = ex.SocketErrorCode;
						if (socketErrorCode == SocketError.Interrupted || socketErrorCode == SocketError.ConnectionAborted || socketErrorCode == SocketError.ConnectionReset)
						{
							break;
						}
						throw;
					}
				}
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002740C File Offset: 0x0002560C
		public static int ReadByte(Socket socket, TimeSpan timeout)
		{
			byte[] array = new byte[1];
			if (SocketAbstraction.Read(socket, array, 0, 1, timeout) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00027434 File Offset: 0x00025634
		public static void SendByte(Socket socket, byte value)
		{
			byte[] data = new byte[]
			{
				value
			};
			SocketAbstraction.Send(socket, data, 0, 1);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00027458 File Offset: 0x00025658
		public static int Read(Socket socket, byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			int num = 0;
			socket.ReceiveTimeout = (int)timeout.TotalMilliseconds;
			do
			{
				try
				{
					int num2 = socket.Receive(buffer, offset + num, size - num, SocketFlags.None);
					if (num2 == 0)
					{
						return 0;
					}
					num += num2;
				}
				catch (SocketException ex)
				{
					if (SocketAbstraction.IsErrorResumable(ex.SocketErrorCode))
					{
						ThreadAbstraction.Sleep(30);
					}
					else
					{
						if (ex.SocketErrorCode == SocketError.TimedOut)
						{
							throw new SshOperationTimeoutException(string.Format(CultureInfo.InvariantCulture, "Socket read operation has timed out after {0:F0} milliseconds.", new object[]
							{
								timeout.TotalMilliseconds
							}));
						}
						throw;
					}
				}
			}
			while (num < size);
			return num;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00027500 File Offset: 0x00025700
		public static void Send(Socket socket, byte[] data)
		{
			SocketAbstraction.Send(socket, data, 0, data.Length);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00027510 File Offset: 0x00025710
		public static void Send(Socket socket, byte[] data, int offset, int size)
		{
			int num = 0;
			do
			{
				try
				{
					int num2 = socket.Send(data, offset + num, size - num, SocketFlags.None);
					if (num2 == 0)
					{
						throw new SshConnectionException("An established connection was aborted by the server.", DisconnectReason.ConnectionLost);
					}
					num += num2;
				}
				catch (SocketException ex)
				{
					if (!SocketAbstraction.IsErrorResumable(ex.SocketErrorCode))
					{
						throw;
					}
					ThreadAbstraction.Sleep(30);
				}
			}
			while (num < size);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00027574 File Offset: 0x00025774
		public static bool IsErrorResumable(SocketError socketError)
		{
			return socketError == SocketError.IOPending || socketError == SocketError.WouldBlock || socketError == SocketError.NoBufferSpaceAvailable;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00027594 File Offset: 0x00025794
		private static void ConnectCompleted(object sender, SocketAsyncEventArgs e)
		{
			ManualResetEvent manualResetEvent = (ManualResetEvent)e.UserToken;
			if (manualResetEvent != null)
			{
				manualResetEvent.Set();
			}
		}
	}
}
