using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x02000387 RID: 903
	public class TcpListener
	{
		// Token: 0x060021C7 RID: 8647 RVA: 0x000A1D70 File Offset: 0x0009FF70
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

		// Token: 0x060021C8 RID: 8648 RVA: 0x000A1DE0 File Offset: 0x0009FFE0
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

		// Token: 0x060021C9 RID: 8649 RVA: 0x000A1E6C File Offset: 0x000A006C
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

		// Token: 0x060021CA RID: 8650 RVA: 0x000A1EBC File Offset: 0x000A00BC
		public static TcpListener Create(int port)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Sockets, "TcpListener.Create", "Port: " + port.ToString());
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			TcpListener tcpListener = new TcpListener(IPAddress.IPv6Any, port);
			tcpListener.Server.DualMode = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, "TcpListener.Create", "Port: " + port.ToString());
			}
			return tcpListener;
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000A1F43 File Offset: 0x000A0143
		public Socket Server
		{
			get
			{
				return this.m_ServerSocket;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x000A1F4B File Offset: 0x000A014B
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000A1F53 File Offset: 0x000A0153
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

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000A1F6F File Offset: 0x000A016F
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x000A1F7C File Offset: 0x000A017C
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

		// Token: 0x060021D0 RID: 8656 RVA: 0x000A1FA9 File Offset: 0x000A01A9
		public void AllowNatTraversal(bool allowed)
		{
			if (this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_tcplistener_mustbestopped"));
			}
			if (allowed)
			{
				this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
				return;
			}
			this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000A1FE1 File Offset: 0x000A01E1
		public void Start()
		{
			this.Start(int.MaxValue);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000A1FF0 File Offset: 0x000A01F0
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
			try
			{
				this.m_ServerSocket.Listen(backlog);
			}
			catch (SocketException)
			{
				this.Stop();
				throw;
			}
			this.m_Active = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Sockets, this, "Start", null);
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000A20BC File Offset: 0x000A02BC
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

		// Token: 0x060021D4 RID: 8660 RVA: 0x000A2146 File Offset: 0x000A0346
		public bool Pending()
		{
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_stopped"));
			}
			return this.m_ServerSocket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000A2170 File Offset: 0x000A0370
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

		// Token: 0x060021D6 RID: 8662 RVA: 0x000A21D4 File Offset: 0x000A03D4
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

		// Token: 0x060021D7 RID: 8663 RVA: 0x000A2240 File Offset: 0x000A0440
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

		// Token: 0x060021D8 RID: 8664 RVA: 0x000A22A4 File Offset: 0x000A04A4
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

		// Token: 0x060021D9 RID: 8665 RVA: 0x000A232C File Offset: 0x000A052C
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

		// Token: 0x060021DA RID: 8666 RVA: 0x000A2390 File Offset: 0x000A0590
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

		// Token: 0x060021DB RID: 8667 RVA: 0x000A241A File Offset: 0x000A061A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Socket> AcceptSocketAsync()
		{
			return Task<Socket>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptSocket), new Func<IAsyncResult, Socket>(this.EndAcceptSocket), null);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000A243F File Offset: 0x000A063F
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<TcpClient> AcceptTcpClientAsync()
		{
			return Task<TcpClient>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptTcpClient), new Func<IAsyncResult, TcpClient>(this.EndAcceptTcpClient), null);
		}

		// Token: 0x04001F4A RID: 8010
		private IPEndPoint m_ServerSocketEP;

		// Token: 0x04001F4B RID: 8011
		private Socket m_ServerSocket;

		// Token: 0x04001F4C RID: 8012
		private bool m_Active;

		// Token: 0x04001F4D RID: 8013
		private bool m_ExclusiveAddressUse;
	}
}
