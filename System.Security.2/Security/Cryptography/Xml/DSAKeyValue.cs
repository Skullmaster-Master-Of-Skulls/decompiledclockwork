using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000047 RID: 71
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class DSAKeyValue : KeyInfoClause
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000A29E File Offset: 0x0000849E
		public DSAKeyValue()
		{
			this.m_key = DSA.Create();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A2B1 File Offset: 0x000084B1
		public DSAKeyValue(DSA key)
		{
			this.m_key = key;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000A2C0 File Offset: 0x000084C0
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public DSA Key
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

		// Token: 0x06000240 RID: 576 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A2F8 File Offset: 0x000084F8
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			DSAParameters dsaparameters = this.m_key.ExportParameters(false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("DSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("P", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.P)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Q", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Q)));
			xmlElement2.AppendChild(xmlElement4);
			XmlElement xmlElement5 = xmlDocument.CreateElement("G", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement5.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.G)));
			xmlElement2.AppendChild(xmlElement5);
			XmlElement xmlElement6 = xmlDocument.CreateElement("Y", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement6.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Y)));
			xmlElement2.AppendChild(xmlElement6);
			if (dsaparameters.J != null)
			{
				XmlElement xmlElement7 = xmlDocument.CreateElement("J", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement7.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.J)));
				xmlElement2.AppendChild(xmlElement7);
			}
			if (dsaparameters.Seed != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("Seed", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Seed)));
				xmlElement2.AppendChild(xmlElement8);
				XmlElement xmlElement9 = xmlDocument.CreateElement("PgenCounter", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement9.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaparameters.Counter))));
				xmlElement2.AppendChild(xmlElement9);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A4BB File Offset: 0x000086BB
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_key.FromXmlString(value.OuterXml);
		}

		// Token: 0x040003EC RID: 1004
		private DSA m_key;
	}
}
