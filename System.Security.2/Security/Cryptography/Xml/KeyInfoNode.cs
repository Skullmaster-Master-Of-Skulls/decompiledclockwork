using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004D RID: 77
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoNode : KeyInfoClause
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000A5FF File Offset: 0x000087FF
		public KeyInfoNode()
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B0B8 File Offset: 0x000092B8
		public KeyInfoNode(XmlElement node)
		{
			this.m_node = node;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B0C7 File Offset: 0x000092C7
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000B0CF File Offset: 0x000092CF
		public XmlElement Value
		{
			get
			{
				return this.m_node;
			}
			set
			{
				this.m_node = value;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B0F9 File Offset: 0x000092F9
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			return xmlDocument.ImportNode(this.m_node, true) as XmlElement;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B0CF File Offset: 0x000092CF
		public override void LoadXml(XmlElement value)
		{
			this.m_node = value;
		}

		// Token: 0x040003F8 RID: 1016
		private XmlElement m_node;
	}
}
