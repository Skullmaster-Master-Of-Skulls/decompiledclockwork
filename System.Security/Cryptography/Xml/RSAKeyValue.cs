using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200009F RID: 159
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class RSAKeyValue : KeyInfoClause
	{
		// Token: 0x06000304 RID: 772 RVA: 0x000101B5 File Offset: 0x0000F1B5
		public RSAKeyValue()
		{
			this.m_key = RSA.Create();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000101C8 File Offset: 0x0000F1C8
		public RSAKeyValue(RSA key)
		{
			this.m_key = key;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000101D7 File Offset: 0x0000F1D7
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000101DF File Offset: 0x0000F1DF
		public RSA Key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000101E8 File Offset: 0x0000F1E8
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001020C File Offset: 0x0000F20C
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			RSAParameters rsaparameters = this.m_key.ExportParameters(false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("RSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("Modulus", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rsaparameters.Modulus)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Exponent", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rsaparameters.Exponent)));
			xmlElement2.AppendChild(xmlElement4);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000102B8 File Offset: 0x0000F2B8
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_key.FromXmlString(value.OuterXml);
		}

		// Token: 0x04000503 RID: 1283
		private RSA m_key;
	}
}
