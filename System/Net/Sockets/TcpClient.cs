using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020005C9 RID: 1481
	public class TcpClient : IDisposable
	{
		// Token: 0x06002E3F RID: 11839 RVA: 0x000CBE68 File Offset: 0x000CAE68
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

		// Token: 0x06002E40 RID: 11840 RVA: 0x000CBEE2 File Offset: 0x000CAEE2
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

		// Token: 0x06002E41 RID: 11841 RVA: 0x000CBF1C File Offset: 0x000CAF1C
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

		// Token: 0x06002E42 RID: 11842 RVA: 0x000CBFA8 File Offset: 0x000CAFA8
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

		// Token: 0x06002E43 RID: 11843 RVA: 0x000CC060 File Offset: 0x000CB060
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

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x000CC0B8 File Offset: 0x000CB0B8
		// (set) Token: 0x06002E45 RID: 11845 RVA: 0x000CC0C0 File Offset: 0x000CB0C0
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

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000CC0C9 File Offset: 0x000CB0C9
		// (set) Token: 0x06002E47 RID: 11847 RVA: 0x000CC0D1 File Offset: 0x000CB0D1
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

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x000CC0DA File Offset: 0x000CB0DA
		public int Available
		{
			get
			{
				return this.m_ClientSocket.Available;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000CC0E7 File Offset: 0x000CB0E7
		public bool Connected
		{
			get
			{
				return this.m_ClientSocket.Connected;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000CC0F4 File Offset: 0x000CB0F4
		// (set) Token: 0x06002E4B RID: 11851 RVA: 0x000CC101 File Offset: 0x000CB101
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

		// Token: 0x06002E4C RID: 11852 RVA: 0x000CC110 File Offset: 0x000CB110
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
					if (Socket.SupportsIPv4)
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

		// Token: 0x06002E4D RID: 11853 RVA: 0x000CC340 File Offset: 0x000CB340
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

		// Token: 0x06002E4E RID: 11854 RVA: 0x000CC3C8 File Offset: 0x000CB3C8
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

		// Token: 0x06002E4F RID: 11855 RVA: 0x000CC440 File Offset: 0x000CB440
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

		// Token: 0x06002E50 RID: 11856 RVA: 0x000CC494 File Offset: 0x000CB494
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

		// Token: 0x06002E51 RID: 11857 RVA: 0x000CC4E4 File Offset: 0x000CB4E4
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

		// Token: 0x06002E52 RID: 11858 RVA: 0x000CC534 File Offset: 0x000CB534
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

		// Token: 0x06002E53 RID: 11859 RVA: 0x000CC584 File Offset: 0x000CB584
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

		// Token: 0x06002E54 RID: 11860 RVA: 0x000CC5D4 File Offset: 0x000CB5D4
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

		// Token: 0x06002E55 RID: 11861 RVA: 0x000CC670 File Offset: 0x000CB670
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

		// Token: 0x06002E56 RID: 11862 RVA: 0x000CC6B0 File Offset: 0x000CB6B0
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

		// Token: 0x06002E57 RID: 11863 RVA: 0x000CC770 File Offset: 0x000CB770
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000CC77C File Offset: 0x000CB77C
		~TcpClient()
		{
			this.Dispose(false);
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000CC7AC File Offset: 0x000CB7AC
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x000CC7BE File Offset: 0x000CB7BE
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

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000CC7D6 File Offset: 0x000CB7D6
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x000CC7E8 File Offset: 0x000CB7E8
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

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000CC800 File Offset: 0x000CB800
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x000CC812 File Offset: 0x000CB812
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

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000CC82A File Offset: 0x000CB82A
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000CC83C File Offset: 0x000CB83C
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

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000CC854 File Offset: 0x000CB854
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000CC870 File Offset: 0x000CB870
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

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000CC888 File Offset: 0x000CB888
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x000CC897 File Offset: 0x000CB897
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

		// Token: 0x06002E65 RID: 11877 RVA: 0x000CC8AD File Offset: 0x000CB8AD
		private void initialize()
		{
			this.Client = new Socket(this.m_Family, SocketType.Stream, ProtocolType.Tcp);
			this.m_Active = false;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000CC8C9 File Offset: 0x000CB8C9
		private int numericOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			return (int)this.Client.GetSocketOption(optionLevel, optionName);
		}

		// Token: 0x04002C2D RID: 11309
		private Socket m_ClientSocket;

		// Token: 0x04002C2E RID: 11310
		private bool m_Active;

		// Token: 0x04002C2F RID: 11311
		private NetworkStream m_DataStream;

		// Token: 0x04002C30 RID: 11312
		private AddressFamily m_Family = AddressFamily.InterNetwork;

		// Token: 0x04002C31 RID: 11313
		private bool m_CleanedUp;
	}
}
