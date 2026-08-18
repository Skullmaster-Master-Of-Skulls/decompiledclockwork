using System;
using System.Security.Permissions;

namespace System.Net.Sockets
{
	// Token: 0x020005CA RID: 1482
	public class TcpListener
	{
		// Token: 0x06002E67 RID: 11879 RVA: 0x000CC8E0 File Offset: 0x000CB8E0
		public TcpListener(IPEndPoint localEP)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpListener", localEP);
			}
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_ServerSocketEP = localEP;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpListener", null);
			}
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000CC950 File Offset: 0x000CB950
		public TcpListener(IPAddress localaddr, int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "TcpListener", localaddr);
			}
			if (localaddr == null)
			{
				throw new ArgumentNullException("localaddr");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(localaddr, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "TcpListener", null);
			}
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000CC9DC File Offset: 0x000CB9DC
		[Obsolete("This method has been deprecated. Please use TcpListener(IPAddress localaddr, int port) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public TcpListener(int port)
		{
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(IPAddress.Any, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000CCA2B File Offset: 0x000CBA2B
		public Socket Server
		{
			get
			{
				return this.m_ServerSocket;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x000CCA33 File Offset: 0x000CBA33
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x000CCA3B File Offset: 0x000CBA3B
		public EndPoint LocalEndpoint
		{
			get
			{
				if (!this.m_Active)
				{
					return this.m_ServerSocketEP;
				}
				return this.m_ServerSocket.LocalEndPoint;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x000CCA57 File Offset: 0x000CBA57
		// (set) Token: 0x06002E6E RID: 11886 RVA: 0x000CCA64 File Offset: 0x000CBA64
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ServerSocket.ExclusiveAddressUse;
			}
			set
			{
				if (this.m_Active)
				{
					throw new InvalidOperationException(SR.GetString("net_tcplistener_mustbestopped"));
				}
				this.m_ServerSocket.ExclusiveAddressUse = value;
				this.m_ExclusiveAddressUse = value;
			}
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000CCA91 File Offset: 0x000CBA91
		public void Start()
		{
			this.Start(int.MaxValue);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000CCAA0 File Offset: 0x000CBAA0
		public void Start(int backlog)
		{
			if (backlog > 2147483647 || backlog < 0)
			{
				throw new ArgumentOutOfRangeException("backlog");
			}
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Start", null);
			}
			if (this.m_ServerSocket == null)
			{
				throw new InvalidOperationException(SR.GetString("net_InvalidSocketHandle"));
			}
			if (this.m_Active)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Sockets, this, "Start", null);
				}
				return;
			}
			this.m_ServerSocket.Bind(this.m_ServerSocketEP);
			this.m_ServerSocket.Listen(backlog);
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Start", null);
			}
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000CCB54 File Offset: 0x000CBB54
		public void Stop()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "Stop", null);
			}
			if (this.m_ServerSocket != null)
			{
				this.m_ServerSocket.Close();
				this.m_ServerSocket = null;
			}
			this.m_Active = false;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (this.m_ExclusiveAddressUse)
			{
				this.m_ServerSocket.ExclusiveAddressUse = true;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Stop", null);
			}
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000CCBDE File Offset: 0x000CBBDE
		public bool Pending()
		{
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			return this.m_ServerSocket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000CCC08 File Offset: 0x000CBC08
		public Socket AcceptSocket()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "AcceptSocket", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			Socket socket = this.m_ServerSocket.Accept();
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "AcceptSocket", socket);
			}
			return socket;
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000CCC6C File Offset: 0x000CBC6C
		public TcpClient AcceptTcpClient()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "AcceptTcpClient", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			Socket acceptedSocket = this.m_ServerSocket.Accept();
			TcpClient tcpClient = new TcpClient(acceptedSocket);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "AcceptTcpClient", tcpClient);
			}
			return tcpClient;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000CCCD8 File Offset: 0x000CBCD8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginAcceptSocket", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			IAsyncResult result = this.m_ServerSocket.BeginAccept(callback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginAcceptSocket", null);
			}
			return result;
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000CCD3C File Offset: 0x000CBD3C
		public Socket EndAcceptSocket(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "EndAcceptSocket", null);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			Socket socket = (lazyAsyncResult == null) ? null : (lazyAsyncResult.AsyncObject as Socket);
			if (socket == null)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			Socket socket2 = socket.EndAccept(asyncResult);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "EndAcceptSocket", socket2);
			}
			return socket2;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000CCDC4 File Offset: 0x000CBDC4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "BeginAcceptTcpClient", null);
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			IAsyncResult result = this.m_ServerSocket.BeginAccept(callback, state);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "BeginAcceptTcpClient", null);
			}
			return result;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000CCE28 File Offset: 0x000CBE28
		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, this, "EndAcceptTcpClient", null);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			Socket socket = (lazyAsyncResult == null) ? null : (lazyAsyncResult.AsyncObject as Socket);
			if (socket == null)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			Socket socket2 = socket.EndAccept(asyncResult);
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "EndAcceptTcpClient", socket2);
			}
			return new TcpClient(socket2);
		}

		// Token: 0x04002C32 RID: 11314
		private IPEndPoint m_ServerSocketEP;

		// Token: 0x04002C33 RID: 11315
		private Socket m_ServerSocket;

		// Token: 0x04002C34 RID: 11316
		private bool m_Active;

		// Token: 0x04002C35 RID: 11317
		private bool m_ExclusiveAddressUse;
	}
}
