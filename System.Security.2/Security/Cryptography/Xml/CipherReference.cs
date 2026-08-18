using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000039 RID: 57
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CipherReference : EncryptedReference
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00007D3E File Offset: 0x00005F3E
		public CipherReference()
		{
			base.ReferenceType = "CipherReference";
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007D51 File Offset: 0x00005F51
		public CipherReference(string uri) : base(uri)
		{
			base.ReferenceType = "CipherReference";
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007D65 File Offset: 0x00005F65
		public CipherReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "CipherReference";
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00007D7A File Offset: 0x00005F7A
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00007D8C File Offset: 0x00005F8C
		internal byte[] CipherValue
		{
			get
			{
				if (!base.CacheValid)
				{
					return null;
				}
				return this.m_cipherValue;
			}
			set
			{
				this.m_cipherValue = value;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007D98 File Offset: 0x00005F98
		public override XmlElement GetXml()
		{
			if (base.CacheValid)
			{
				return this.m_cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007DC8 File Offset: 0x00005FC8
		internal new XmlElement GetXml(XmlDocument document)
		{
			if (base.ReferenceType == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_ReferenceTypeRequired"));
			}
			XmlElement xmlElement = document.CreateElement(base.ReferenceType, "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(base.Uri))
			{
				xmlElement.SetAttribute("URI", base.Uri);
			}
			if (base.TransformChain.Count > 0)
			{
				xmlElement.AppendChild(base.TransformChain.GetXml(document, "http://www.w3.org/2001/04/xmlenc#"));
			}
			return xmlElement;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007E44 File Offset: 0x00006044
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.ReferenceType = value.LocalName;
			string attribute = Utils.GetAttribute(value, "URI", "http://www.w3.org/2001/04/xmlenc#");
			if (!Utils.GetSkipSignatureAttributeEnforcement() && attribute == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriRequired"));
			}
			base.Uri = attribute;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:Transforms", xmlNamespaceManager);
			if (xmlNode != null)
			{
				base.TransformChain.LoadXml(xmlNode as XmlElement);
			}
			this.m_cachedXml = value;
		}

		// Token: 0x040003B6 RID: 950
		private byte[] m_cipherValue;
	}
}
