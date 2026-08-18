using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003E RID: 62
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EncryptionProperty
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x000044A9 File Offset: 0x000026A9
		public EncryptionProperty()
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008260 File Offset: 0x00006460
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000082C3 File Offset: 0x000064C3
		public string Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000082CB File Offset: 0x000064CB
		public string Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000082D3 File Offset: 0x000064D3
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000082DC File Offset: 0x000064DC
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008339 File Offset: 0x00006539
		private bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008344 File Offset: 0x00006544
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

		// Token: 0x060001D1 RID: 465 RVA: 0x00008374 File Offset: 0x00006574
		internal XmlElement GetXml(XmlDocument document)
		{
			return document.ImportNode(this.m_elemProp, true) as XmlElement;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008388 File Offset: 0x00006588
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

		// Token: 0x040003C4 RID: 964
		private string m_target;

		// Token: 0x040003C5 RID: 965
		private string m_id;

		// Token: 0x040003C6 RID: 966
		private XmlElement m_elemProp;

		// Token: 0x040003C7 RID: 967
		private XmlElement m_cachedXml;
	}
}
