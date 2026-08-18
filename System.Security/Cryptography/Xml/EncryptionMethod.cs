using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000BC RID: 188
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EncryptionMethod
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00016CB1 File Offset: 0x00015CB1
		public EncryptionMethod()
		{
			this.m_cachedXml = null;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016CC0 File Offset: 0x00015CC0
		public EncryptionMethod(string algorithm)
		{
			this.m_algorithm = algorithm;
			this.m_cachedXml = null;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00016CD6 File Offset: 0x00015CD6
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00016CE4 File Offset: 0x00015CE4
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00016CEC File Offset: 0x00015CEC
		public int KeySize
		{
			get
			{
				return this.m_keySize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidKeySize"));
				}
				this.m_keySize = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00016D10 File Offset: 0x00015D10
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00016D18 File Offset: 0x00015D18
		public string KeyAlgorithm
		{
			get
			{
				return this.m_algorithm;
			}
			set
			{
				this.m_algorithm = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016D28 File Offset: 0x00015D28
		public XmlElement GetXml()
		{
			if (this.CacheValid)
			{
				return this.m_cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016D58 File Offset: 0x00015D58
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptionMethod", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(this.m_algorithm))
			{
				xmlElement.SetAttribute("Algorithm", this.m_algorithm);
			}
			if (this.m_keySize > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("KeySize", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.AppendChild(document.CreateTextNode(this.m_keySize.ToString(null, null)));
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00016DD4 File Offset: 0x00015DD4
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this.m_algorithm = Utils.GetAttribute(value, "Algorithm", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:KeySize", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.KeySize = Convert.ToInt32(Utils.DiscardWhiteSpaces(xmlNode.InnerText), null);
			}
			this.m_cachedXml = value;
		}

		// Token: 0x040005A8 RID: 1448
		private XmlElement m_cachedXml;

		// Token: 0x040005A9 RID: 1449
		private int m_keySize;

		// Token: 0x040005AA RID: 1450
		private string m_algorithm;
	}
}
