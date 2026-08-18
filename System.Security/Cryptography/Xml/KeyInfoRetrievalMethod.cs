using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A0 RID: 160
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoRetrievalMethod : KeyInfoClause
	{
		// Token: 0x0600030B RID: 779 RVA: 0x000102D9 File Offset: 0x0000F2D9
		public KeyInfoRetrievalMethod()
		{
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000102E1 File Offset: 0x0000F2E1
		public KeyInfoRetrievalMethod(string strUri)
		{
			this.m_uri = strUri;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000102F0 File Offset: 0x0000F2F0
		public KeyInfoRetrievalMethod(string strUri, string typeName)
		{
			this.m_uri = strUri;
			this.m_type = typeName;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00010306 File Offset: 0x0000F306
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0001030E File Offset: 0x0000F30E
		public string Uri
		{
			get
			{
				return this.m_uri;
			}
			set
			{
				this.m_uri = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00010317 File Offset: 0x0000F317
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0001031F File Offset: 0x0000F31F
		[ComVisible(false)]
		public string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010328 File Offset: 0x0000F328
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001034C File Offset: 0x0000F34C
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("RetrievalMethod", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.m_uri))
			{
				xmlElement.SetAttribute("URI", this.m_uri);
			}
			if (!string.IsNullOrEmpty(this.m_type))
			{
				xmlElement.SetAttribute("Type", this.m_type);
			}
			return xmlElement;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000103A7 File Offset: 0x0000F3A7
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			this.m_type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x04000504 RID: 1284
		private string m_uri;

		// Token: 0x04000505 RID: 1285
		private string m_type;
	}
}
