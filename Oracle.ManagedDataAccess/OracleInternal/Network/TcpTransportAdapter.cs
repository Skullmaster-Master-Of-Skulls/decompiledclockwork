using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Threading;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000174 RID: 372
	internal class TcpTransportAdapter : ITransportAdapter
	{
		// Token: 0x06000E7D RID: 3709 RVA: 0x000977BC File Offset: 0x000959BC
		internal TcpTransportAdapter(NameValueCollection socketOptions)
		{
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000977F0 File Offset: 0x000959F0
		internal TcpTransportAdapter(ConnectionOption conOption, TcpClient tcpClient)
		{
			this.m_client = tcpClient;
			this.m_Connected = true;
			this.Initialize(conOption);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00097844 File Offset: 0x00095A44
		[SocketPermission(SecurityAction.Assert, Unrestricted = true)]
		[DnsPermission(SecurityAction.Assert, Unrestricted = true)]
		public virtual void Connect(ConnectionOption conOption)
		{
			int num = 0;
			IPAddress[] array = null;
			SocketException ex = null;
			new SqlNetOraConfig();
			this.Initialize(conOption);
			this.m_portNo = conOption.Port;
			if (conOption.Host == null)
			{
				this.m_host = "";
			}
			else
			{
				this.m_host = conOption.Host;
			}
			if (conOption.IP != null && string.Equals(conOption.IP, "loopback", StringComparison.InvariantCultureIgnoreCase))
			{
				array = new IPAddress[]
				{
					IPAddress.Loopback
				};
			}
			else if (this.m_host.Contains(":"))
			{
				array = new IPAddress[]
				{
					IPAddress.Parse(this.m_host)
				};
			}
			else
			{
				try
				{
					if ((array = Dns.GetHostAddresses(this.m_host)) == null)
					{
						throw new NetworkException(12545);
					}
				}
				catch (Exception inner)
				{
					throw new NetworkException(12545, inner);
				}
			}
			do
			{
				try
				{
					ex = null;
					IPAddress ipaddress = array[num++];
					this.m_client = new TcpClient(ipaddress.AddressFamily);
					this.m_client.Client.NoDelay = SqlNetOraConfig.NoDelay;
					if (conOption.SBS != 0)
					{
						this.m_client.Client.SendBufferSize = conOption.SBS;
					}
					if (conOption.RBS != 0)
					{
						this.m_client.Client.ReceiveBufferSize = conOption.RBS;
					}
					IAsyncResult asyncResult = this.m_client.BeginConnect(ipaddress, this.m_portNo, null, null);
					asyncResult.AsyncWaitHandle.WaitOne(conOption.TransportConnectTO, false);
					this.m_client.EndConnect(asyncResult);
					if (this.m_client.Client != null)
					{
						this.m_Connected = true;
					}
				}
				catch (SocketException ex2)
				{
					ex = ex2;
				}
			}
			while (!this.m_Connected && num < array.Length);
			if (this.m_Connected)
			{
				return;
			}
			if (ex == null)
			{
				throw new NetworkException(-6403);
			}
			if (ex.ErrorCode == 10061)
			{
				throw new NetworkException(12541, ex);
			}
			throw new NetworkException(-6403, ex);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00097A3C File Offset: 0x00095C3C
		private void Initialize(ConnectionOption conOption)
		{
			this.m_conOption = conOption;
			this.m_OraBufPool = conOption.AsyncBufferPool;
			this.m_OraBufInitArg = conOption.AsyncBufferInitArg;
			if (this.m_OraBufPool == null)
			{
				throw new NetworkException(12532);
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00097A70 File Offset: 0x00095C70
		[SocketPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private void StartListening(IPAddress addr, int i)
		{
			this.m_listsocks[i] = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (this.m_v6OnlyOff)
			{
				this.m_listsocks[i].SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
			}
			this.m_listsocks[i].Bind(new IPEndPoint(addr, this.m_portNo));
			this.m_listsocks[i].Listen(TcpTransportAdapter.m_DefListenBacklog);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00097AD8 File Offset: 0x00095CD8
		public void Listen(ConnectionOption conOption)
		{
			this.Initialize(conOption);
			try
			{
				this.m_portNo = conOption.Port;
				IPAddress addr;
				if (conOption.Host == null || conOption.Host == "" || string.Compare(conOption.Host, Dns.GetHostName(), true) == 0 || conOption.inAddr_Any)
				{
					this.m_host = "";
					if (Environment.OSVersion.Version.Major >= 6)
					{
						addr = IPAddress.IPv6Any;
						this.m_v6OnlyOff = true;
					}
					else if (NetEnvironment.gotIPv4)
					{
						addr = IPAddress.Any;
						if (NetEnvironment.gotIPv6)
						{
							this.m_DblListen = true;
						}
					}
					else
					{
						addr = IPAddress.IPv6Any;
					}
				}
				else
				{
					this.m_host = conOption.Host;
					if (this.m_host.Contains(":"))
					{
						addr = IPAddress.Parse(this.m_host);
					}
					else
					{
						IPAddress[] hostAddresses = Dns.GetHostAddresses(this.m_host);
						addr = hostAddresses[0];
					}
				}
				this.StartListening(addr, 0);
				if (this.m_DblListen)
				{
					this.StartListening(IPAddress.IPv6Any, 1);
					this.m_AcceptARs[0] = this.m_listsocks[0].BeginAccept(null, null);
					this.m_AcceptARs[1] = this.m_listsocks[1].BeginAccept(null, null);
				}
			}
			catch (SocketException ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00097C28 File Offset: 0x00095E28
		private Socket DblListenAccept()
		{
			int num = WaitHandle.WaitAny(new WaitHandle[]
			{
				this.m_AcceptARs[0].AsyncWaitHandle,
				this.m_AcceptARs[1].AsyncWaitHandle
			});
			Socket result = this.m_listsocks[num].EndAccept(this.m_AcceptARs[num]);
			this.m_AcceptARs[num] = this.m_listsocks[num].BeginAccept(null, null);
			return result;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00097C94 File Offset: 0x00095E94
		public virtual ITransportAdapter Answer(ConnectionOption conOption)
		{
			this.Initialize(conOption);
			ITransportAdapter result;
			try
			{
				if (this.m_listsocks[0] == null || (this.m_DblListen && this.m_listsocks[1] == null))
				{
					throw new NetworkException(-6002);
				}
				Socket client;
				if (!this.m_DblListen)
				{
					client = this.m_listsocks[0].Accept();
				}
				else
				{
					client = this.DblListenAccept();
				}
				result = new TcpTransportAdapter(conOption, new TcpClient
				{
					Client = client
				});
			}
			catch (SocketException)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00097D1C File Offset: 0x00095F1C
		public virtual void Disconnect()
		{
			lock (this.m_discLock)
			{
				try
				{
					if (this.m_client != null)
					{
						this.m_client.Client.Close();
						this.m_client.Close();
						this.m_client = null;
					}
					if (this.m_listener != null)
					{
						this.m_listener.Stop();
						this.m_listener = null;
					}
				}
				catch (SocketException)
				{
				}
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00097DAC File Offset: 0x00095FAC
		public virtual Stream GetStream()
		{
			if (this.m_client != null)
			{
				return this.m_client.GetStream();
			}
			return null;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00097DC4 File Offset: 0x00095FC4
		public virtual Socket GetSocket()
		{
			if (this.m_client != null)
			{
				return this.m_client.Client;
			}
			return null;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00097DDC File Offset: 0x00095FDC
		public virtual bool UrgentDataSupported()
		{
			return true;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00097DE0 File Offset: 0x00095FE0
		private void Send(OraArraySegment[] OAS, int OASLength)
		{
			for (int i = 0; i < OASLength; i++)
			{
				this.m_client.Client.Send(OAS[i].Array, OAS[i].Offset, OAS[i].Count, SocketFlags.None);
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00097E24 File Offset: 0x00096024
		public virtual void Send(OraBuf OB)
		{
			try
			{
				if (OB.the_ByteSegments_Count == 2 && OB.the_ByteSegments[0].Array == OB.the_ByteSegments[1].Array)
				{
					this.m_client.Client.Send(OB.m_buf, 0, OB.m_curlen, SocketFlags.None);
				}
				else
				{
					this.Send(OB.the_ByteSegments, OB.the_ByteSegments_Count);
				}
			}
			catch (Exception inner)
			{
				throw new NetworkException(12571, inner);
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00097EA8 File Offset: 0x000960A8
		public virtual void BeginAsyncReceives(OraBuf.AsyncReceiveCallback myCallback, int AsyncBufferSize)
		{
			this.m_OraBufSize = AsyncBufferSize;
			this.m_AsyncRecvCB = myCallback;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00097EB8 File Offset: 0x000960B8
		public void SendUrgent(byte[] data, int offset, int length)
		{
			this.m_client.Client.Send(data, offset, length, SocketFlags.OutOfBand);
		}

		// Token: 0x170002AA RID: 682
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x00097ED0 File Offset: 0x000960D0
		public ConOraBufPool OraBufPool
		{
			set
			{
				this.m_OraBufPool = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00097EDC File Offset: 0x000960DC
		public bool Connected
		{
			get
			{
				return this.m_client.Connected && !this.m_client.Client.Poll(0, SelectMode.SelectError) && (!this.m_client.Client.Poll(0, SelectMode.SelectRead) || this.m_client.Client.Available != 0);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00097F38 File Offset: 0x00096138
		public virtual bool NeedReneg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00097F3C File Offset: 0x0009613C
		public virtual void Renegotiate(ConnectionOption conOption)
		{
		}

		// Token: 0x040010BF RID: 4287
		protected TcpClient m_client;

		// Token: 0x040010C0 RID: 4288
		protected TcpListener m_listener;

		// Token: 0x040010C1 RID: 4289
		protected string m_host;

		// Token: 0x040010C2 RID: 4290
		protected int m_portNo = -1;

		// Token: 0x040010C3 RID: 4291
		protected bool m_Connected;

		// Token: 0x040010C4 RID: 4292
		protected ConnectionOption m_conOption;

		// Token: 0x040010C5 RID: 4293
		protected OraBuf.AsyncReceiveCallback m_AsyncRecvCB;

		// Token: 0x040010C6 RID: 4294
		protected ConOraBufPool m_OraBufPool;

		// Token: 0x040010C7 RID: 4295
		protected int m_OraBufSize;

		// Token: 0x040010C8 RID: 4296
		protected object m_OraBufInitArg;

		// Token: 0x040010C9 RID: 4297
		protected IAsyncResult m_ar;

		// Token: 0x040010CA RID: 4298
		protected Socket[] m_listsocks = new Socket[2];

		// Token: 0x040010CB RID: 4299
		protected bool m_v6OnlyOff;

		// Token: 0x040010CC RID: 4300
		protected bool m_DblListen;

		// Token: 0x040010CD RID: 4301
		private bool m_EOF;

		// Token: 0x040010CE RID: 4302
		private static int m_DefListenBacklog = 50;

		// Token: 0x040010CF RID: 4303
		private IAsyncResult[] m_AcceptARs = new IAsyncResult[2];

		// Token: 0x040010D0 RID: 4304
		protected byte[] m_AsyncBuffer;

		// Token: 0x040010D1 RID: 4305
		protected OraBuf m_OraBuf;

		// Token: 0x040010D2 RID: 4306
		protected object m_discLock = new object();
	}
}
