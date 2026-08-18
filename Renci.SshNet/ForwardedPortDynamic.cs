using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x0200001D RID: 29
	public class ForwardedPortDynamic : ForwardedPort
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000495C File Offset: 0x00002B5C
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00004964 File Offset: 0x00002B64
		public string BoundHost { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000496D File Offset: 0x00002B6D
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00004975 File Offset: 0x00002B75
		public uint BoundPort { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000497E File Offset: 0x00002B7E
		public override bool IsStarted
		{
			get
			{
				return this._status == ForwardedPortStatus.Started;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004990 File Offset: 0x00002B90
		public ForwardedPortDynamic(uint port) : this(string.Empty, port)
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000499E File Offset: 0x00002B9E
		public ForwardedPortDynamic(string host, uint port)
		{
			this.BoundHost = host;
			this.BoundPort = port;
			this._status = ForwardedPortStatus.Stopped;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000049C0 File Offset: 0x00002BC0
		protected override void StartPort()
		{
			if (!ForwardedPortStatus.ToStarting(ref this._status))
			{
				return;
			}
			try
			{
				this.InternalStart();
			}
			catch (Exception)
			{
				this._status = ForwardedPortStatus.Stopped;
				throw;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004A04 File Offset: 0x00002C04
		protected override void StopPort(TimeSpan timeout)
		{
			if (!ForwardedPortStatus.ToStopping(ref this._status))
			{
				return;
			}
			base.StopPort(timeout);
			this.StopListener();
			this.InternalStop(timeout);
			this._status = ForwardedPortStatus.Stopped;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004A33 File Offset: 0x00002C33
		protected override void CheckDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004A50 File Offset: 0x00002C50
		private void InternalStart()
		{
			this.InitializePendingChannelCountdown();
			IPAddress address = IPAddress.Any;
			if (!string.IsNullOrEmpty(this.BoundHost))
			{
				address = DnsAbstraction.GetHostAddresses(this.BoundHost)[0];
			}
			IPEndPoint ipendPoint = new IPEndPoint(address, (int)this.BoundPort);
			this._listener = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
			{
				NoDelay = true
			};
			this._listener.Bind(ipendPoint);
			this._listener.Listen(5);
			base.Session.ErrorOccured += this.Session_ErrorOccured;
			base.Session.Disconnected += this.Session_Disconnected;
			this._status = ForwardedPortStatus.Started;
			this.StartAccept(null);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004B04 File Offset: 0x00002D04
		private void StopListener()
		{
			Socket listener = this._listener;
			if (listener != null)
			{
				listener.Dispose();
			}
			ISession session = base.Session;
			if (session != null)
			{
				session.ErrorOccured -= this.Session_ErrorOccured;
				session.Disconnected -= this.Session_Disconnected;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004B4F File Offset: 0x00002D4F
		private void InternalStop(TimeSpan timeout)
		{
			this._pendingChannelCountdown.Signal();
			this._pendingChannelCountdown.Wait(timeout);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004B6A File Offset: 0x00002D6A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004B7C File Offset: 0x00002D7C
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Socket listener = this._listener;
				if (listener != null)
				{
					this._listener = null;
					listener.Dispose();
				}
				CountdownEvent pendingChannelCountdown = this._pendingChannelCountdown;
				if (pendingChannelCountdown != null)
				{
					this._pendingChannelCountdown = null;
					pendingChannelCountdown.Dispose();
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004BBA File Offset: 0x00002DBA
		protected override void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			base.Dispose(disposing);
			this.InternalDispose(disposing);
			this._isDisposed = true;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004BDC File Offset: 0x00002DDC
		~ForwardedPortDynamic()
		{
			this.Dispose(false);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004C0C File Offset: 0x00002E0C
		private void StartAccept(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				e = new SocketAsyncEventArgs();
				e.Completed += this.AcceptCompleted;
			}
			else
			{
				e.AcceptSocket = null;
			}
			if (this.IsStarted)
			{
				try
				{
					if (!this._listener.AcceptAsync(e))
					{
						this.AcceptCompleted(null, e);
					}
				}
				catch (ObjectDisposedException)
				{
					if (!(this._status == ForwardedPortStatus.Stopped) && !(this._status == ForwardedPortStatus.Stopped))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004C98 File Offset: 0x00002E98
		private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.OperationAborted || e.SocketError == SocketError.NotSocket)
			{
				return;
			}
			Socket acceptSocket = e.AcceptSocket;
			if (e.SocketError != SocketError.Success)
			{
				this.StartAccept(e);
				ForwardedPortDynamic.CloseClientSocket(acceptSocket);
				return;
			}
			this.StartAccept(e);
			this.ProcessAccept(acceptSocket);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004CEC File Offset: 0x00002EEC
		private void ProcessAccept(Socket clientSocket)
		{
			if (!this.IsStarted)
			{
				ForwardedPortDynamic.CloseClientSocket(clientSocket);
				return;
			}
			CountdownEvent pendingChannelCountdown = this._pendingChannelCountdown;
			pendingChannelCountdown.AddCount();
			try
			{
				using (IChannelDirectTcpip channelDirectTcpip = base.Session.CreateChannelDirectTcpip())
				{
					channelDirectTcpip.Exception += this.Channel_Exception;
					try
					{
						if (!this.HandleSocks(channelDirectTcpip, clientSocket, base.Session.ConnectionInfo.Timeout))
						{
							ForwardedPortDynamic.CloseClientSocket(clientSocket);
						}
						else
						{
							channelDirectTcpip.Bind();
						}
					}
					finally
					{
						channelDirectTcpip.Close();
					}
				}
			}
			catch (Exception exception)
			{
				base.RaiseExceptionEvent(exception);
				ForwardedPortDynamic.CloseClientSocket(clientSocket);
			}
			finally
			{
				try
				{
					pendingChannelCountdown.Signal();
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004DD0 File Offset: 0x00002FD0
		private void InitializePendingChannelCountdown()
		{
			CountdownEvent countdownEvent = Interlocked.Exchange<CountdownEvent>(ref this._pendingChannelCountdown, new CountdownEvent(1));
			if (countdownEvent != null)
			{
				countdownEvent.Dispose();
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004DF8 File Offset: 0x00002FF8
		private bool HandleSocks(IChannelDirectTcpip channel, Socket clientSocket, TimeSpan timeout)
		{
			EventHandler value = delegate(object _, EventArgs args)
			{
				ForwardedPortDynamic.CloseClientSocket(clientSocket);
			};
			base.Closing += value;
			bool result;
			try
			{
				int num = SocketAbstraction.ReadByte(clientSocket, timeout);
				if (num != -1)
				{
					if (num != 4)
					{
						if (num != 5)
						{
							throw new NotSupportedException(string.Format("SOCKS version {0} is not supported.", num));
						}
						result = this.HandleSocks5(clientSocket, channel, timeout);
					}
					else
					{
						result = this.HandleSocks4(clientSocket, channel, timeout);
					}
				}
				else
				{
					result = false;
				}
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode != SocketError.Interrupted)
				{
					base.RaiseExceptionEvent(ex);
				}
				result = false;
			}
			finally
			{
				base.Closing -= value;
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004EC0 File Offset: 0x000030C0
		private static void CloseClientSocket(Socket clientSocket)
		{
			if (clientSocket.Connected)
			{
				try
				{
					clientSocket.Shutdown(SocketShutdown.Send);
				}
				catch (Exception)
				{
				}
			}
			clientSocket.Dispose();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004EF8 File Offset: 0x000030F8
		private void Session_Disconnected(object sender, EventArgs e)
		{
			ISession session = base.Session;
			if (session != null)
			{
				this.StopPort(session.ConnectionInfo.Timeout);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004F20 File Offset: 0x00003120
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			ISession session = base.Session;
			if (session != null)
			{
				this.StopPort(session.ConnectionInfo.Timeout);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004F48 File Offset: 0x00003148
		private void Channel_Exception(object sender, ExceptionEventArgs e)
		{
			base.RaiseExceptionEvent(e.Exception);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004F58 File Offset: 0x00003158
		private bool HandleSocks4(Socket socket, IChannelDirectTcpip channel, TimeSpan timeout)
		{
			if (SocketAbstraction.ReadByte(socket, timeout) == -1)
			{
				return false;
			}
			byte[] array = new byte[2];
			if (SocketAbstraction.Read(socket, array, 0, array.Length, timeout) == 0)
			{
				return false;
			}
			uint port = (uint)array[0] * 256U + (uint)array[1];
			byte[] array2 = new byte[4];
			if (SocketAbstraction.Read(socket, array2, 0, array2.Length, timeout) == 0)
			{
				return false;
			}
			IPAddress ipaddress = new IPAddress(array2);
			if (ForwardedPortDynamic.ReadString(socket, timeout) == null)
			{
				return false;
			}
			string text = ipaddress.ToString();
			base.RaiseRequestReceived(text, port);
			channel.Open(text, port, this, socket);
			SocketAbstraction.SendByte(socket, 0);
			if (channel.IsOpen)
			{
				SocketAbstraction.SendByte(socket, 90);
				SocketAbstraction.Send(socket, array, 0, array.Length);
				SocketAbstraction.Send(socket, array2, 0, array2.Length);
				return true;
			}
			SocketAbstraction.SendByte(socket, 91);
			return false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005014 File Offset: 0x00003214
		private bool HandleSocks5(Socket socket, IChannelDirectTcpip channel, TimeSpan timeout)
		{
			int num = SocketAbstraction.ReadByte(socket, timeout);
			if (num == -1)
			{
				return false;
			}
			byte[] array = new byte[num];
			if (SocketAbstraction.Read(socket, array, 0, array.Length, timeout) == 0)
			{
				return false;
			}
			if (array.Min<byte>() == 0)
			{
				byte[] array2 = new byte[2];
				array2[0] = 5;
				SocketAbstraction.Send(socket, array2, 0, 2);
			}
			else
			{
				SocketAbstraction.Send(socket, new byte[]
				{
					5,
					byte.MaxValue
				}, 0, 2);
			}
			int num2 = SocketAbstraction.ReadByte(socket, timeout);
			if (num2 == -1)
			{
				return false;
			}
			if (num2 != 5)
			{
				throw new ProxyException("SOCKS5: Version 5 is expected.");
			}
			if (SocketAbstraction.ReadByte(socket, timeout) == -1)
			{
				return false;
			}
			int num3 = SocketAbstraction.ReadByte(socket, timeout);
			if (num3 == -1)
			{
				return false;
			}
			if (num3 != 0)
			{
				throw new ProxyException("SOCKS5: 0 is expected for reserved byte.");
			}
			int num4 = SocketAbstraction.ReadByte(socket, timeout);
			if (num4 == -1)
			{
				return false;
			}
			IPAddress ipaddress;
			switch (num4)
			{
			case 1:
			{
				byte[] array3 = new byte[4];
				if (SocketAbstraction.Read(socket, array3, 0, 4, timeout) == 0)
				{
					return false;
				}
				ipaddress = new IPAddress(array3);
				goto IL_173;
			}
			case 3:
			{
				int num5 = SocketAbstraction.ReadByte(socket, timeout);
				if (num5 == -1)
				{
					return false;
				}
				byte[] array3 = new byte[num5];
				if (SocketAbstraction.Read(socket, array3, 0, array3.Length, timeout) == 0)
				{
					return false;
				}
				ipaddress = IPAddress.Parse(SshData.Ascii.GetString(array3, 0, array3.Length));
				goto IL_173;
			}
			case 4:
			{
				byte[] array3 = new byte[16];
				if (SocketAbstraction.Read(socket, array3, 0, 16, timeout) == 0)
				{
					return false;
				}
				ipaddress = new IPAddress(array3);
				goto IL_173;
			}
			}
			throw new ProxyException(string.Format("SOCKS5: Address type '{0}' is not supported.", num4));
			IL_173:
			byte[] array4 = new byte[2];
			if (SocketAbstraction.Read(socket, array4, 0, array4.Length, timeout) == 0)
			{
				return false;
			}
			uint port = (uint)array4[0] * 256U + (uint)array4[1];
			string text = ipaddress.ToString();
			base.RaiseRequestReceived(text, port);
			channel.Open(text, port, this, socket);
			SocketAbstraction.SendByte(socket, 5);
			if (channel.IsOpen)
			{
				SocketAbstraction.SendByte(socket, 0);
			}
			else
			{
				SocketAbstraction.SendByte(socket, 1);
			}
			SocketAbstraction.SendByte(socket, 0);
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				SocketAbstraction.SendByte(socket, 1);
			}
			else
			{
				if (ipaddress.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new NotSupportedException("Not supported address family.");
				}
				SocketAbstraction.SendByte(socket, 4);
			}
			byte[] addressBytes = ipaddress.GetAddressBytes();
			SocketAbstraction.Send(socket, addressBytes, 0, addressBytes.Length);
			SocketAbstraction.Send(socket, array4, 0, array4.Length);
			return true;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000525C File Offset: 0x0000345C
		private static string ReadString(Socket socket, TimeSpan timeout)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = new byte[1];
			while (SocketAbstraction.Read(socket, array, 0, 1, timeout) != 0)
			{
				byte b = array[0];
				if (b == 0)
				{
					return stringBuilder.ToString();
				}
				char value = (char)b;
				stringBuilder.Append(value);
			}
			return null;
		}

		// Token: 0x0400005C RID: 92
		private ForwardedPortStatus _status;

		// Token: 0x0400005F RID: 95
		private bool _isDisposed;

		// Token: 0x04000060 RID: 96
		private Socket _listener;

		// Token: 0x04000061 RID: 97
		private CountdownEvent _pendingChannelCountdown;
	}
}
