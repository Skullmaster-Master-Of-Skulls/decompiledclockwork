using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003D RID: 61
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EncryptionMethod
	{
		// Token: 0x060001BF RID: 447 RVA: 0x000080B9 File Offset: 0x000062B9
		public EncryptionMethod()
		{
			this.m_cachedXml = null;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000080C8 File Offset: 0x000062C8
		public EncryptionMethod(string algorithm)
		{
			this.m_algorithm = algorithm;
			this.m_cachedXml = null;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000080DE File Offset: 0x000062DE
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000080E9 File Offset: 0x000062E9
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000080F1 File Offset: 0x000062F1
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008115 File Offset: 0x00006315
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000811D File Offset: 0x0000631D
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

		// Token: 0x060001C6 RID: 454 RVA: 0x00008130 File Offset: 0x00006330
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

		// Token: 0x060001C7 RID: 455 RVA: 0x00008160 File Offset: 0x00006360
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

		// Token: 0x060001C8 RID: 456 RVA: 0x000081DC File Offset: 0x000063DC
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

		// Token: 0x040003C1 RID: 961
		private XmlElement m_cachedXml;

		// Token: 0x040003C2 RID: 962
		private int m_keySize;

		// Token: 0x040003C3 RID: 963
		private string m_algorithm;
	}
}
