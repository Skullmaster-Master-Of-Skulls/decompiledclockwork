using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Renci.SshNet.Common;

namespace Renci.SshNet.NetConf
{
	// Token: 0x02000096 RID: 150
	internal class NetConfSession : SubsystemSession, INetConfSession, ISubsystemSession, IDisposable
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001D54B File Offset: 0x0001B74B
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0001D553 File Offset: 0x0001B753
		public XmlDocument ServerCapabilities { get; private set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001D55C File Offset: 0x0001B75C
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0001D564 File Offset: 0x0001B764
		public XmlDocument ClientCapabilities { get; private set; }

		// Token: 0x0600077E RID: 1918 RVA: 0x0001D570 File Offset: 0x0001B770
		public NetConfSession(ISession session, TimeSpan operationTimeout) : base(session, "netconf", operationTimeout, Encoding.UTF8)
		{
			this.ClientCapabilities = new XmlDocument();
			this.ClientCapabilities.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><hello xmlns=\"urn:ietf:params:xml:ns:netconf:base:1.0\"><capabilities><capability>urn:ietf:params:netconf:base:1.0</capability></capabilities></hello>");
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		public XmlDocument SendReceiveRpc(XmlDocument rpc, bool automaticMessageIdHandling)
		{
			this._data.Clear();
			XmlNamespaceManager xmlNamespaceManager = null;
			if (automaticMessageIdHandling)
			{
				this._messageId++;
				xmlNamespaceManager = new XmlNamespaceManager(rpc.NameTable);
				xmlNamespaceManager.AddNamespace("nc", "urn:ietf:params:xml:ns:netconf:base:1.0");
				rpc.SelectSingleNode("/nc:rpc/@message-id", xmlNamespaceManager).Value = this._messageId.ToString(CultureInfo.InvariantCulture);
			}
			this._rpcReply = new StringBuilder();
			this._rpcReplyReceived.Reset();
			XmlDocument xmlDocument = new XmlDocument();
			if (this._usingFramingProtocol)
			{
				StringBuilder stringBuilder = new StringBuilder(rpc.InnerXml.Length + 10);
				stringBuilder.AppendFormat("\n#{0}\n", rpc.InnerXml.Length);
				stringBuilder.Append(rpc.InnerXml);
				stringBuilder.Append("\n##\n");
				base.SendData(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
				base.WaitOnHandle(this._rpcReplyReceived, base.OperationTimeout);
				xmlDocument.LoadXml(this._rpcReply.ToString());
			}
			else
			{
				base.SendData(Encoding.UTF8.GetBytes(rpc.InnerXml + "]]>]]>"));
				base.WaitOnHandle(this._rpcReplyReceived, base.OperationTimeout);
				xmlDocument.LoadXml(this._rpcReply.ToString());
			}
			if (automaticMessageIdHandling && rpc.SelectSingleNode("/nc:rpc/@message-id", xmlNamespaceManager).Value != this._messageId.ToString(CultureInfo.InvariantCulture))
			{
				throw new NetConfServerException("The rpc message id does not match the rpc-reply message id.");
			}
			return xmlDocument;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001D768 File Offset: 0x0001B968
		protected override void OnChannelOpen()
		{
			this._data.Clear();
			string s = string.Format("{0}{1}", this.ClientCapabilities.InnerXml, "]]>]]>");
			base.SendData(Encoding.UTF8.GetBytes(s));
			base.WaitOnHandle(this._serverCapabilitiesConfirmed, base.OperationTimeout);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		protected override void OnDataReceived(byte[] data)
		{
			string text = Encoding.UTF8.GetString(data);
			if (this.ServerCapabilities != null)
			{
				if (this._usingFramingProtocol)
				{
					int num = 0;
					for (;;)
					{
						Match match = Regex.Match(text.Substring(num), "\\n#(?<length>\\d+)\\n");
						if (!match.Success)
						{
							break;
						}
						int num2 = Convert.ToInt32(match.Groups["length"].Value);
						this._rpcReply.Append(text, num + match.Index + match.Length, num2);
						num += match.Index + match.Length + num2;
					}
					if (Regex.IsMatch(text.Substring(num), "\\n##\\n"))
					{
						this._rpcReplyReceived.Set();
						return;
					}
				}
				else
				{
					this._data.Append(text);
					if (!text.Contains("]]>]]>"))
					{
						return;
					}
					text = this._data.ToString();
					this._data.Clear();
					this._rpcReply.Append(text.Replace("]]>]]>", ""));
					this._rpcReplyReceived.Set();
				}
				return;
			}
			this._data.Append(text);
			if (!text.Contains("]]>]]>"))
			{
				return;
			}
			try
			{
				text = this._data.ToString();
				this._data.Clear();
				this.ServerCapabilities = new XmlDocument();
				this.ServerCapabilities.LoadXml(text.Replace("]]>]]>", ""));
			}
			catch (XmlException innerException)
			{
				throw new NetConfServerException("Server capabilities received are not well formed XML", innerException);
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.ServerCapabilities.NameTable);
			xmlNamespaceManager.AddNamespace("nc", "urn:ietf:params:xml:ns:netconf:base:1.0");
			this._usingFramingProtocol = (this.ServerCapabilities.SelectSingleNode("/nc:hello/nc:capabilities/nc:capability[text()='urn:ietf:params:netconf:base:1.1']", xmlNamespaceManager) != null);
			this._serverCapabilitiesConfirmed.Set();
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001D9A0 File Offset: 0x0001BBA0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._serverCapabilitiesConfirmed != null)
				{
					this._serverCapabilitiesConfirmed.Dispose();
					this._serverCapabilitiesConfirmed = null;
				}
				if (this._rpcReplyReceived != null)
				{
					this._rpcReplyReceived.Dispose();
					this._rpcReplyReceived = null;
				}
			}
		}

		// Token: 0x040002EF RID: 751
		private const string Prompt = "]]>]]>";

		// Token: 0x040002F0 RID: 752
		private readonly StringBuilder _data = new StringBuilder();

		// Token: 0x040002F1 RID: 753
		private bool _usingFramingProtocol;

		// Token: 0x040002F2 RID: 754
		private EventWaitHandle _serverCapabilitiesConfirmed = new AutoResetEvent(false);

		// Token: 0x040002F3 RID: 755
		private EventWaitHandle _rpcReplyReceived = new AutoResetEvent(false);

		// Token: 0x040002F4 RID: 756
		private StringBuilder _rpcReply = new StringBuilder();

		// Token: 0x040002F5 RID: 757
		private int _messageId;
	}
}
