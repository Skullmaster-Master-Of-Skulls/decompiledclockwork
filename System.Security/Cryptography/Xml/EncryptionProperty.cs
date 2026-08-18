using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000BD RID: 189
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EncryptionProperty
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x00016E56 File Offset: 0x00015E56
		public EncryptionProperty()
		{
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00016E60 File Offset: 0x00015E60
		public EncryptionProperty(XmlElement elementProperty)
		{
			if (elementProperty == null)
			{
				throw new ArgumentNullException("elementProperty");
			}
			if (elementProperty.LocalName != "EncryptionProperty" || elementProperty.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidEncryptionProperty"));
			}
			this.m_elemProp = elementProperty;
			this.m_cachedXml = null;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00016EC3 File Offset: 0x00015EC3
		public string Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00016ECB File Offset: 0x00015ECB
		public string Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00016ED3 File Offset: 0x00015ED3
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00016EDC File Offset: 0x00015EDC
		public XmlElement PropertyElement
		{
			get
			{
				return this.m_elemProp;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidEncryptionProperty"));
				}
				this.m_elemProp = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00016F39 File Offset: 0x00015F39
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00016F48 File Offset: 0x00015F48
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

		// Token: 0x06000471 RID: 1137 RVA: 0x00016F78 File Offset: 0x00015F78
		internal XmlElement GetXml(XmlDocument document)
		{
			return document.ImportNode(this.m_elemProp, true) as XmlElement;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00016F8C File Offset: 0x00015F8C
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidEncryptionProperty"));
			}
			this.m_cachedXml = value;
			this.m_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
			this.m_target = Utils.GetAttribute(value, "Target", "http://www.w3.org/2001/04/xmlenc#");
			this.m_elemProp = value;
		}

		// Token: 0x040005AB RID: 1451
		private string m_target;

		// Token: 0x040005AC RID: 1452
		private string m_id;

		// Token: 0x040005AD RID: 1453
		private XmlElement m_elemProp;

		// Token: 0x040005AE RID: 1454
		private XmlElement m_cachedXml;
	}
}
