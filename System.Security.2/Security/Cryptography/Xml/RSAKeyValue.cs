using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000048 RID: 72
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class RSAKeyValue : KeyInfoClause
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000A4DC File Offset: 0x000086DC
		public RSAKeyValue()
		{
			this.m_key = RSA.Create();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A4EF File Offset: 0x000086EF
		public RSAKeyValue(RSA key)
		{
			this.m_key = key;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A4FE File Offset: 0x000086FE
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000A506 File Offset: 0x00008706
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

		// Token: 0x06000247 RID: 583 RVA: 0x0000A510 File Offset: 0x00008710
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000A534 File Offset: 0x00008734
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

		// Token: 0x06000249 RID: 585 RVA: 0x0000A5DE File Offset: 0x000087DE
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_key.FromXmlString(value.OuterXml);
		}

		// Token: 0x040003ED RID: 1005
		private RSA m_key;
	}
}
