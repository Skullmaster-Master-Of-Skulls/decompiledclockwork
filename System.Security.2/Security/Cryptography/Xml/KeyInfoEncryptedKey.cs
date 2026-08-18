using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004A RID: 74
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoEncryptedKey : KeyInfoClause
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000A5FF File Offset: 0x000087FF
		public KeyInfoEncryptedKey()
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A719 File Offset: 0x00008919
		public KeyInfoEncryptedKey(EncryptedKey encryptedKey)
		{
			this.m_encryptedKey = encryptedKey;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A728 File Offset: 0x00008928
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000A730 File Offset: 0x00008930
		public EncryptedKey EncryptedKey
		{
			get
			{
				return this.m_encryptedKey;
			}
			set
			{
				this.m_encryptedKey = value;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A739 File Offset: 0x00008939
		public override XmlElement GetXml()
		{
			if (this.m_encryptedKey == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "KeyInfoEncryptedKey");
			}
			return this.m_encryptedKey.GetXml();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A763 File Offset: 0x00008963
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			if (this.m_encryptedKey == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "KeyInfoEncryptedKey");
			}
			return this.m_encryptedKey.GetXml(xmlDocument);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A78E File Offset: 0x0000898E
		public override void LoadXml(XmlElement value)
		{
			this.m_encryptedKey = new EncryptedKey();
			this.m_encryptedKey.LoadXml(value);
		}

		// Token: 0x040003F0 RID: 1008
		private EncryptedKey m_encryptedKey;
	}
}
