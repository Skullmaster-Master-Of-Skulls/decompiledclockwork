using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A4 RID: 164
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoNode : KeyInfoClause
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00010DA0 File Offset: 0x0000FDA0
		public KeyInfoNode()
		{
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00010DA8 File Offset: 0x0000FDA8
		public KeyInfoNode(XmlElement node)
		{
			this.m_node = node;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00010DB7 File Offset: 0x0000FDB7
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00010DBF File Offset: 0x0000FDBF
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

		// Token: 0x06000339 RID: 825 RVA: 0x00010DC8 File Offset: 0x0000FDC8
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010DE9 File Offset: 0x0000FDE9
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			return xmlDocument.ImportNode(this.m_node, true) as XmlElement;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010DFD File Offset: 0x0000FDFD
		public override void LoadXml(XmlElement value)
		{
			this.m_node = value;
		}

		// Token: 0x0400050E RID: 1294
		private XmlElement m_node;
	}
}
