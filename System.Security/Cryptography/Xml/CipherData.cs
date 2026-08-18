using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C6 RID: 198
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CipherData
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x000181D8 File Offset: 0x000171D8
		public CipherData()
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000181E0 File Offset: 0x000171E0
		public CipherData(byte[] cipherValue)
		{
			this.CipherValue = cipherValue;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000181EF File Offset: 0x000171EF
		public CipherData(CipherReference cipherReference)
		{
			this.CipherReference = cipherReference;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000181FE File Offset: 0x000171FE
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001820C File Offset: 0x0001720C
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00018214 File Offset: 0x00017214
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001824A File Offset: 0x0001724A
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00018252 File Offset: 0x00017252
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

		// Token: 0x060004D4 RID: 1236 RVA: 0x00018294 File Offset: 0x00017294
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

		// Token: 0x060004D5 RID: 1237 RVA: 0x000182C4 File Offset: 0x000172C4
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

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001834C File Offset: 0x0001734C
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

		// Token: 0x040005B9 RID: 1465
		private XmlElement m_cachedXml;

		// Token: 0x040005BA RID: 1466
		private CipherReference m_cipherReference;

		// Token: 0x040005BB RID: 1467
		private byte[] m_cipherValue;
	}
}
