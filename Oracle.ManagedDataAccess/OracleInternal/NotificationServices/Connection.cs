using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200017D RID: 381
	internal class Connection
	{
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00098C54 File Offset: 0x00096E54
		internal Connection(NodeList cList, string cHost, int cPort, int index)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.nodeList = cList;
			this.ons = cList.ons;
			this.host = cHost;
			this.port = cPort;
			this.listIndex = index;
			this.socketlock = new object();
			this.socket = null;
			this.scanDelay = 0L;
			this.shutdown = false;
			this.waiters = 0;
			this.id = string.Concat(new object[]
			{
				this.nodeList.getId(),
				"-",
				this.host,
				":",
				this.port
			});
			this.serverVersion = 4;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00098D38 File Offset: 0x00096F38
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00098D40 File Offset: 0x00096F40
		protected internal virtual int ConcurrencyIndex
		{
			get
			{
				return this.concurrencyIndex;
			}
			set
			{
				this.concurrencyIndex = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x00098D4C File Offset: 0x00096F4C
		protected internal virtual long ScanDelay
		{
			set
			{
				this.scanDelay = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00098D58 File Offset: 0x00096F58
		public virtual string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00098D60 File Offset: 0x00096F60
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00098D68 File Offset: 0x00096F68
		public virtual int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
			set
			{
				this.serverVersion = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00098D74 File Offset: 0x00096F74
		protected internal virtual int ListIndex
		{
			get
			{
				return this.listIndex;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00098D7C File Offset: 0x00096F7C
		protected internal virtual ReceiverThread ClientReceiver
		{
			set
			{
				this.receiver = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x00098D88 File Offset: 0x00096F88
		protected internal virtual SenderThread ClientSender
		{
			set
			{
				this.sender = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00098D94 File Offset: 0x00096F94
		protected internal virtual bool ClientShutdown
		{
			set
			{
				lock (this.socketlock)
				{
					this.shutdown = value;
					if (value && this.waiters > 0)
					{
						Monitor.PulseAll(this.socketlock);
					}
				}
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00098DEC File Offset: 0x00096FEC
		protected internal virtual ONSTcpClient getClientSocket(bool block)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			ONSTcpClient result = null;
			lock (this.socketlock)
			{
				this.waiters++;
				while (!this.shutdown && block && this.socket == null)
				{
					try
					{
						Monitor.Wait(this.socketlock);
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
					}
				}
				this.waiters--;
				result = this.socket;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return result;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00098EBC File Offset: 0x000970BC
		protected internal virtual void setClientSocket(ONSTcpClient sock)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (this.socket != null)
			{
				try
				{
					this.socket.Close();
				}
				catch (IOException ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				}
				this.socket = null;
			}
			lock (this.socketlock)
			{
				this.socket = sock;
				if (this.waiters > 0)
				{
					Monitor.PulseAll(this.socketlock);
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00098F7C File Offset: 0x0009717C
		protected internal virtual void closeClientSocket()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				ONSTcpClient onstcpClient;
				lock (this.socketlock)
				{
					onstcpClient = this.socket;
					this.socket = null;
				}
				if (onstcpClient != null)
				{
					onstcpClient.Close();
				}
				this.serverVersion = 4;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00099034 File Offset: 0x00097234
		protected internal virtual ONSTcpClient connect()
		{
			TcpClient tcpClient = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			ONSTcpClient result;
			try
			{
				new SqlNetOraConfig();
				SslProtocols enabledSslProtocols = SslProtocols.Ssl2 | SslProtocols.Ssl3 | SslProtocols.Tls;
				try
				{
					int millisecondsTimeout = -1;
					if (!this.ons.localConn && this.ons.remoteIOtimeout != 0)
					{
						millisecondsTimeout = this.ons.remoteIOtimeout * 2;
					}
					IPAddress address = Dns.GetHostAddresses(this.host)[0];
					new IPEndPoint(address, this.port);
					tcpClient = new TcpClient();
					IAsyncResult asyncResult = tcpClient.BeginConnect(address, this.port, null, null);
					WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
					try
					{
						if (!asyncWaitHandle.WaitOne(millisecondsTimeout, false))
						{
							tcpClient.Close();
							throw new TimeoutException();
						}
					}
					finally
					{
						asyncWaitHandle.Close();
					}
					if (!this.ons.localConn && this.ons.remoteIOtimeout != 0)
					{
						tcpClient.ReceiveTimeout = this.ons.remoteIOtimeout;
					}
					tcpClient.LingerState = new LingerOption(true, 5);
					NetworkStream networkStream = new NetworkStream(tcpClient.Client);
					Hashtable walletLocation = SqlNetOraConfig.WalletLocation;
					string text = "";
					if (walletLocation != null)
					{
						text = ((string)walletLocation["METHOD"]).Trim().ToUpperInvariant();
					}
					if (text.Equals("MCS"))
					{
						X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
						x509Store.Open(OpenFlags.ReadOnly);
						X509CertificateCollection certificates = x509Store.Certificates;
						SslStream sslStream = new SslStream(networkStream, false, new RemoteCertificateValidationCallback(Connection.ValidateRemoteCertificate), null);
						sslStream.AuthenticateAsClient(this.host, certificates, enabledSslProtocols, false);
						return new ONSTcpClient(tcpClient, sslStream);
					}
					return new ONSTcpClient(tcpClient, networkStream);
				}
				catch (TimeoutException ex)
				{
					if (tcpClient != null)
					{
						try
						{
							tcpClient.Close();
						}
						catch (IOException ex2)
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
						}
						tcpClient = null;
					}
					throw ex;
				}
				catch (Exception)
				{
					if (tcpClient != null)
					{
						try
						{
							tcpClient.Close();
						}
						catch (IOException ex3)
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex3, null);
						}
						tcpClient = null;
					}
				}
				result = null;
			}
			catch (Exception ex4)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex4, null);
				result = null;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000992FC File Offset: 0x000974FC
		private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return true;
		}

		// Token: 0x040010F2 RID: 4338
		private ONS ons;

		// Token: 0x040010F3 RID: 4339
		private NodeList nodeList;

		// Token: 0x040010F4 RID: 4340
		private int listIndex;

		// Token: 0x040010F5 RID: 4341
		private int concurrencyIndex;

		// Token: 0x040010F6 RID: 4342
		private string host;

		// Token: 0x040010F7 RID: 4343
		private int port;

		// Token: 0x040010F8 RID: 4344
		private string id;

		// Token: 0x040010F9 RID: 4345
		private object socketlock;

		// Token: 0x040010FA RID: 4346
		private int waiters;

		// Token: 0x040010FB RID: 4347
		private int serverVersion;

		// Token: 0x040010FC RID: 4348
		protected static string RequestPrefix = "POST /connect HTTP/1.1\r\nContent-Length: 0\r\nOPMNtype: pm\r\nOPMNrequest: /";

		// Token: 0x040010FD RID: 4349
		protected static string RequestFormFactor = "\r\nFormFactor: ";

		// Token: 0x040010FE RID: 4350
		protected static string RequestSuffix = "\r\n\r\n";

		// Token: 0x040010FF RID: 4351
		protected static string ResponseOK = "HTTP/1.1 200 OK";

		// Token: 0x04001100 RID: 4352
		internal ONSTcpClient socket;

		// Token: 0x04001101 RID: 4353
		internal ReceiverThread receiver;

		// Token: 0x04001102 RID: 4354
		internal SenderThread sender;

		// Token: 0x04001103 RID: 4355
		internal long scanDelay;

		// Token: 0x04001104 RID: 4356
		internal bool shutdown;

		// Token: 0x0200017E RID: 382
		internal enum Status
		{
			// Token: 0x04001106 RID: 4358
			Idle,
			// Token: 0x04001107 RID: 4359
			Connecting,
			// Token: 0x04001108 RID: 4360
			Connected,
			// Token: 0x04001109 RID: 4361
			Shutdown
		}
	}
}
