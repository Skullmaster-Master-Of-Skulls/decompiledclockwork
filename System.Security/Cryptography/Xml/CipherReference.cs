using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C0 RID: 192
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CipherReference : EncryptedReference
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0001742E File Offset: 0x0001642E
		public CipherReference()
		{
			base.ReferenceType = "CipherReference";
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00017441 File Offset: 0x00016441
		public CipherReference(string uri) : base(uri)
		{
			base.ReferenceType = "CipherReference";
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00017455 File Offset: 0x00016455
		public CipherReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "CipherReference";
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001746A File Offset: 0x0001646A
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0001747C File Offset: 0x0001647C
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

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017488 File Offset: 0x00016488
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

		// Token: 0x060004A1 RID: 1185 RVA: 0x000174B8 File Offset: 0x000164B8
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

		// Token: 0x060004A2 RID: 1186 RVA: 0x00017534 File Offset: 0x00016534
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

		// Token: 0x040005B4 RID: 1460
		private byte[] m_cipherValue;
	}
}
