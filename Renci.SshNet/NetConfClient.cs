using System;
using System.Xml;
using Renci.SshNet.NetConf;

namespace Renci.SshNet
{
	// Token: 0x02000027 RID: 39
	public class NetConfClient : BaseClient
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000077B3 File Offset: 0x000059B3
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000077BB File Offset: 0x000059BB
		public TimeSpan OperationTimeout { get; set; }

		// Token: 0x060001FF RID: 511 RVA: 0x000077C4 File Offset: 0x000059C4
		public NetConfClient(ConnectionInfo connectionInfo) : this(connectionInfo, false)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000077CE File Offset: 0x000059CE
		public NetConfClient(string host, int port, string username, string password) : this(new PasswordConnectionInfo(host, port, username, password), true)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000077E1 File Offset: 0x000059E1
		public NetConfClient(string host, string username, string password) : this(host, ConnectionInfo.DefaultPort, username, password)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000077F1 File Offset: 0x000059F1
		public NetConfClient(string host, int port, string username, params PrivateKeyFile[] keyFiles) : this(new PrivateKeyConnectionInfo(host, port, username, keyFiles), true)
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007804 File Offset: 0x00005A04
		public NetConfClient(string host, string username, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, keyFiles)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007814 File Offset: 0x00005A14
		private NetConfClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo) : this(connectionInfo, ownsConnectionInfo, new ServiceFactory())
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007823 File Offset: 0x00005A23
		internal NetConfClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo, IServiceFactory serviceFactory) : base(connectionInfo, ownsConnectionInfo, serviceFactory)
		{
			this.OperationTimeout = Renci.SshNet.Session.InfiniteTimeSpan;
			this.AutomaticMessageIdHandling = true;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00007840 File Offset: 0x00005A40
		public XmlDocument ServerCapabilities
		{
			get
			{
				return this._netConfSession.ServerCapabilities;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000784D File Offset: 0x00005A4D
		public XmlDocument ClientCapabilities
		{
			get
			{
				return this._netConfSession.ClientCapabilities;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000785A File Offset: 0x00005A5A
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00007862 File Offset: 0x00005A62
		public bool AutomaticMessageIdHandling { get; set; }

		// Token: 0x0600020A RID: 522 RVA: 0x0000786B File Offset: 0x00005A6B
		public XmlDocument SendReceiveRpc(XmlDocument rpc)
		{
			return this._netConfSession.SendReceiveRpc(rpc, this.AutomaticMessageIdHandling);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007880 File Offset: 0x00005A80
		public XmlDocument SendReceiveRpc(string xml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			return this.SendReceiveRpc(xmlDocument);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000078A4 File Offset: 0x00005AA4
		public XmlDocument SendCloseRpc()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><rpc message-id=\"6666\" xmlns=\"urn:ietf:params:xml:ns:netconf:base:1.0\"><close-session/></rpc>");
			return this._netConfSession.SendReceiveRpc(xmlDocument, this.AutomaticMessageIdHandling);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000078D4 File Offset: 0x00005AD4
		protected override void OnConnected()
		{
			base.OnConnected();
			this._netConfSession = base.ServiceFactory.CreateNetConfSession(base.Session, this.OperationTimeout);
			this._netConfSession.Connect();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007904 File Offset: 0x00005B04
		protected override void OnDisconnecting()
		{
			base.OnDisconnecting();
			this._netConfSession.Disconnect();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007917 File Offset: 0x00005B17
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this._netConfSession != null)
			{
				this._netConfSession.Dispose();
				this._netConfSession = null;
			}
		}

		// Token: 0x0400008E RID: 142
		private INetConfSession _netConfSession;
	}
}
