using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000049 RID: 73
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoRetrievalMethod : KeyInfoClause
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000A5FF File Offset: 0x000087FF
		public KeyInfoRetrievalMethod()
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A607 File Offset: 0x00008807
		public KeyInfoRetrievalMethod(string strUri)
		{
			this.m_uri = strUri;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A616 File Offset: 0x00008816
		public KeyInfoRetrievalMethod(string strUri, string typeName)
		{
			this.m_uri = strUri;
			this.m_type = typeName;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A62C File Offset: 0x0000882C
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A634 File Offset: 0x00008834
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A63D File Offset: 0x0000883D
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000A645 File Offset: 0x00008845
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

		// Token: 0x06000251 RID: 593 RVA: 0x0000A650 File Offset: 0x00008850
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A674 File Offset: 0x00008874
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

		// Token: 0x06000253 RID: 595 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			this.m_type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x040003EE RID: 1006
		private string m_uri;

		// Token: 0x040003EF RID: 1007
		private string m_type;
	}
}
