using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000033 RID: 51
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CipherData
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000044A9 File Offset: 0x000026A9
		public CipherData()
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006AA7 File Offset: 0x00004CA7
		public CipherData(byte[] cipherValue)
		{
			this.CipherValue = cipherValue;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006AB6 File Offset: 0x00004CB6
		public CipherData(CipherReference cipherReference)
		{
			this.CipherReference = cipherReference;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006AC5 File Offset: 0x00004CC5
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006AD0 File Offset: 0x00004CD0
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public CipherReference CipherReference
		{
			get
			{
				return this.m_cipherReference;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.CipherValue != null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CipherValueElementRequired"));
				}
				this.m_cipherReference = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006B0E File Offset: 0x00004D0E
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006B16 File Offset: 0x00004D16
		public byte[] CipherValue
		{
			get
			{
				return this.m_cipherValue;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.CipherReference != null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CipherValueElementRequired"));
				}
				this.m_cipherValue = (byte[])value.Clone();
				this.m_cachedXml = null;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006B58 File Offset: 0x00004D58
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

		// Token: 0x0600015A RID: 346 RVA: 0x00006B88 File Offset: 0x00004D88
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("CipherData", "http://www.w3.org/2001/04/xmlenc#");
			if (this.CipherValue != null)
			{
				XmlElement xmlElement2 = document.CreateElement("CipherValue", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.AppendChild(document.CreateTextNode(Convert.ToBase64String(this.CipherValue)));
				xmlElement.AppendChild(xmlElement2);
			}
			else
			{
				if (this.CipherReference == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CipherValueElementRequired"));
				}
				xmlElement.AppendChild(this.CipherReference.GetXml(document));
			}
			return xmlElement;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006C10 File Offset: 0x00004E10
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:CipherValue", xmlNamespaceManager);
			XmlNode xmlNode2 = value.SelectSingleNode("enc:CipherReference", xmlNamespaceManager);
			if (xmlNode != null)
			{
				if (xmlNode2 != null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CipherValueElementRequired"));
				}
				this.m_cipherValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode.InnerText));
			}
			else
			{
				if (xmlNode2 == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CipherValueElementRequired"));
				}
				this.m_cipherReference = new CipherReference();
				this.m_cipherReference.LoadXml((XmlElement)xmlNode2);
			}
			this.m_cachedXml = value;
		}

		// Token: 0x040003A6 RID: 934
		private XmlElement m_cachedXml;

		// Token: 0x040003A7 RID: 935
		private CipherReference m_cipherReference;

		// Token: 0x040003A8 RID: 936
		private byte[] m_cipherValue;
	}
}
