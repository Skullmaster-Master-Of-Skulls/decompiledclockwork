using System;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x02000386 RID: 902
	public class TcpClient : IDisposable
	{
		// Token: 0x0600219C RID: 8604 RVA: 0x000A1284 File Offset: 0x0009F484
		public TcpClient(IPEndPoint localEP)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", localEP);
			}
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_Family = localEP.AddressFamily;
			this.initialize();
			this.Client.Bind(localEP);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", "");
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000A12FE File Offset: 0x0009F4FE
		public TcpClient() : this(AddressFamily.InterNetwork)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", null);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000A1338 File Offset: 0x0009F538
		public TcpClient(AddressFamily family)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", family);
			}
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(SR.GetString("net_protocol_invalid_family", new object[]
				{
					"TCP"
				}), "family");
			}
			this.m_Family = family;
			this.initialize();
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000A13C0 File Offset: 0x0009F5C0
		public TcpClient(string hostname, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", hostname);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			try
			{
				this.Connect(hostname, port);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (this.m_ClientSocket != null)
				{
					this.m_ClientSocket.Close();
				}
				throw ex;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000A1478 File Offset: 0x0009F678
		internal TcpClient(Socket acceptedSocket)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpClient", acceptedSocket);
			}
			this.Client = acceptedSocket;
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpClient", null);
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x000A14D0 File Offset: 0x0009F6D0
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x000A14D8 File Offset: 0x0009F6D8
		public Socket Client
		{
			get
			{
				return this.m_ClientSocket;
			}
			set
			{
				this.m_ClientSocket = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x000A14E1 File Offset: 0x0009F6E1
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x000A14E9 File Offset: 0x0009F6E9
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000A14F2 File Offset: 0x0009F6F2
		public int Available
		{
			get
			{
				return this.m_ClientSocket.Available;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x000A14FF File Offset: 0x0009F6FF
		public bool Connected
		{
			get
			{
				return this.m_ClientSocket.Connected;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x000A150C File Offset: 0x0009F70C
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x000A1519 File Offset: 0x0009F719
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ClientSocket.ExclusiveAddressUse;
			}
			set
			{
				this.m_ClientSocket.ExclusiveAddressUse = value;
			}
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000A1528 File Offset: 0x0009F728
		public void Connect(string hostname, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", hostname);
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.m_Active)
			{
				throw new SocketException(SocketError.IsConnected);
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			Exception ex = null;
			Socket socket = null;
			Socket socket2 = null;
			try
			{
				if (this.m_ClientSocket == null)
				{
					if (Socket.OSSupportsIPv4)
					{
						socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					}
					if (Socket.OSSupportsIPv6)
					{
						socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
					}
				}
				foreach (IPAddress ipaddress in hostAddresses)
				{
					try
					{
						if (this.m_ClientSocket == null)
						{
							if (ipaddress.AddressFamily == AddressFamily.InterNetwork && socket2 != null)
							{
								socket2.Connect(ipaddress, port);
								this.m_ClientSocket = socket2;
								if (socket != null)
								{
									socket.Close();
								}
							}
							else if (socket != null)
							{
								socket.Connect(ipaddress, port);
								this.m_ClientSocket = socket;
								if (socket2 != null)
								{
									socket2.Close();
								}
							}
							this.m_Family = ipaddress.AddressFamily;
							this.m_Active = true;
							break;
						}
						if (ipaddress.AddressFamily == this.m_Family)
						{
							this.Connect(new IPEndPoint(ipaddress, port));
							this.m_Active = true;
							break;
						}
					}
					catch (Exception ex2)
					{
						if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			catch (Exception ex3)
			{
				if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!this.m_Active)
				{
					if (socket != null)
					{
						socket.Close();
					}
					if (socket2 != null)
					{
						socket2.Close();
					}
					if (ex != null)
					{
						throw ex;
					}
					throw new SocketException(SocketError.NotConnected);
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000A1758 File Offset: 0x0009F958
		public void Connect(IPAddress address, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", address);
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPEndPoint remoteEP = new IPEndPoint(address, port);
			this.Connect(remoteEP);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000A17E0 File Offset: 0x0009F9E0
		public void Connect(IPEndPoint remoteEP)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", remoteEP);
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			this.Client.Connect(remoteEP);
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000A1858 File Offset: 0x0009FA58
		public void Connect(IPAddress[] ipAddresses, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Connect", ipAddresses);
			}
			this.Client.Connect(ipAddresses, port);
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Connect", null);
			}
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000A18AC File Offset: 0x0009FAAC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", host);
			}
			IAsyncResult result = this.Client.BeginConnect(host, port, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnect", null);
			}
			return result;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000A18FC File Offset: 0x0009FAFC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", address);
			}
			IAsyncResult result = this.Client.BeginConnect(address, port, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnect", null);
			}
			return result;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000A194C File Offset: 0x0009FB4C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginConnect", addresses);
			}
			IAsyncResult result = this.Client.BeginConnect(addresses, port, requestCallback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginConnect", null);
			}
			return result;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000A199C File Offset: 0x0009FB9C
		public void EndConnect(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "EndConnect", asyncResult);
			}
			this.Client.EndConnect(asyncResult);
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "EndConnect", null);
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000A19EC File Offset: 0x0009FBEC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task ConnectAsync(IPAddress address, int port)
		{
			return Task.Factory.FromAsync<IPAddress, int>(new Func<IPAddress, int, AsyncCallback, object, IAsyncResult>(this.BeginConnect), new Action<IAsyncResult>(this.EndConnect), address, port, null);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000A1A13 File Offset: 0x0009FC13
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task ConnectAsync(string host, int port)
		{
			return Task.Factory.FromAsync<string, int>(new Func<string, int, AsyncCallback, object, IAsyncResult>(this.BeginConnect), new Action<IAsyncResult>(this.EndConnect), host, port, null);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000A1A3A File Offset: 0x0009FC3A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task ConnectAsync(IPAddress[] addresses, int port)
		{
			return Task.Factory.FromAsync<IPAddress[], int>(new Func<IPAddress[], int, AsyncCallback, object, IAsyncResult>(this.BeginConnect), new Action<IAsyncResult>(this.EndConnect), addresses, port, null);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000A1A64 File Offset: 0x0009FC64
		public NetworkStream GetStream()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "GetStream", "");
			}
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.Client.Connected)
			{
				throw new InvalidOperationException(SR.GetString("net_notconnected"));
			}
			if (this.m_DataStream == null)
			{
				this.m_DataStream = new NetworkStream(this.Client, true);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "GetStream", this.m_DataStream);
			}
			return this.m_DataStream;
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000A1B00 File Offset: 0x0009FD00
		public void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Close", "");
			}
			((IDisposable)this).Dispose();
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Close", "");
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000A1B40 File Offset: 0x0009FD40
		protected virtual void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Dispose", "");
			}
			if (this.m_CleanedUp)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Sockets, this, "Dispose", "");
				}
				return;
			}
			if (disposing)
			{
				IDisposable dataStream = this.m_DataStream;
				if (dataStream != null)
				{
					dataStream.Dispose();
				}
				else
				{
					Socket client = this.Client;
					if (client != null)
					{
						try
						{
							client.InternalShutdown(SocketShutdown.Both);
						}
						finally
						{
							client.Close();
							this.Client = null;
						}
					}
				}
				GC.SuppressFinalize(this);
			}
			this.m_CleanedUp = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Dispose", "");
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000A1C00 File Offset: 0x0009FE00
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000A1C0C File Offset: 0x0009FE0C
		~TcpClient()
		{
			this.Dispose(false);
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000A1C3C File Offset: 0x0009FE3C
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x000A1C4E File Offset: 0x0009FE4E
		public int ReceiveBufferSize
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x000A1C66 File Offset: 0x0009FE66
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x000A1C78 File Offset: 0x0009FE78
		public int SendBufferSize
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000A1C90 File Offset: 0x0009FE90
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x000A1CA2 File Offset: 0x0009FEA2
		public int ReceiveTimeout
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x000A1CBA File Offset: 0x0009FEBA
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x000A1CCC File Offset: 0x0009FECC
		public int SendTimeout
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x000A1CE4 File Offset: 0x0009FEE4
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x000A1D00 File Offset: 0x0009FF00
		public LingerOption LingerState
		{
			get
			{
				return (LingerOption)this.Client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x000A1D18 File Offset: 0x0009FF18
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x000A1D27 File Offset: 0x0009FF27
		public bool NoDelay
		{
			get
			{
				return this.numericOption(SocketOptionLevel.Tcp, SocketOptionName.Debug) != 0;
			}
			set
			{
				this.Client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, value ? 1 : 0);
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000A1D3D File Offset: 0x0009FF3D
		private void initialize()
		{
			this.Client = new Socket(this.m_Family, SocketType.Stream, ProtocolType.Tcp);
			this.m_Active = false;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000A1D59 File Offset: 0x0009FF59
		private int numericOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			return (int)this.Client.GetSocketOption(optionLevel, optionName);
		}

		// Token: 0x04001F45 RID: 8005
		private Socket m_ClientSocket;

		// Token: 0x04001F46 RID: 8006
		private bool m_Active;

		// Token: 0x04001F47 RID: 8007
		private NetworkStream m_DataStream;

		// Token: 0x04001F48 RID: 8008
		private AddressFamily m_Family = AddressFamily.InterNetwork;

		// Token: 0x04001F49 RID: 8009
		private bool m_CleanedUp;
	}
}
